using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System.Web.DynamicData;
using System.Diagnostics;

public class _BLOLN_QuestionPaperSeries 
{
 DataLayer  DL = new DataLayer();











public void GetOLN_QuestionPaperSeries(_GCOLN_QuestionPaperSeries gc)
{
string qry="SELECT * FROM [OLN_QuestionPaperSeries] WHERE [SeriesCode]=@SeriesCode";
 SqlCommand cmd = new SqlCommand();
cmd.Parameters.AddWithValue("@SeriesCode", gc.SeriesCode);
 cmd.CommandText = qry;
SqlDataReader dtrData = (SqlDataReader) (DL.GetReader(cmd));
if (dtrData.Read())
{
gc.Academicyear=dtrData["Academicyear"].ToString();
gc.Slno= dtrData["Slno"].ToString();
gc.examcode= dtrData["examcode"].ToString();
			if (dtrData["ExamDate"] != DBNull.Value)
			{
                gc.ExamDate = Convert.ToDateTime(dtrData["ExamDate"]);
            }

            if (dtrData["ExamTimingFrom"] != DBNull.Value)
            {
                gc.ExamTimingFrom = Convert.ToDateTime(dtrData["ExamTimingFrom"]);
            }

            if (dtrData["ExamTimingTo"] != DBNull.Value)
            {
                gc.ExamTimingTo = Convert.ToDateTime(dtrData["ExamTimingTo"]);
            }


          



          
gc.SeriesCode=dtrData["SeriesCode"].ToString();
gc.Seriesquestion_code= dtrData["Seriesquestion_code"].ToString();
gc.Orgquestion_code= dtrData["Orgquestion_code"].ToString();

           

            gc.Negativemarks= dtrData["Negativemarks"].ToString();
gc.Marks= dtrData["Marks"].ToString();
gc.question_code=dtrData["question_code"].ToString();
gc.question=dtrData["question"].ToString();
gc.option_a=dtrData["option_a"].ToString();
gc.option_b=dtrData["option_b"].ToString();
gc.option_c=dtrData["option_c"].ToString();
gc.option_d=dtrData["option_d"].ToString();
gc.answer=dtrData["answer"].ToString();
gc.solutions=dtrData["solutions"].ToString();
gc.difficulty_level=dtrData["difficulty_level"].ToString();
gc.created_by=dtrData["created_by"].ToString();
            if (dtrData["created_date"] != DBNull.Value)
            {
                gc.created_date = Convert.ToDateTime(dtrData["created_date"]);
            }
         
gc.ic= dtrData["ic"].ToString();
gc.sc=dtrData["sc"].ToString();
gc.subjectcode=dtrData["subjectcode"].ToString();
gc.chapter=dtrData["chapter"].ToString();
}
}













public void RptOLN_QuestionPaperSeriesAdapter(Repeater list)
{
 	string qry = @"SELECT * from [OLN_QuestionPaperSeries] order by SeriesCode ";
	DataTable dt = new DataTable();
	SqlCommand cmd = new SqlCommand();
	cmd.CommandText = qry;
	dt = DL.GetDataTable(cmd);
	list.DataSource = dt;
 	list.DataBind();
}



    public void Fill_Exam_DropDown(DropDownList ddl,string ic,string sc)
    {
        string query = " SELECT exam_code,exam_name from OLN_ExamMaster where sc=@sc and ic=@ic order by exam_code";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
      
        cmd.Parameters.AddWithValue("@ic", ic);
        cmd.Parameters.AddWithValue("@sc", sc);
        DataTable dt = new DataTable();
        dt = DL.GetDataTable(cmd);
        ddl.DataSource = dt;
        ddl.DataValueField = "exam_code";
        ddl.DataTextField = "exam_name";
        ddl.DataBind();
    }


    public DataTable GetQuestions_For_Proccessing(string ic,string sc,string examCode,string year)
    {
        string query = "  SELECT * FROM OLN_QuestionPaper where Academicyear=@year and examcode=@examCode and ic=@ic and sc=@sc";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@ic", ic);
        cmd.Parameters.AddWithValue("@sc", sc);
        cmd.Parameters.AddWithValue("@year", year);
        cmd.Parameters.AddWithValue("@examCode", examCode);

        DataTable dt = new DataTable();
        dt = DL.GetDataTable(cmd);
        return dt;
    }


    public void Fill_Exam_AndSeriesCode(DropDownList ddlExam,DropDownList ddlSeries,string ic,string sc)
    {
        string query = "  SELECT EXAM_CODE,EXAM_NAME FROM OLN_ExamMaster WHERE EXAM_CODE IN (SELECT distinct examcode from OLN_QuestionPaperSeries) and ic=@ic and sc=@sc;";
        query += "  SELECT DISTINCT SeriesCode FROM OLN_QuestionPaperSeries where  ic=@ic and sc=@sc ORDER BY SeriesCode";
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@ic", ic);
        cmd.Parameters.AddWithValue("@sc", sc);
        cmd.CommandText = query;


        DataSet ds = new DataSet();
        ds = DL.ReturnDataSet(cmd);
        Bind_DropDown(ddlExam, "EXAM_CODE", "EXAM_NAME", ds.Tables[0]);
        Bind_DropDown(ddlSeries, "SeriesCode", "SeriesCode", ds.Tables[1]);





    }


    public void Bind_Series_Report(Repeater rpt,string ic,string sc,string examcode,string series,string  year)
    {
        string query = String.Format(@"
  SELECT OLN_Subjects.name, OLN_QuestionPaperSeries.Academicyear, OLN_QuestionPaperSeries.Slno, OLN_QuestionPaperSeries.examcode, OLN_QuestionPaperSeries.SeriesCode, OLN_QuestionPaperSeries.Seriesquestion_code, OLN_QuestionPaperSeries.Orgquestion_code, OLN_QuestionPaperSeries.Negativemarks, OLN_QuestionPaperSeries.question_code, 
         OLN_QuestionPaperSeries.question, OLN_QuestionPaperSeries.option_a, OLN_QuestionPaperSeries.option_b, OLN_QuestionPaperSeries.option_c, OLN_QuestionPaperSeries.option_d, OLN_QuestionPaperSeries.answer
FROM  OLN_QuestionPaperSeries INNER JOIN
         OLN_Subjects ON OLN_QuestionPaperSeries.subjectcode = OLN_Subjects.subjectcode
		 where Academicyear=@year and examcode=@examcode and SeriesCode=@codeSeries 
		 order by Seriesquestion_code


            ");


        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddWithValue("@ic", ic);
        cmd.Parameters.AddWithValue("@sc", sc);
        cmd.Parameters.AddWithValue("@examcode", examcode);

        cmd.Parameters.AddWithValue("@codeSeries", series);
        cmd.Parameters.AddWithValue("@year", year);

        DataTable dt = new DataTable();
        dt = DL.GetDataTable(cmd);
        rpt.DataSource = dt;
        rpt.DataBind();

    }


    public void Bind_DropDown(DropDownList ddl,string value,string text,DataTable dt)
    {
        ddl.DataSource = dt;
        ddl.DataValueField = value;
        ddl.DataTextField = text;
        ddl.DataBind();
    }

    public DataTable FilterDataTable(DataTable originalDataTable, string condition)
    {
       
        DataView dataView = new DataView(originalDataTable);
        dataView.RowFilter = condition;

     
        DataTable filteredDataTable = dataView.ToTable();

        return filteredDataTable;
    }




    public  DataTable GetDistinctValues(DataTable originalDataTable, string columnName)
    {
        // Create a DataView to get distinct values
        DataView dataView = new DataView(originalDataTable);
        DataTable distinctValuesDataTable = dataView.ToTable(true, columnName);

        return distinctValuesDataTable;
    }



    public   DataTable RandomizeRows(DataTable sourceDataTable)
    {
        Random random = new Random();
        DataTable randomizedDataTable = sourceDataTable.Clone();

        foreach (DataRow sourceRow in sourceDataTable.Rows)
        {
            int randomIndex = random.Next(randomizedDataTable.Rows.Count + 1);
            DataRow newRow = randomizedDataTable.NewRow();
            newRow.ItemArray = sourceRow.ItemArray;
            randomizedDataTable.Rows.InsertAt(newRow, randomIndex);
        }

        return randomizedDataTable;
    }

    public void SaveOLN_QuestionPaperSeries(_GCOLN_QuestionPaperSeries gc)
    {
        string qry = "INSERT INTO [OLN_QuestionPaperSeries] ([Academicyear],[Slno],[examcode],[ExamDate],[ExamTimingFrom],[ExamTimingTo],[SeriesCode],[Seriesquestion_code],[Orgquestion_code],[Negativemarks],[Marks],[question_code],[question],[option_a],[option_b],[option_c],[option_d],[answer],[solutions],[difficulty_level],[created_by],[created_date],[ic],[sc],[subjectcode],[chapter] )VALUES (@Academicyear,(select isnull(max(convert(int,[slno])),0)+1 from [OLN_QuestionPaperSeries]),@examcode,@ExamDate,@ExamTimingFrom,@ExamTimingTo,@SeriesCode,@Seriesquestion_code,@Orgquestion_code,@Negativemarks,@Marks,@question_code,@question,@option_a,@option_b,@option_c,@option_d,@answer,@solutions,@difficulty_level,@created_by,@created_date,@ic,@sc,@subjectcode,@chapter )";
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@Academicyear", gc.Academicyear);
      
        cmd.Parameters.AddWithValue("@examcode", gc.examcode);
        cmd.Parameters.AddWithValue("@ExamDate", gc.ExamDate);
        cmd.Parameters.AddWithValue("@ExamTimingFrom", gc.ExamTimingFrom);
        cmd.Parameters.AddWithValue("@ExamTimingTo", gc.ExamTimingTo);
        cmd.Parameters.AddWithValue("@SeriesCode", gc.SeriesCode);
        cmd.Parameters.AddWithValue("@Seriesquestion_code", gc.Seriesquestion_code);
        cmd.Parameters.AddWithValue("@Orgquestion_code", gc.Orgquestion_code);
        cmd.Parameters.AddWithValue("@Negativemarks", gc.Negativemarks);
        cmd.Parameters.AddWithValue("@Marks", gc.Marks);
        cmd.Parameters.AddWithValue("@question_code", gc.question_code);
        cmd.Parameters.AddWithValue("@question", gc.question);
        cmd.Parameters.AddWithValue("@option_a", gc.option_a);
        cmd.Parameters.AddWithValue("@option_b", gc.option_b);
        cmd.Parameters.AddWithValue("@option_c", gc.option_c);
        cmd.Parameters.AddWithValue("@option_d", gc.option_d);
        cmd.Parameters.AddWithValue("@answer", gc.answer);
        cmd.Parameters.AddWithValue("@solutions", gc.solutions);
        cmd.Parameters.AddWithValue("@difficulty_level", gc.difficulty_level);
        cmd.Parameters.AddWithValue("@created_by", gc.created_by);
        cmd.Parameters.AddWithValue("@created_date", gc.created_date);
        cmd.Parameters.AddWithValue("@ic", gc.ic);
        cmd.Parameters.AddWithValue("@sc", gc.sc);
        cmd.Parameters.AddWithValue("@subjectcode", gc.subjectcode);
        cmd.Parameters.AddWithValue("@chapter", gc.chapter);
        cmd.CommandText = qry;
        DL.ExecuteCMD(cmd);
    }




    public void Save_Question_PaperSeries(List<DataTable> TableList,string seriescode)
    {
        int seriesQuestionCode = 1;

        for (int i=0;i< TableList.Count; i++)
        {
            
            for (int j = 0; j < TableList[i].Rows.Count; j++)
            {
                _GCOLN_QuestionPaperSeries Series_Object = new _GCOLN_QuestionPaperSeries();
                Series_Object.Academicyear = TableList[i].Rows[j]["Academicyear"].ToString();
             
                Series_Object.examcode = TableList[i].Rows[j]["examcode"].ToString();
                if(TableList[i].Rows[j]["ExamDate"] != DBNull.Value)
                {
                    Series_Object.ExamDate = Convert.ToDateTime(TableList[i].Rows[j]["ExamDate"].ToString());

                }

                if (TableList[i].Rows[j]["ExamTimingFrom"] != DBNull.Value)
                {
                    Series_Object.ExamTimingFrom = Convert.ToDateTime(TableList[i].Rows[j]["ExamTimingFrom"].ToString());

                }

                if (TableList[i].Rows[j]["ExamTimingTo"] != DBNull.Value)
                {
                    Series_Object.ExamTimingTo = Convert.ToDateTime(TableList[i].Rows[j]["ExamTimingTo"].ToString());

                }

                if (TableList[i].Rows[j]["created_date"] != DBNull.Value)
                {
                    Series_Object.created_date = Convert.ToDateTime(TableList[i].Rows[j]["created_date"].ToString());

                }


                Series_Object.SeriesCode = seriescode;
                Series_Object.Seriesquestion_code = seriesQuestionCode.ToString();
                Series_Object.Orgquestion_code = TableList[i].Rows[j]["question_slno"].ToString();
                Series_Object.Negativemarks = TableList[i].Rows[j]["Negativemarks"].ToString();
                Series_Object.Marks = TableList[i].Rows[j]["Marks"].ToString();
                Series_Object.question_code = TableList[i].Rows[j]["question_slno"].ToString();
                Series_Object.question = TableList[i].Rows[j]["question"].ToString();
                Series_Object.option_a = TableList[i].Rows[j]["option_a"].ToString();
                Series_Object.option_b = TableList[i].Rows[j]["option_b"].ToString();
                Series_Object.option_c = TableList[i].Rows[j]["option_c"].ToString();
                Series_Object.option_d = TableList[i].Rows[j]["option_d"].ToString();
                Series_Object.answer = TableList[i].Rows[j]["answer"].ToString();
                Series_Object.solutions = TableList[i].Rows[j]["solutions"].ToString();
                Series_Object.difficulty_level = TableList[i].Rows[j]["difficulty_level"].ToString();

                Series_Object.created_by = TableList[i].Rows[j]["created_by"].ToString();
                Series_Object.ic = TableList[i].Rows[j]["ic"].ToString();
                Series_Object.sc = TableList[i].Rows[j]["sc"].ToString();
                Series_Object.subjectcode = TableList[i].Rows[j]["subjectcode"].ToString();
                Series_Object.chapter = TableList[i].Rows[j]["chapter"].ToString();
                SaveOLN_QuestionPaperSeries(Series_Object);
                seriesQuestionCode++;
            }
           
        }
    }


    public void DeleteExistingSeries(string examcode,string year,string ic,string sc)
    {
        string query = "   DELETE FROM OLN_QuestionPaperSeries WHERE sc=@sc and ic=@ic and  examcode=@examcode and Academicyear=@year  ";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddWithValue("@examcode", examcode);
        cmd.Parameters.AddWithValue("@year", year);
        cmd.Parameters.AddWithValue("@ic", ic);
        cmd.Parameters.AddWithValue("@sc", sc);
        DL.ExecuteCMD(cmd);

    }


    public string ReturnString(string qry)
    {

        string result = "";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = qry;
        SqlDataReader dtrData = (SqlDataReader)(DL.GetReader(cmd));
        if (dtrData.Read())
        {
            if (!string.IsNullOrEmpty(dtrData[0].ToString()))
            {
                result = dtrData[0].ToString();
            }
        }
        return result;
    }


    public string GetOLN_QuestionPaperSeriesPrimaryCode()
{
 string result = "";
string qry = "select isnull(max(convert(int,[SeriesCode])),0)+1 from [OLN_QuestionPaperSeries]";
 SqlCommand cmd = new SqlCommand();
  cmd.CommandText = qry;
SqlDataReader dtrData = (SqlDataReader)(DL.GetReader(cmd));
if (dtrData.Read())
{
result = dtrData[""].ToString();
}
return result;
}
public void DeleteOLN_QuestionPaperSeriesItem(_GCOLN_QuestionPaperSeries gc)
{
string query ="DELETE FROM [OLN_QuestionPaperSeries] WHERE [SeriesCode]=@SeriesCode;";
 SqlCommand cmd = new SqlCommand();
cmd.Parameters.AddWithValue("@SeriesCode", gc.SeriesCode);
cmd.CommandText = query;
 DL.ExecuteCMD(cmd);
}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.XPath;

/// <summary>
/// Summary description for _BLCheckin
/// </summary>
public class ExamBusinessLayer
{
    public ExamBusinessLayer()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    DataLayer DL = new DataLayer();

   

   
  
  


    // changes by kusahl
    DataTable RandomizeRows(DataTable sourceDataTable)
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


   
   
   
    public void Execute(string qry)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = qry;
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
    public DataSet ReturnDataSet(string qry)
    {


        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = qry;
        return (DL.ReturnDataSet(cmd));

    }
    
    public string GetSlno(DataTable dt)
    {
        string result = "";
        string Max_Number = dt.Rows[0]["MaxNo"].ToString();
        string ExistingNo = dt.Rows[0]["ExistingNo"].ToString();

        result = ExistingNo == "0" ? Max_Number : ExistingNo;

        return result;
    }



    //vishnu 1112024//

    public void select_Subjects(Repeater list, string SC, string IC, string examcode)
    {
        string qry = @"select distinct  OLN_Subjects.subjectcode, OLN_Subjects.name from OLN_QuestionPaperSeries INNER JOIN  dbo.OLN_Subjects ON dbo.OLN_QuestionPaperSeries.subjectcode = dbo.OLN_Subjects.subjectcode where OLN_QuestionPaperSeries.sc=@sc and OLN_QuestionPaperSeries.ic=@ic and OLN_QuestionPaperSeries.examcode=@examcode  ";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@sc", SC);
        cmd.Parameters.AddWithValue("@ic", IC);
        cmd.Parameters.AddWithValue("@examcode", examcode);
        cmd.CommandText = qry;
        dt = DL.GetDataTable(cmd);
        list.DataSource = dt;
        list.DataBind();

    }
    public void QuestionGenarate(Repeater list, string IC, string SC, string examcode, string subjectcode)
    {
        string qry = @"SELECT *FROM   dbo.OLN_QuestionPaperSeries INNER JOIN dbo.OLN_Subjects ON dbo.OLN_QuestionPaperSeries.subjectcode = dbo.OLN_Subjects.subjectcode AND dbo.OLN_QuestionPaperSeries.ic = dbo.OLN_Subjects.ic AND dbo.OLN_QuestionPaperSeries.sc = dbo.OLN_Subjects.sc where OLN_QuestionPaperSeries.ic=@ic and OLN_QuestionPaperSeries.sc=@sc and OLN_QuestionPaperSeries.examcode=@examcode and OLN_QuestionPaperSeries.subjectcode=@subjectcode and OLN_QuestionPaperSeries.SeriesCode='A' order by OLN_QuestionPaperSeries.Seriesquestion_code ";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@sc", SC);
        cmd.Parameters.AddWithValue("@ic", IC);
        cmd.Parameters.AddWithValue("@examcode", examcode);
        cmd.Parameters.AddWithValue("@subjectcode", subjectcode);
        cmd.CommandText = qry;
        dt = DL.GetDataTable(cmd);
        // dt= RandomizeRows(dt);
        list.DataSource = dt;
        list.DataBind();

    }
    public void QuestionGenarate_Examsheet(Repeater list, string IC, string SC, string examcode)
    {
        string qry = @"SELECT *FROM   dbo.OLN_QuestionPaperSeries INNER JOIN dbo.OLN_Subjects ON dbo.OLN_QuestionPaperSeries.subjectcode = dbo.OLN_Subjects.subjectcode AND dbo.OLN_QuestionPaperSeries.ic = dbo.OLN_Subjects.ic AND dbo.OLN_QuestionPaperSeries.sc = dbo.OLN_Subjects.sc where OLN_QuestionPaperSeries.ic=@ic and OLN_QuestionPaperSeries.sc=@sc and OLN_QuestionPaperSeries.examcode=@examcode  and OLN_QuestionPaperSeries.SeriesCode='A' order by OLN_QuestionPaperSeries.Seriesquestion_code ";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@sc", SC);
        cmd.Parameters.AddWithValue("@ic", IC);
        cmd.Parameters.AddWithValue("@examcode", examcode);       
        cmd.CommandText = qry;
        dt = DL.GetDataTable(cmd);
        // dt= RandomizeRows(dt);
        list.DataSource = dt;
        list.DataBind();

    }
    public void QuestionGenarate_Selected(Repeater list, string IC, string SC, string examcode, string subjectcode, string qpcode)
    {
       // string qry = @"SELECT *FROM   dbo.OLN_QuestionPaperSeries INNER JOIN dbo.OLN_Subjects ON dbo.OLN_QuestionPaperSeries.subjectcode = dbo.OLN_Subjects.subjectcode AND dbo.OLN_QuestionPaperSeries.ic = dbo.OLN_Subjects.ic AND dbo.OLN_QuestionPaperSeries.sc = dbo.OLN_Subjects.sc where OLN_QuestionPaperSeries.ic=@ic and OLN_QuestionPaperSeries.sc=@sc and OLN_QuestionPaperSeries.examcode=@examcode and OLN_QuestionPaperSeries.subjectcode=@subjectcode and OLN_QuestionPaperSeries.Seriesquestion_code=@qpcode and OLN_QuestionPaperSeries.SeriesCode='A' order by OLN_QuestionPaperSeries.Seriesquestion_code   ";
        string qry = @"SELECT *FROM   dbo.OLN_QuestionPaperSeries INNER JOIN dbo.OLN_Subjects ON dbo.OLN_QuestionPaperSeries.subjectcode = dbo.OLN_Subjects.subjectcode AND dbo.OLN_QuestionPaperSeries.ic = dbo.OLN_Subjects.ic AND dbo.OLN_QuestionPaperSeries.sc = dbo.OLN_Subjects.sc where OLN_QuestionPaperSeries.ic=@ic and OLN_QuestionPaperSeries.sc=@sc and OLN_QuestionPaperSeries.examcode=@examcode and OLN_QuestionPaperSeries.subjectcode=@subjectcode  and OLN_QuestionPaperSeries.SeriesCode='A' order by OLN_QuestionPaperSeries.Seriesquestion_code   ";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@sc", SC);
        cmd.Parameters.AddWithValue("@ic", IC);
        cmd.Parameters.AddWithValue("@examcode", examcode);
        cmd.Parameters.AddWithValue("@subjectcode", subjectcode);
        cmd.Parameters.AddWithValue("@qpcode", qpcode);
        cmd.CommandText = qry;
        dt = DL.GetDataTable(cmd);
       // dt= RandomizeRows(dt);
        list.DataSource = dt;
        list.DataBind();

    }


    public void InsertOLN_ExamMarksSheet(string Academicyear , string exam_code, string ExamDate , string ExamTimingFrom ,string ExamTimingTo ,string StartTimeTo, string Series_Code ,   string Student_IdNo ,  string CandidateName , string RollNo )  
             
            
    {


        string qry1 = "select * from OLN_ExamMarksSheet where Academicyear='" + Academicyear + "' and examcode='" + exam_code + "' and SeriesCode='" + Series_Code + "' and StudentIdNo='" + Student_IdNo + "'";
        string qry = "  insert into [OLN_ExamMarksSheet] (Academicyear,Slno,examcode ,ExamDate ,ExamTimingFrom ,ExamTimingTo ,StartTimeTo ,SeriesCode ,StudentIdNo ,CandidateName ,RollNo)";
       qry += " values(@Academicyear, (SELECT isnull(max(convert(int, Slno)), 0) + 1 from [OLN_ExamMarksSheet]), @examcode,convert(date, @ExamDate,105), @ExamTimingFrom, @ExamTimingTo, @StartTimeTo, @SeriesCode, @StudentIdNo, @CandidateName, @RollNo)  ";

       if (qry1 == "")
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Parameters.AddWithValue("@Academicyear", Academicyear);
           // cmd.Parameters.AddWithValue("@Slno", question);
           cmd.Parameters.AddWithValue("@examcode", exam_code);
           cmd.Parameters.AddWithValue("@ExamDate", ExamDate);
           cmd.Parameters.AddWithValue("@ExamTimingFrom", ExamTimingFrom);
           cmd.Parameters.AddWithValue("@ExamTimingTo", ExamTimingTo);
           cmd.Parameters.AddWithValue("@StartTimeTo", StartTimeTo);
           cmd.Parameters.AddWithValue("@SeriesCode", Series_Code);
           cmd.Parameters.AddWithValue("@StudentIdNo", Student_IdNo);
           cmd.Parameters.AddWithValue("@CandidateName", CandidateName);
           cmd.Parameters.AddWithValue("@RollNo", RollNo);

           cmd.CommandText = qry;
           DL.ExecuteCMD(cmd);
       }
    }

}
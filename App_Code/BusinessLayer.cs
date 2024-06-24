using DocumentFormat.OpenXml.Bibliography;
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
public class BusinessLayer
{
    public BusinessLayer()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    DataLayer DL = new DataLayer();

   
    public void create_users(string id, string pwd, string username, string imagePath, string slno)
    {
        SqlCommand cmd = new SqlCommand();
        string qry = " delete from Login where userid='" + id + "' ; insert into [Login] with (rowlock) ( [userid]  , [password] ,[username],[usertype],[imagepath],[StaffSlno] ) values( '" + id + "', '" + pwd + "', '" + username + "','D','" + imagePath + "','" + slno + "')";
        cmd.Parameters.AddWithValue("@userid", id);
        cmd.CommandText = qry;
        DL.ExecuteCMD(cmd);

    }
    public void FillUsers(GridView list, string college)
    {
        string qry = "SELECT  [userid]  ,[username],[password] from [Login] where  usertype='D' order by userid";
        DataTable dt = new DataTable();
        dt = DL.GetDataTable(qry);
        list.DataSource = dt;
        list.DataBind();
    }
    public void select_Subjects(Repeater list, string SC, string IC,string examcode)
    {
        string qry = @"select distinct Subjects.name,Subjects.code from OLN_QuestionPaper INNER JOIN  dbo.Subjects ON dbo.OLN_QuestionPaper.subjectcode = dbo.Subjects.subjectcode where OLN_QuestionPaper.sc=@sc and OLN_QuestionPaper.ic=@ic and examcode=@examcode  order by code ";
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
    public void ExamList(Repeater list, string SC, string IC,string year,string slno,string course)
    {
        string qry = @"select * from OLN_ExamMaster where SC=@sc and IC=@ic and exam_code in (  SELECT examcode from OLN_STUDENTMAPPING where ic=@ic and sc=@sc and academicyear=@year and course=@course and SlNo=@slno) order by ExamDate desc";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@sc", SC);
        cmd.Parameters.AddWithValue("@ic", IC);
        cmd.Parameters.AddWithValue("@year", year);
        cmd.Parameters.AddWithValue("@slno", slno);
        cmd.Parameters.AddWithValue("@course", course);

        cmd.CommandText = qry;
        dt = DL.GetDataTable(cmd);
        list.DataSource = dt;
        list.DataBind();

    }
    public void ExamList(Repeater list, string SC, string IC)
    {
        string qry = @"select * from OLN_ExamMaster where SC=@sc and IC=@ic   order by ExamDate desc";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@sc", SC);
        cmd.Parameters.AddWithValue("@ic", IC);

        cmd.CommandText = qry;
        dt = DL.GetDataTable(cmd);
        list.DataSource = dt;
        list.DataBind();

    }
    public void Select_Exam(Repeater list, string SC, string IC,string examcode)
    {
        string qry = @"select * from OLN_ExamMaster where SC=@sc and IC=@ic and exam_code=@exam_code";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@sc", SC);
        cmd.Parameters.AddWithValue("@ic", IC);
        cmd.Parameters.AddWithValue("@exam_code", examcode);
        cmd.CommandText = qry;
        dt = DL.GetDataTable(cmd);
        list.DataSource = dt;
        list.DataBind();

    }
    public void que_ExamList(CheckBoxList list, string SC, string IC)
    {
        string qry = "select exam_code,exam_name  from OLN_ExamMaster where SC=@sc and IC=@ic order by exam_code ";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@sc", SC);
        cmd.Parameters.AddWithValue("@ic", IC);
        cmd.CommandText = qry;
        dt = DL.GetDataTable(cmd);
        list.DataSource = dt;
        list.DataTextField = "exam_name";
        list.DataValueField = "exam_code";
        list.DataBind();
    }
    public void CreateExam(string IC, string SC, string exam_code, string exam_name, string duration, string negative_marks, string no_of_qustions, string Exam_Instraction,string createddate,string createdby,DateTime examDate,DateTime examTimeFrom,DateTime examTimeTo,string seriesCount)
    {
        int a;
        string qry = "delete from OLN_ExamMaster where IC=@ic and SC=@sc and exam_code=@exam_code  ";

        qry += "  insert into [OLN_ExamMaster] (exam_code,exam_name,duration,negative_marks,no_of_qustions,Exam_instractions,SC,IC,created_date,created_by,ExamDate,ExamTimingFrom,ExamTimingTo,SeriesCount)";
        qry += " values( @exam_code,@exam_name,@duration,@negative_marks,@no_of_qustions,@Exam_instractions,@SC,@IC,convert(date,@created_date,105),@created_by,@ExamDate,@ExamTimingFrom,@ExamTimingTo,@SeriesCount)  ";

        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@ic", IC);
        cmd.Parameters.AddWithValue("@sc", SC);
        cmd.Parameters.AddWithValue("@exam_code", exam_code);
        cmd.Parameters.AddWithValue("@exam_name", exam_name);
        cmd.Parameters.AddWithValue("@duration", duration);
        cmd.Parameters.AddWithValue("@negative_marks", negative_marks);
        cmd.Parameters.AddWithValue("@no_of_qustions", no_of_qustions);
        cmd.Parameters.AddWithValue("@Exam_instractions", Exam_Instraction);
        
        cmd.Parameters.AddWithValue("@created_date", createddate);
        cmd.Parameters.AddWithValue("@created_by", createdby);
        cmd.Parameters.AddWithValue("@ExamDate", examDate);
        cmd.Parameters.AddWithValue("@ExamTimingFrom", examTimeFrom);
        cmd.Parameters.AddWithValue("@ExamTimingTo", examTimeTo);
        cmd.Parameters.AddWithValue("@seriesCount", seriesCount);



        cmd.CommandText = qry;
        DL.ExecuteCMD(cmd);
    }
    public void CreateQuestions(string ic, string question, string question_slno, string sc, string option_a, string option_b, string option_c, string option_d, string answer, string solutions, string subjectcode, string examcode, string chapter, string difficulty_level,string negmarks,string marks, string created_date, string created_by,string year,string ExamDate,string ExamTimingFrom,string ExamTimingTo,string question_code,string slno)
    {

        string qry = "delete from OLN_QuestionPaper where IC=@ic and SC=@sc and question_slno=@question_slno and examcode=@examcode";

        qry += "  insert into [OLN_QuestionPaper] (question_slno,question,option_a,option_b,option_c,option_d,answer ,solutions,subjectcode,examcode,chapter,difficulty_level,Negativemarks,marks,created_by,created_date,ic,sc,Academicyear,ExamDate,ExamTimingFrom,ExamTimingTo,question_code,slno)";
        qry += " values( @question_slno,@question,@option_a,@option_b,@option_c,@option_d,@answer ,@solutions,@subjectcode,@examcode,@chapter,@difficulty_level,@Negativemarks,@marks,@created_by,convert(date,@created_date,105),@ic,@sc,@year,@ExamDate,@ExamTimingFrom,@ExamTimingTo,@question_code,@slno)  ";

        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@ic", ic);
        cmd.Parameters.AddWithValue("@question", question);
        cmd.Parameters.AddWithValue("@question_slno", question_slno);
        cmd.Parameters.AddWithValue("@sc", sc);
        cmd.Parameters.AddWithValue("@option_a", option_a);
        cmd.Parameters.AddWithValue("@option_b", option_b);
        cmd.Parameters.AddWithValue("@option_c", option_c);
        cmd.Parameters.AddWithValue("@option_d", option_d);
        cmd.Parameters.AddWithValue("@answer", answer);
        cmd.Parameters.AddWithValue("@solutions", solutions);
        cmd.Parameters.AddWithValue("@subjectcode", subjectcode);
        cmd.Parameters.AddWithValue("@examcode", examcode);
        cmd.Parameters.AddWithValue("@chapter", chapter);
        cmd.Parameters.AddWithValue("@difficulty_level", difficulty_level);
        cmd.Parameters.AddWithValue("@Negativemarks", negmarks);
        cmd.Parameters.AddWithValue("@marks", marks);

        cmd.Parameters.AddWithValue("@created_date", created_date);
        cmd.Parameters.AddWithValue("@created_by", created_by);
        cmd.Parameters.AddWithValue("@year", year);
        cmd.Parameters.AddWithValue("@ExamDate", ExamDate);
        cmd.Parameters.AddWithValue("@ExamTimingFrom", ExamTimingFrom);
        cmd.Parameters.AddWithValue("@ExamTimingTo", ExamTimingTo);
        cmd.Parameters.AddWithValue("@question_code", question_code);
        cmd.Parameters.AddWithValue("@slno", slno);





        cmd.CommandText = qry;
        DL.ExecuteCMD(cmd);
    }
    public void questionlist(Repeater list, string SC, string IC)
    {
        string qry = @"select *,Subjects.name,question as questionname from OLN_QuestionPaper INNER JOIN  dbo.Subjects ON dbo.OLN_QuestionPaper.subjectcode = dbo.Subjects.subjectcode where OLN_QuestionPaper.sc=@sc and OLN_QuestionPaper.ic=@ic  order by  OLN_QuestionPaper.question_code asc";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@sc", SC);
        cmd.Parameters.AddWithValue("@ic", IC);
        cmd.CommandText = qry;
        dt = DL.GetDataTable(cmd);
       // dt= RandomizeRows(dt);
        list.DataSource = dt;
        list.DataBind();

    }

    public DataSet questionlistWithCount( string SC, string IC,string examcode,string subjectcode,int pageNum,int pagesize)
    {

       
        int offset = pagesize * (pageNum - 1);
        string qry = String.Format(@"select *,Subjects.name,CONCAT('Q No.',question_code,question) as questionname from OLN_QuestionPaper INNER JOIN  dbo.Subjects ON dbo.OLN_QuestionPaper.subjectcode = dbo.Subjects.subjectcode where OLN_QuestionPaper.sc=@sc and OLN_QuestionPaper.ic=@ic order by  OLN_QuestionPaper.question_code offset {0} rows fetch next {1} rows only;", offset,pagesize);

        qry += "select count(*) from OLN_QuestionPaper INNER JOIN  dbo.Subjects ON dbo.OLN_QuestionPaper.subjectcode = dbo.Subjects.subjectcode where OLN_QuestionPaper.sc=@sc and OLN_QuestionPaper.ic=@ic";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@sc", SC);
        cmd.Parameters.AddWithValue("@ic", IC);
        cmd.CommandText = qry;
        DataSet ds = new DataSet();
        ds = DL.ReturnDataSet(cmd);


       
        return ds;

    }
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


    public void Fill_Subjectlist(DropDownList list, string SC, string IC)
    {
        string qry = "select subjectcode,name  from Subjects where sc=@sc and ic=@ic";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@sc", SC);
        cmd.Parameters.AddWithValue("@ic", IC);
        cmd.CommandText = qry;
        dt = DL.GetDataTable(cmd);
        list.DataSource = dt;
        list.DataTextField = "name";
        list.DataValueField = "subjectcode";
        list.DataBind();
    }
    public string CreateQuestionsMaxValue()
    {
        string qry = "SELECT isnull(max(convert(int, question_slno)), 0) + 1 from [OLN_QuestionPaper]";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = qry;
        SqlDataReader dtrData = (SqlDataReader)(DL.GetReader(cmd));
        if (dtrData.Read())
        {
            if (!string.IsNullOrEmpty(dtrData[0].ToString()))
            {
                qry = dtrData[""].ToString();
            }
        }
        return qry;
    }
    public string CreateExamMaxValue()
    {
        string qry = "SELECT isnull(max(convert(int, exam_code)), 0) + 1 from [OLN_ExamMaster]";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = qry;
        SqlDataReader dtrData = (SqlDataReader)(DL.GetReader(cmd));
        if (dtrData.Read())
        {
            if (!string.IsNullOrEmpty(dtrData[0].ToString()))
            {
                qry = dtrData[""].ToString();
            }
        }
        return qry;
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

    public DataSet GetMaxSlno(string year,string exam,string subject)
    {
        string query = "SELECT (select isnull(max(convert(int, slno)), 0)+1 from OLN_QuestionPaper where Academicyear=@year) as MaxNo,isnull(max(convert(int, slno)), 0) as ExistingNo from [OLN_QuestionPaper] where examcode=@exam and Academicyear=@year;";

        query += " SELECT isnull(max(convert(int, question_code)), 0)+1  from [OLN_QuestionPaper] where examcode=@exam and Academicyear=@year and subjectcode=@subject;";


        query += "select * from OLN_ExamMaster where exam_code=@exam";

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddWithValue("@year", year);
        cmd.Parameters.AddWithValue("@exam", exam);
        cmd.Parameters.AddWithValue("@subject", subject);
        DataSet ds = new DataSet();
        ds = DL.ReturnDataSet(cmd);
        return ds;

    }






}
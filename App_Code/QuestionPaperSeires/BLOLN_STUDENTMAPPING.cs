using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI.WebControls;
public class _BLOLN_STUDENTMAPPING 
{
 DataLayer  DL = new DataLayer();
public void SaveOLN_STUDENTMAPPING(_GCOLN_STUDENTMAPPING gc)
{
string qry= "DELETE FROM OLN_STUDENTMAPPING WHERE  ic=@ic and examcode=@examcode AND academicyear=@academicyear and slno=@SlNo and course=@course ;INSERT INTO [OLN_STUDENTMAPPING] ([SlNo],[ic],[sc],[academicyear],[course],[examcode],[StudentIdNo],[Auto_Slno],[term],[Division],[RollNo],[combination])VALUES (@SlNo,@ic,@sc,@academicyear,@course,@examcode,@StudentIdNo,(select isnull(max(convert(int,[Auto_Slno])),0)+1 from [OLN_STUDENTMAPPING]),@term,@Division,@RollNo,@combination)";
 SqlCommand cmd = new SqlCommand();
cmd.Parameters.AddWithValue("@SlNo", gc.SlNo);
cmd.Parameters.AddWithValue("@ic", gc.ic);
cmd.Parameters.AddWithValue("@sc", gc.sc);
cmd.Parameters.AddWithValue("@academicyear", gc.academicyear);
cmd.Parameters.AddWithValue("@course", gc.course);
cmd.Parameters.AddWithValue("@examcode", gc.examcode);
cmd.Parameters.AddWithValue("@StudentIdNo", gc.StudentIdNo);

cmd.Parameters.AddWithValue("@term", gc.term);
cmd.Parameters.AddWithValue("@Division", gc.Division);
cmd.Parameters.AddWithValue("@RollNo", gc.RollNo);

        cmd.Parameters.AddWithValue("@combination", gc.combination);
 cmd.CommandText = qry;
  DL.ExecuteCMD(cmd);
}













public void GetOLN_STUDENTMAPPING(_GCOLN_STUDENTMAPPING gc)
{
string qry="SELECT * FROM [OLN_STUDENTMAPPING] WHERE [Auto_Slno]=@Auto_Slno";
 SqlCommand cmd = new SqlCommand();
cmd.Parameters.AddWithValue("@Auto_Slno", gc.Auto_Slno);
 cmd.CommandText = qry;
SqlDataReader dtrData = (SqlDataReader) (DL.GetReader(cmd));
if (dtrData.Read())
{
gc.SlNo= dtrData["SlNo"].ToString();
gc.ic=dtrData["ic"].ToString();
gc.sc=dtrData["sc"].ToString();
gc.academicyear=dtrData["academicyear"].ToString();
gc.course=dtrData["course"].ToString();
gc.examcode=dtrData["examcode"].ToString();
gc.StudentIdNo=dtrData["StudentIdNo"].ToString();
gc.Auto_Slno=dtrData["Auto_Slno"].ToString();
gc.term=dtrData["term"].ToString();
gc.Division=dtrData["Division"].ToString();
gc.RollNo= dtrData["RollNo"].ToString();

}
}













public void RptOLN_STUDENTMAPPINGAdapter(Repeater list)
{
 	string qry = @"SELECT * from [OLN_STUDENTMAPPING] order by Auto_Slno ";
	DataTable dt = new DataTable();
	SqlCommand cmd = new SqlCommand();
	cmd.CommandText = qry;
	dt = DL.GetDataTable(cmd);
	list.DataSource = dt;
 	list.DataBind();
}












    public void Fill_Exam_DropDown(DropDownList ddl, string ic, string sc)
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

    public void Fill_AcademicYear_DropDown(DropDownList ddl, string ic, string sc)
    {
        string query = " SELECT code from ACADEMIC_YEAR order by code desc";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;


        DataTable dt = new DataTable();
        dt = DL.GetDataTable(cmd);
        ddl.DataSource = dt;
        ddl.DataValueField = "code";
        ddl.DataTextField = "code";
        ddl.DataBind();
    }



    public void Fill_Course_DropDown(DropDownList ddl, string ic)
    {
        string query = "  SELECT code,coursename from CourseMaster where InstituteCode=@ic order by code desc";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddWithValue("@ic", ic);


        DataTable dt = new DataTable();
        dt = DL.GetDataTable(cmd);
        ddl.DataSource = dt;
        ddl.DataValueField = "code";
        ddl.DataTextField = "coursename";
        ddl.DataBind();
    }

    public void Fill_Division_DropDown(DropDownList ddl, string ic,string year,string course,bool add_all_placeholder)
    {
        string query = " select Distinct Division from studentAdmissionRegister where InstituteCode=@ic and academicyear=@year and course=@course order by Division";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddWithValue("@ic", ic);
        cmd.Parameters.AddWithValue("@year", year);
        cmd.Parameters.AddWithValue("@course", course);


        DataTable dt = new DataTable();
        dt = DL.GetDataTable(cmd);
        ddl.DataSource = dt;
        ddl.DataValueField = "Division";
        ddl.DataTextField = "Division";
        ddl.DataBind();
        if (add_all_placeholder)
        {
            ddl.Items.Insert(0, new ListItem("ALL", "-1"));
        }


    }


    public void Fill_SubjectCombination_DropDown(DropDownList ddl, string ic, string year, string course,bool add_all_as_placeholder)
    {
        string query = " select Distinct combination from studentAdmissionRegister where InstituteCode=@ic and academicyear=@year and course=@course order by combination";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddWithValue("@ic", ic);
        cmd.Parameters.AddWithValue("@year", year);
        cmd.Parameters.AddWithValue("@course", course);


        DataTable dt = new DataTable();
        dt = DL.GetDataTable(cmd);
        ddl.DataSource = dt;
        ddl.DataValueField = "combination";
        ddl.DataTextField = "combination";
        ddl.DataBind();

        if (add_all_as_placeholder)
        {
            ddl.Items.Insert(0, new ListItem("ALL", "-1"));
        }
    }



    public void Get_StudentListSubjectCombinationWise(Repeater rpt,string ic,string year,string course,string division,string subjectcombination)
    {
        string divisionFilter = "";
        string subjectCombinationFilter = "";
        if(division != "-1")
        {
            divisionFilter = String.Format("and StudentAdmissionRegister.Division=@div");
        }

        if(subjectcombination != "-1")
        {
            subjectCombinationFilter = String.Format("and StudentAdmissionRegister.combination=@combination");

        }


        string query = String.Format(@"

            SELECT StudentRegister.ic, StudentAdmissionRegister.StudentIdNo, StudentAdmissionRegister.SlNo, StudentAdmissionRegister.academicyear, StudentAdmissionRegister.term, StudentAdmissionRegister.course, StudentAdmissionRegister.CandidateName, StudentAdmissionRegister.Division, StudentAdmissionRegister.RollNo, StudentAdmissionRegister.combination
            FROM  StudentRegister INNER JOIN
            StudentAdmissionRegister ON StudentRegister.ic = StudentAdmissionRegister.InstituteCode AND StudentRegister.SlNo = StudentAdmissionRegister.SlNo
		    where StudentAdmissionRegister.InstituteCode=@ic and StudentAdmissionRegister.academicyear=@year and StudentAdmissionRegister.course=@course and StudentAdmissionRegister.status='A'  {0} {1}
		    order by StudentIdNo

                ", divisionFilter, subjectCombinationFilter);

            
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddWithValue("@ic", ic);
        cmd.Parameters.AddWithValue("@year", year);
        cmd.Parameters.AddWithValue("@course", course);
        cmd.Parameters.AddWithValue("@div", division);
        cmd.Parameters.AddWithValue("@combination", subjectcombination);

        DataTable dt = DL.GetDataTable(cmd);
        rpt.DataSource = dt;
        rpt.DataBind();





    }

  




    public string GetOLN_STUDENTMAPPINGPrimaryCode()
{
 string result = "";
string qry = "select isnull(max(convert(int,[Auto_Slno])),0)+1 from [OLN_STUDENTMAPPING]";
 SqlCommand cmd = new SqlCommand();
  cmd.CommandText = qry;
SqlDataReader dtrData = (SqlDataReader)(DL.GetReader(cmd));
if (dtrData.Read())
{
result = dtrData[""].ToString();
}
return result;
}
public void DeleteOLN_STUDENTMAPPINGItem(_GCOLN_STUDENTMAPPING gc)
{
string query ="DELETE FROM [OLN_STUDENTMAPPING] WHERE [Auto_Slno]=@Auto_Slno;";
 SqlCommand cmd = new SqlCommand();
cmd.Parameters.AddWithValue("@Auto_Slno", gc.Auto_Slno);
cmd.CommandText = query;
 DL.ExecuteCMD(cmd);
}
}

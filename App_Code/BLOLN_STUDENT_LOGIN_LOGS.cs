using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.DynamicData;

public class _BLOLN_STUDENT_LOGIN_LOGS 
{
 DataLayer  DL = new DataLayer();
public _GCOLN_STUDENT_LOGIN_LOGS SaveOLN_STUDENT_LOGIN_LOGS(_GCOLN_STUDENT_LOGIN_LOGS gc)
{

		DataTable ExistingLogs = new DataTable();
		ExistingLogs = Get_Existing_Logs(gc.ic, gc.Slno, gc.Exam_Code, gc.course, gc.academicyear);
		
		 if(ExistingLogs.Rows.Count > 0)
		{
			gc.SeriesCode = ExistingLogs.Rows[0]["SeriesCode"].ToString();
		}

            string qry = "INSERT INTO [OLN_STUDENT_LOGIN_LOGS] ([AutoSlno],[Slno],[ic],[sc],[StudentIdNo],[Exam_Code],[SeriesCode],[LoginTime],[academicyear],[course] )VALUES ((select isnull(max(convert(int,[AutoSlno])),0)+1 from [OLN_STUDENT_LOGIN_LOGS]),@Slno,@ic,@sc,@StudentIdNo,@Exam_Code,@SeriesCode,@LoginTime,@academicyear,@course )";
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.AddWithValue("@Slno", gc.Slno);
            cmd.Parameters.AddWithValue("@ic", gc.ic);
            cmd.Parameters.AddWithValue("@sc", gc.sc);
            cmd.Parameters.AddWithValue("@StudentIdNo", gc.StudentIdNo);
            cmd.Parameters.AddWithValue("@Exam_Code", gc.Exam_Code);
            cmd.Parameters.AddWithValue("@SeriesCode", gc.SeriesCode);
            cmd.Parameters.AddWithValue("@LoginTime", gc.LoginTime);
            cmd.Parameters.AddWithValue("@academicyear", gc.academicyear);
            cmd.Parameters.AddWithValue("@course", gc.course);
            cmd.CommandText = qry;
            DL.ExecuteCMD(cmd);
        



		return gc;
}




	DataTable Get_Existing_Logs(string ic,string slno,string exam_code,string course,string year)
	{
		DataTable dt = new DataTable();
		string query = String.Format("SELECT * FROM OLN_STUDENT_LOGIN_LOGS WHERE IC='{0}' and academicyear='{1}' and course='{2}' and exam_code='{3}' and slno='{4}' order by LoginTime desc",ic,year,course,exam_code,slno);
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = query;
		dt = DL.GetDataTable(cmd);

		return dt;
	}








public void GetOLN_STUDENT_LOGIN_LOGS(_GCOLN_STUDENT_LOGIN_LOGS gc)
{
string qry="SELECT * FROM [OLN_STUDENT_LOGIN_LOGS] WHERE [AutoSlno]=@AutoSlno";
 SqlCommand cmd = new SqlCommand();
cmd.Parameters.AddWithValue("@AutoSlno", gc.AutoSlno);
 cmd.CommandText = qry;
SqlDataReader dtrData = (SqlDataReader) (DL.GetReader(cmd));
if (dtrData.Read())
{
gc.AutoSlno=dtrData["AutoSlno"].ToString();
gc.Slno=dtrData["Slno"].ToString();
gc.ic=dtrData["ic"].ToString();
gc.sc=dtrData["sc"].ToString();
gc.StudentIdNo=dtrData["StudentIdNo"].ToString();
gc.Exam_Code=dtrData["Exam_Code"].ToString();
gc.SeriesCode=dtrData["SeriesCode"].ToString();
gc.LoginTime= Convert.ToDateTime(dtrData["LoginTime"]);
gc.academicyear=dtrData["academicyear"].ToString();
gc.course=dtrData["course"].ToString();
}
}













public void RptOLN_STUDENT_LOGIN_LOGSAdapter(Repeater list)
{
 	string qry = @"SELECT * from [OLN_STUDENT_LOGIN_LOGS] order by AutoSlno ";
	DataTable dt = new DataTable();
	SqlCommand cmd = new SqlCommand();
	cmd.CommandText = qry;
	dt = DL.GetDataTable(cmd);
	list.DataSource = dt;
 	list.DataBind();
}















                  



                        
public string GetOLN_STUDENT_LOGIN_LOGSPrimaryCode()
{
 string result = "";
string qry = "select isnull(max(convert(int,[AutoSlno])),0)+1 from [OLN_STUDENT_LOGIN_LOGS]";
 SqlCommand cmd = new SqlCommand();
  cmd.CommandText = qry;
SqlDataReader dtrData = (SqlDataReader)(DL.GetReader(cmd));
if (dtrData.Read())
{
result = dtrData[""].ToString();
}
return result;
}
public void DeleteOLN_STUDENT_LOGIN_LOGSItem(_GCOLN_STUDENT_LOGIN_LOGS gc)
{
string query ="DELETE FROM [OLN_STUDENT_LOGIN_LOGS] WHERE [AutoSlno]=@AutoSlno;";
 SqlCommand cmd = new SqlCommand();
cmd.Parameters.AddWithValue("@AutoSlno", gc.AutoSlno);
cmd.CommandText = query;
 DL.ExecuteCMD(cmd);
}
}

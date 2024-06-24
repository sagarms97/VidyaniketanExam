using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Instraction : System.Web.UI.Page
{

    BusinessLayer BL = new BusinessLayer();
    string IC = "18";
    string SC = "2";

    Cookie_Manager Cookie_Master=new Cookie_Manager();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            if (!Page.IsPostBack)
            {
                string Examcode = GetQueryOrNull();
                if (String.IsNullOrEmpty(Examcode))
                {
                    btnstartExam.Enabled = false;
                    chkSelection.Enabled = false;
                    btnstartExam.Text = "PLEASE CHECK THE LINK OR RE-LOGIN";
                    throw new Exception("PLEASE CHECK THE LINK OR RE-LOGIN");
                }
                BL.Select_Exam(rep_Examinstruction, SC, IC, Examcode);
            }

        }catch(Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal()", true);
        }
      
    }


    public string GetQueryOrNull()
    {
        string result = "";
        try
        {
            result= Request.QueryString["Examcode"].ToString();

        }
        catch(Exception)
        {

        }
        return result;
    }
    protected void btnstartExam_Click(object sender, EventArgs e)
    {
        try
        {


            char[] alphabets = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            _BLOLN_STUDENT_LOGIN_LOGS Logs_Master = new _BLOLN_STUDENT_LOGIN_LOGS();
            _GCOLN_STUDENT_LOGIN_LOGS Logs_Data = new _GCOLN_STUDENT_LOGIN_LOGS();
            string Exam_Code = GetQueryOrNull();
            string ic = Cookie_Master.Get_Erp_IC();
            
            string num_of_series = BL.ReturnString(String.Format("select SeriesCount  from OLN_ExamMaster where ic='{0}'  and exam_code='{1}'",ic,Exam_Code));
            string ExamTimingFrom = BL.ReturnString(String.Format("select ExamTimingFrom  from OLN_ExamMaster where ic='{0}'  and exam_code='{1}'", ic, Exam_Code));
            string ExamTimingTo = BL.ReturnString(String.Format("select FORMAT(ExamTimingTo, 'dd-MM-yyyy hh:mm tt') as ExamTimingTo  from OLN_ExamMaster where ic='{0}'  and exam_code='{1}'", ic, Exam_Code));




           
            DateTime _ToDate = DateTime.ParseExact(ExamTimingTo,"dd-MM-yyyy hh:mm tt",CultureInfo.InvariantCulture);


            string date_value = _ToDate.ToString("dd-MM-yyyy hh:mm tt");
            if (String.IsNullOrEmpty(num_of_series)){


                throw new Exception("INVALID TEST SETTINGS PLEASE CONTACT ADMIN");
            }

            int SeriesCount = Convert.ToInt32(num_of_series);
            Random rand_index = new Random();
            int random_number=rand_index.Next(SeriesCount);


            Logs_Data.SeriesCode = alphabets[random_number].ToString();
            Logs_Data.ic = Cookie_Master.Get_Erp_IC();
            Logs_Data.LoginTime = DateTime.Now;
            Logs_Data.sc = SC;
            Logs_Data.StudentIdNo = Cookie_Master.Get_StudentIdno();
            Logs_Data.academicyear = Cookie_Master.Get_Year();
            Logs_Data.Exam_Code = Exam_Code;
            Logs_Data.course = Cookie_Master.Get_Erp_StudentCourse();
            Logs_Data.Slno = Cookie_Master.Get_Erp_Slno();




            Logs_Data= Logs_Master.SaveOLN_STUDENT_LOGIN_LOGS(Logs_Data);

            HttpCookie Erp_SeriesCode = new HttpCookie("Erp_SeriesCode");
            Erp_SeriesCode.Value = Logs_Data.SeriesCode;
            Erp_SeriesCode.Expires = DateTime.Now.AddHours(2);
            Response.SetCookie(Erp_SeriesCode);

            TimeSpan remaning_time = _ToDate - Logs_Data.LoginTime;
            double in_minutes = remaning_time.TotalMinutes;

         

            string remaining_minutes = _ToDate.Subtract(Logs_Data.LoginTime).TotalMinutes.ToString();
            HttpCookie Erp_Time = new HttpCookie("Erp_RemainingTime");
            Erp_Time.Value = remaining_minutes;
            Erp_Time.Expires = DateTime.Now.AddHours(2);
            Response.SetCookie(Erp_Time);



            Response.Redirect("StartExam.aspx",false);

        }
        catch(Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal()", true);

        }
      
    }

    protected void chkSelection_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            btnstartExam.Enabled= chkSelection.Checked;
            if (btnstartExam.Enabled == true)
            {
                btnstartExam.CssClass = "btn btn-default  btn btn-success";
            }
            else
            {
                btnstartExam.CssClass = "btn btn-default  btn btn-secondary";

            }

        }
        catch(Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal()", true);
        }
    }
}
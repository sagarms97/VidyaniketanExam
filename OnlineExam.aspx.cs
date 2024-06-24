using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OnlineExam : System.Web.UI.Page
{
    BusinessLayer BL = new BusinessLayer();
    Cookie_Manager Cookie_Master = new Cookie_Manager();

    string IC = "18";
    string SC = "2";

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {
                string studnentId = Cookie_Master.Get_StudentIdno();
                string instituteCode = Cookie_Master.Get_Erp_IC();
                string erp_slno = Cookie_Master.Get_Erp_Slno();
                string course = Cookie_Master.Get_Erp_StudentCourse();
                string year = Cookie_Master.Get_Year();

                if (string.IsNullOrEmpty(studnentId) || string.IsNullOrEmpty(instituteCode) || string.IsNullOrEmpty(erp_slno) || string.IsNullOrEmpty(course) || string.IsNullOrEmpty(year))
                {
                    Response.Redirect("~/Default.aspx", false);
                }
                else
                {
                    BL.ExamList(rep_Exams, SC, IC, year, erp_slno, course);
                    CheckAndDisableExams();
                    if(rep_Exams.Items.Count > 0)
                    {
                        lblNoRecords.Visible = false;
                    }
                    else
                    {
                        lblNoRecords.Visible = true;
                    }
                }




            }

        }
        catch(Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ErrorModal();", true);
        }

     
    }


    private void CheckAndDisableExams()
    {
        try
        {
            for (int i = 0; i < rep_Exams.Items.Count; i++)
            {
                LinkButton examButton = rep_Exams.Items[i].FindControl("btnSelect") as LinkButton;
                Label lblExamDate = rep_Exams.Items[i].FindControl("lblExamDate") as Label;
                Label lblExamTimeFrom = rep_Exams.Items[i].FindControl("lblExamTimeFrom") as Label;
                Label lblExamTimeTo = rep_Exams.Items[i].FindControl("lblExamTimeTo") as Label;

                Panel pnlItesm = rep_Exams.Items[i].FindControl("pnlExamData") as Panel;



                DateTime combinedFromDateTime = DateTime.ParseExact(lblExamTimeFrom.Text, "yyyy-MM-dd hh:mm tt", null);
                DateTime combinedToDateTime = DateTime.ParseExact(lblExamTimeTo.Text, "yyyy-MM-dd hh:mm tt", null);

                DateTime currentDateTime = DateTime.Now;
                if (combinedFromDateTime <= currentDateTime && currentDateTime <= combinedToDateTime)
                {
                    examButton.Enabled = true;
                }
                else
                {
                    examButton.Enabled = false;
                   
                    examButton.CssClass = "list-group-item bg-secondary text-white mt-2";
                  
                }





            }

        }
        catch(Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ErrorModal();", true);
        }
      
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        LinkButton lnkRemove = (LinkButton) sender;


        string code = lnkRemove.CommandArgument;
        Response.Redirect("Instraction.aspx?Examcode=" + code + "");
    }
}
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_StudentExamMapping : System.Web.UI.Page
{

    _BLOLN_STUDENTMAPPING MappingMaster = new _BLOLN_STUDENTMAPPING();

    string IC = "18";
    string SC = "2";

    Cookie_Manager CookieMaster = new Cookie_Manager();


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {


              

                MappingMaster.Fill_AcademicYear_DropDown(ddlYear, IC, SC);
                MappingMaster.Fill_Exam_DropDown(ddlExam, IC, SC);
                MappingMaster.Fill_Course_DropDown(ddlCourse, IC);
                MappingMaster.Fill_Division_DropDown(ddlDivision, IC, ddlYear.SelectedValue, ddlCourse.SelectedValue,true);
                MappingMaster.Fill_SubjectCombination_DropDown(ddlCombination, IC, ddlYear.SelectedValue, ddlCourse.SelectedValue, true);
                
            }
        }
        catch(Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal();", true);
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            MappingMaster.Fill_Division_DropDown(ddlDivision, IC, ddlYear.SelectedValue, ddlCourse.SelectedValue,true);
            MappingMaster.Fill_SubjectCombination_DropDown(ddlCombination, IC, ddlYear.SelectedValue, ddlCourse.SelectedValue,true);


        }
        catch (Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal();", true);
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            MappingMaster.Get_StudentListSubjectCombinationWise(RptStudentList,IC,ddlYear.SelectedValue,ddlCourse.SelectedValue,ddlDivision.SelectedValue,ddlCombination.SelectedValue) ;




        }
        catch(Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal();", true);
        }
    }

   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int selected_student_count = 0;
            if (RptStudentList.Items.Count == 0)
            {
                throw new Exception("PLEASE SELECT STUDENTS");
            }





            for(int i = 0; i < RptStudentList.Items.Count; i++)
            {
                CheckBox chkSelection = RptStudentList.Items[i].FindControl("chkSelect") as CheckBox;
                if (chkSelection.Checked)
                {
                    selected_student_count++;
                    Label lblSlno = RptStudentList.Items[i].FindControl("lblSlno") as Label;
                    Label lblStudentIdNo = RptStudentList.Items[i].FindControl("lblStudentIdNo") as Label;
                    Label lblRollNo = RptStudentList.Items[i].FindControl("lblRollNo") as Label;
                    Label lblDivision = RptStudentList.Items[i].FindControl("lblDivision") as Label;
                    Label lblacademicyear = RptStudentList.Items[i].FindControl("lblacademicyear") as Label;
                    Label lblterm = RptStudentList.Items[i].FindControl("lblterm") as Label;
                    Label lblCombination = RptStudentList.Items[i].FindControl("lblCombination") as Label;
                    _GCOLN_STUDENTMAPPING GC = new _GCOLN_STUDENTMAPPING();
                    GC.sc = SC;
                    GC.SlNo = lblSlno.Text;
                    GC.ic = IC;
                    GC.academicyear = lblacademicyear.Text;
                    GC.course = ddlCourse.SelectedValue;
                    GC.examcode = ddlExam.SelectedValue;
                    GC.StudentIdNo = lblStudentIdNo.Text;
                    GC.term = lblterm.Text;
                    GC.Division = lblDivision.Text;
                    GC.RollNo = lblRollNo.Text;
                    GC.combination = lblCombination.Text;
                    MappingMaster.SaveOLN_STUDENTMAPPING(GC);

                }

            }


            if (selected_student_count == 0)
            {
                throw new Exception("PLEASE SELECT STUDENTS");

            }


            lblErrorMsg.Text = "SUCCESS : " + "STUDENTS MAPPED SUCCESSFULLY";
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal();", true);

            RptStudentList.Controls.Clear();


        }
        catch(Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal();", true);
        }
    }
}
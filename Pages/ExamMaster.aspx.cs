using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Pages_ExamMaster : System.Web.UI.Page
{
    BusinessLayer BL = new BusinessLayer();
    DataLayer DL = new DataLayer();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            try
            {
                MultiView1.SetActiveView(ViewList);
                BL.ExamList(Rep_List, "2", "18");

            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = "Error : " + ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal()", true);
            }
          
            //examLogic.ExamList(Rep_List, "2", "1");
            //lblslno.Text = examLogic.CreateExamMaxValue();
          
        }
    }
    protected void btnViewNew_Click(object sender, EventArgs e)
    {
        MultiView1.SetActiveView(ViewCreate);
    }
    public string Erpuserid()
    {
        try
        {
            HttpCookie Erpuserid = HttpContext.Current.Request.Cookies["Erpuserid"];
            return HttpContext.Current.Server.HtmlEncode(Erpuserid.Value);
        }
        catch (Exception)
        {
            return "";
        }
    }
    protected void btnViewList_Click(object sender, EventArgs e)
    {
        MultiView1.SetActiveView(ViewList);
    }
    protected void select_Click(object sender, EventArgs e)
    {
        MultiView1.SetActiveView(ViewCreate);
        LinkButton lnkRemove = (LinkButton)sender;
        string code = lnkRemove.CommandArgument;

        string qry = "select exam_code,exam_name,duration,negative_marks,no_of_qustions,Exam_instractions,SC,IC,ExamDate,ExamTimingFrom,ExamTimingTo,SeriesCount from OLN_ExamMaster where SC='2' and IC='18' and exam_code='" + code + "'";
        DataTable dt = new DataTable();
        dt = DL.GetDataTable(qry);
        lblExamcode.Text = lblslno.Text = dt.Rows[0][0].ToString();
        txtexamname.Text = dt.Rows[0][1].ToString();
        txtduration.Text = dt.Rows[0][2].ToString();
        ListItem itmSelected15 = ddlnegativemarks.Items.FindByValue(dt.Rows[0][3].ToString());
        if (itmSelected15 != null)
        {
            ddlnegativemarks.ClearSelection();
            itmSelected15.Selected = true;
        }

        ddlnegativemarks.SelectedItem.Selected = false;
        txtnoofquestion.Text = dt.Rows[0][4].ToString();
        txtexaminstruction.Text = dt.Rows[0][5].ToString();
        if (dt.Rows[0][8] != DBNull.Value)
        {
            txt_ExamDate.Text = Convert.ToDateTime(dt.Rows[0][8]).ToString("dd-MM-yyyy");
        }
        if (dt.Rows[0][9] != DBNull.Value)
        {
            txt_FromTime.Text = Convert.ToDateTime(dt.Rows[0][9]).ToString("hh:mm tt");
        }

        if (dt.Rows[0][10] != DBNull.Value)
        {
            txt_ToTime.Text = Convert.ToDateTime(dt.Rows[0][10]).ToString("hh:mm tt");
        }


        txt_series_no.Text = dt.Rows[0]["SeriesCount"].ToString();
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {

        try
        {
            if (lblslno.Text == "")
            {
                lblslno.Text = BL.CreateExamMaxValue();
            }
            lblExamcode.Text = lblslno.Text;


            DateTime examDate = DateTime.ParseExact(txt_ExamDate.Text, "dd-MM-yyyy", null);


            string _TimeFrom = txt_ExamDate.Text + " " + txt_FromTime.Text;
            string _TimeTo = txt_ExamDate.Text + " " + txt_ToTime.Text;
            DateTime examTimeFrom = DateTime.Parse(_TimeFrom);
            DateTime examTimeTo = DateTime.Parse(_TimeTo);
            BL.CreateExam("18", "2", lblExamcode.Text, txtexamname.Text, txtduration.Text, ddlnegativemarks.SelectedValue.ToString(), txtnoofquestion.Text, txtexaminstruction.Text, DateTime.Now.ToString(), Erpuserid(),examDate,examTimeFrom,examTimeTo,txt_series_no.Text);

           
            lblErrorMsg.Text = "SUCCESS : " + "NEW EXAM CREATED";
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal()", true);

            ClearControls();
        }
        catch(Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal()", true);
        }
      

      

    }

    private void ClearControls()
    {
        try
        {
            txtexamname.Text = "";
            txtnoofeasy.Text = "";
            txtnoofquestion.Text = "";
            txtnoofToughest.Text = "";
            txtnooftough.Text = "";
            txtexamname.Text = "";
            

        }catch(Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal()", true);
        }
    }

    protected void txt_ExamDate_TextChanged(object sender, EventArgs e)
    {
        try
        {

        }catch(Exception ex)
        {

        }
    }
}
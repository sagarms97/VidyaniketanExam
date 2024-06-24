using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_QuestionMaster : System.Web.UI.Page
{
    BusinessLayer BL = new BusinessLayer();
    DataLayer DL = new DataLayer();

    Cookie_Manager CookieManager=new Cookie_Manager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
             MultiView1.SetActiveView(ViewList);
            BL.Fill_Subjectlist(ddlsubject, "2", "18");
            BL.questionlist(Rep_List, "2", "18");
            BL.que_ExamList(chk_Examlist, "2", "18");
           
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
            HttpCookie Erpuserid = HttpContext.Current.Request.Cookies["Erpusername"];
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
        BL.questionlist(Rep_List, "2", "18");
    }


    private void ClearControls()
    {
        try
        {
            rbtn_questionlevel.ClearSelection();
            txtCkEditorQue.Text = "";
            CKEditorobtA.Text = "";
            CKEditorobtB.Text = "";
            CKEditorobtC.Text = "";
            CKEditorobtD.Text = "";
            txtsolution.Text = "";

            lblquationcode.Text = "";
            lblslno.Text = "";
        }
        catch(Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal()", true);
        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            bool isUpdate = false;
            string AcademicYear = CookieManager.Get_Year();

            if (lblslno.Text == "")
            {
                lblslno.Text = BL.CreateQuestionsMaxValue();
                
            }
            else
            {
                isUpdate = true;
            }

            int selectedExamCount = 0;

            for(int i = 0; i < chk_Examlist.Items.Count; i++)
            {
                if (chk_Examlist.Items[i].Selected == true)
                {
                    selectedExamCount++;
                }
            }


            if (selectedExamCount == 0)
            {
               throw new Exception("PLEASE SELECT EXAM");
                
            }


            lblquationcode.Text = lblslno.Text;

            int Code = Convert.ToInt32(lblquationcode.Text);
            for(int i = 0; i < chk_Examlist.Items.Count; i++)
            {
                if (chk_Examlist.Items[i].Selected == true)
                {
                    string slno = "";
                    string question_code = "";
                    string examDate = "";
                    string examTimingFrom = "";
                    string examTimingTo = "";
                    if (!isUpdate)
                    {
                        DataSet ds = BL.GetMaxSlno(AcademicYear, chk_Examlist.Items[i].Value, ddlsubject.SelectedValue);
                        slno = BL.GetSlno(ds.Tables[0]);
                        question_code = ds.Tables[1].Rows[0][0].ToString();
                        examDate = ds.Tables[2].Rows[0]["ExamDate"].ToString();
                        examTimingFrom = ds.Tables[2].Rows[0]["ExamTimingFrom"].ToString();
                        examTimingTo = ds.Tables[2].Rows[0]["ExamTimingTo"].ToString();
                    }
                    else
                    {

                        slno = lblExamSlno.Text;
                        question_code = lblqpCode.Text;
                        examDate = lblExamdate.Text;
                        examTimingFrom = lblexamTimingFrom.Text;
                        examTimingTo = lblexamTimingTo.Text;

                    }


                    BL.CreateQuestions("18", txtCkEditorQue.Text, Code.ToString(), "2", CKEditorobtA.Text, CKEditorobtB.Text, CKEditorobtC.Text, CKEditorobtD.Text, ddlAnswer.SelectedValue, txtsolution.Text, ddlsubject.SelectedValue.ToString(), chk_Examlist.Items[i].Value.ToString(), ddlchapter.SelectedValue.ToString(), rbtn_questionlevel.SelectedValue.ToString(), txtnegativemarks.Text, txtmarks.Text, DateTime.Now.ToString(), Erpuserid(),AcademicYear,examDate,examTimingFrom,examTimingTo,question_code,slno);
                    Code++;
                }
            }
           
            BL.questionlist(Rep_List, "2", "18");
            //BL.CreateQuestions("1", txtCkEditorQue.Text, lblquationcode.Text, "2", CKEditorobtA.Text, CKEditorobtB.Text, CKEditorobtC.Text, CKEditorobtD.Text, txtCKEditorAns.Text, txtsolution.Text, ddlsubject.SelectedValue.ToString(), chk_Examlist.SelectedValue.ToString(), ddlchapter.SelectedValue.ToString(), ddllevel.SelectedValue.ToString(), DateTime.Now.ToString(), "vishnu");
            lblErrorMsg.Text = "SUCCESS : " + "Question Created Successfully";
           
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal()", true);
            lblslno.Text = "";
            if (chk_Examlist.Enabled == false)
            {
                chk_Examlist.Enabled = true;
            }

            ClearControls();
        }
        catch (Exception ex)
        {

            lblErrorMsg.Text = "Error : " + ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal()", true);
        }
    }

    protected void select_Click(object sender, EventArgs e)
    {
        MultiView1.SetActiveView(ViewCreate);
        LinkButton lnkRemove = (LinkButton)sender;
        string code = lnkRemove.CommandArgument;


        string qry = "select * from OLN_QuestionPaper where SC='2' and IC='18' and question_code='" + code + "'";
       // string qry = "select * from OLN_QuestionPaper where SC='2' and IC='18' and question_code='" + code + "' and examcode='"++"'";

        DataTable dt = new DataTable();
        dt = DL.GetDataTable(qry);
        lblslno.Text = dt.Rows[0]["question_slno"].ToString();
        lblquationcode.Text = dt.Rows[0]["question_slno"].ToString();
        txtCkEditorQue.Text = dt.Rows[0]["question"].ToString();
        CKEditorobtA.Text = dt.Rows[0]["option_a"].ToString();
        CKEditorobtB.Text = dt.Rows[0]["option_b"].ToString();
        CKEditorobtC.Text = dt.Rows[0]["option_c"].ToString();
        CKEditorobtD.Text = dt.Rows[0]["option_d"].ToString();
      

        ListItem Item_Answer = ddlAnswer.Items.FindByValue(dt.Rows[0]["answer"].ToString());
        if (Item_Answer != null)
        {
            ddlAnswer.ClearSelection();
            Item_Answer.Selected = true;
        }
        ListItem itmSelected15 = rbtn_questionlevel.Items.FindByValue(dt.Rows[0]["difficulty_level"].ToString());
        if (itmSelected15 != null)
        {

            rbtn_questionlevel.ClearSelection();
            itmSelected15.Selected = true;
        }
        ListItem itmSelected16 = ddlchapter.Items.FindByValue(dt.Rows[0]["chapter"].ToString());
        if (itmSelected15 != null)
        {
            ddlchapter.ClearSelection();
            itmSelected16.Selected = true;
        }
        BL.Fill_Subjectlist(ddlsubject, "2", "1");
        ListItem itmSelected17 = ddlsubject.Items.FindByValue(dt.Rows[0]["subjectcode"].ToString());
        if (itmSelected15 != null)
        {
            ddlsubject.SelectedItem.Selected = false;
            itmSelected17.Selected = true;
        }
        BL.que_ExamList(chk_Examlist, "2", "1");
        ListItem itemcheck = chk_Examlist.Items.FindByValue(dt.Rows[0]["examcode"].ToString());
        bool isAllChecked = true;
        if (itemcheck != null)
        {
           // chk_Examlist.SelectedItem.Selected = false;
            itemcheck.Selected=true;
            
        }
        txtnegativemarks.Text = dt.Rows[0]["Negativemarks"].ToString();
        txtmarks.Text= dt.Rows[0]["marks"].ToString();

        chk_Examlist.Enabled = false;
        lblAcademicYear.Text = dt.Rows[0]["Academicyear"].ToString();
        lblqpCode.Text = dt.Rows[0]["question_code"].ToString();
        lblExamdate.Text = dt.Rows[0]["ExamDate"].ToString();
        lblexamTimingFrom.Text = dt.Rows[0]["ExamTimingFrom"].ToString();
        lblexamTimingTo.Text = dt.Rows[0]["ExamTimingTo"].ToString();
        lblExamSlno.Text = dt.Rows[0]["slno"].ToString();
    }

    //protected void chk_Examlist_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //   bool isAllChecked = true;
    //BL.que_ExamList(chk_Examlist, "2", "1");
    //  foreach (ListItem item in chk_Examlist.Items)
    // {
    //   if (!item.Selected)
    // {
    //     isAllChecked = false;
    //      break;
    // }
    // }


    // }


}
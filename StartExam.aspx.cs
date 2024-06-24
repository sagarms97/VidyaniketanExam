using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StartExam : System.Web.UI.Page
{
    ExamBusinessLayer BL = new ExamBusinessLayer();
  
    DataLayer DL = new DataLayer();
    string year = "23-24";
    string examcode = "1";
    string SeriesCode = "A";
    string StudentIdNo = "1234";
    string Candidatename = "ABS";
    string Rollno = "1";
    string ExamDate = DateTime.Now.ToString();
    string ExamTimingFrom = "";
    string ExamTimingTo = "";
    string StartTimeTo = "";
    string IC = "18";
    string SC = "2";
   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Cookie_Manager Cookie_Master = new Cookie_Manager();
        
        if (!Page.IsPostBack)
        {
            try
            {
                lblstudentname.Text = Cookie_Master.Get_Erp_StudentName();
                lblstudentidno.Text = Cookie_Master.Get_StudentIdno();
                lblcourse.Text = Cookie_Master.Get_Erp_StudentCourse();
                lblrollno.Text = Cookie_Master.Get_Erp_Slno();
                BL.InsertOLN_ExamMarksSheet(year, examcode, ExamDate, ExamTimingFrom, ExamTimingTo, StartTimeTo, SeriesCode, StudentIdNo, Candidatename, Rollno);
               

                BL.select_Subjects(Rep_SubjectsSection,SC,IC,examcode);
                QuestionPageindex();

              
               // BL.QuestionGenarate(Rep_Exam, "1", "2", "1");

             
            }
            
            catch (Exception ex)
            {

            }
           

        }
    }



    
  

    //void Bind_Repeater(string sectioncode)
    //{
    //    try
    //    {

          

    //        DataSet ds=  BL.questionlistWithCount( "2", "1","1",sectioncode,Convert.ToInt32(lblCurrentPage.Text),1);
    //      // BL.questionlist(rep_List,"2","1");

    //        Rep_Exam.DataSource =  ds.Tables[0];
    //        Rep_Exam.DataBind();

        

    //        string totalCount = ds.Tables[1].Rows[0][0].ToString();
    //        int totalRecordCount = 0;
    //        if (!String.IsNullOrEmpty(totalCount))
    //        {
    //            totalRecordCount = Convert.ToInt32(totalCount);
    //        }
    //        else
    //        {
    //            totalRecordCount = 0;
    //        }

    //        PopulatePager(totalRecordCount, Convert.ToInt32(lblCurrentPage.Text));


    //    }
    //    catch(Exception ex)
    //    {
    //        Response.Write(ex.Message);
    //    }
    //}

    //private void PopulatePager(int recordCount, int currentPage)
    //{
    //    double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal("1"));


    //    int pageCount = (int)Math.Ceiling(dblPageCount);

    //    lblTotalRecords.Text = pageCount.ToString();
    //    List<ListItem> pages = new List<ListItem>();
    //    if (pageCount > 0)
    //    {
    //        for (int i = 1; i <= pageCount; i++)
    //        {
    //            pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
    //        }
    //    }
    //    rptPager.DataSource = pages;
    //    rptPager.DataBind();
       

        

       
    //}


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

    
    protected void lnkPage_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkRemove = (LinkButton)sender;
            string qpcode = lnkRemove.CommandArgument;

            LinkButton lnkSubcode = (LinkButton)Rep_SubjectsSection.Items[0].FindControl("btn_selectSubject");
            string Subjectcode = lnkSubcode.CommandArgument;
        // BL.QuestionGenarate_Selected(Rep_Exam, "1", "2", "1",Subjectcode, qpcode);

            GridTOAssigSelectedValue(qpcode);

            UpdateExamMarksSheetTable();
            
            
        
        }
        catch (Exception ex)
        {
            //BLErrors.DisplayError(ex, lblMsg, this.Page);
        }

    }

    public Task<DataSet> GetOnlineBookingListForReconsile(string IC, string SC, string sort, string searchType, string searchValue, int pagesize, int pageNum)
    {
        return Task.Run(async () =>
        {
            DataSet dt = new DataSet();
            string buildingFilter = "";
            string searchFilter = "";

            int offset = pagesize * (pageNum - 1);
            if (SC != "-1")
            {
                buildingFilter = String.Format("and QuestionMaster.question_code='{0}'", SC);
            }
            if (!String.IsNullOrEmpty(searchValue))
            {
                searchFilter = String.Format("and {0} like '%{1}%'", searchType, searchValue);

            }
           
            string query = String.Format(@"
                  select *,Subjects.name,CONCAT('Q No.',question_code,question) as questionname from QuestionMaster INNER JOIN  dbo.Subjects ON dbo.QuestionMaster.subjectcode = dbo.Subjects.subjectcode where QuestionMaster.sc=@sc and QuestionMaster.ic=@ic   ;
                                   select *,Subjects.name,CONCAT('Q No.',question_code,question) as questionname from QuestionMaster INNER JOIN  dbo.Subjects ON dbo.QuestionMaster.subjectcode = dbo.Subjects.subjectcode where QuestionMaster.sc=@sc and QuestionMaster.ic=@ic
     order by  QuestionMaster.question_code
       offset {3} rows
       fetch next {4} rows only;




              ", buildingFilter, searchFilter, sort, offset.ToString(), pagesize.ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@hcode", SC);
            cmd.Parameters.AddWithValue("@bcode", IC);
            cmd.Parameters.AddWithValue("@sort", sort);
            //dt = await DL.GetDataTable(cmd);


            return dt;
        });
    }
    protected void btn_selectSubject_Click(object sender, EventArgs e)
    {
        LinkButton lnkRemove = (LinkButton)sender;
       
        string subjectcode = lnkRemove.CommandArgument;
        BL.QuestionGenarate(rptPager, IC, SC, examcode, subjectcode);
        LinkButton lnkqpcode = (LinkButton)rptPager.Items[0].FindControl("lnkPage");
        string qpcode = lnkqpcode.CommandArgument;
        BL.QuestionGenarate_Selected(Rep_Exam, IC, SC, examcode, subjectcode, qpcode);
        GridTOAssigSelectedValue(qpcode);
    }


    public void QuestionPageindex()
    {
        //by defaut showing question and page indexing
        LinkButton lnkRemove = (LinkButton)Rep_SubjectsSection.Items[0].FindControl("btn_selectSubject");
       
        string Subjectcode = lnkRemove.CommandArgument;
        BL.QuestionGenarate(rptPager, IC, SC, examcode, Subjectcode);
       

        BL.QuestionGenarate_Examsheet(Rep_ExamSheetList, IC, SC, examcode);

        LinkButton lnkRemove1 = (LinkButton)rptPager.Items[0].FindControl("lnkPage");
        string qpcode = lnkRemove1.CommandArgument;
        BL.QuestionGenarate_Selected(Rep_Exam, IC, SC, examcode, Subjectcode, qpcode);
        GridTOAssigSelectedValue(qpcode);
       
       
       
        
    }
    private void AssigSelectedValueToGrid(string selectquecode, string lblorg_anscode,string SelectedOption)
    {

        for (int j = 0; j < Rep_ExamSheetList.Items.Count; j++)
        {

            Label lblOrgquestion_code = (Label)Rep_ExamSheetList.Items[j].FindControl("lblOrgquestion_code");

            TextBox txtselectvalue = (TextBox)Rep_ExamSheetList.Items[j].FindControl("txtselectopt");
            Label lblnegmarks = (Label)Rep_ExamSheetList.Items[j].FindControl("lblNegativemarks");
            Label lblmarks = (Label)Rep_ExamSheetList.Items[j].FindControl("lblMarks");

            TextBox txtnegmarks = (TextBox)Rep_ExamSheetList.Items[j].FindControl("txtnegmarks");
            TextBox txtmarks = (TextBox)Rep_ExamSheetList.Items[j].FindControl("txtmarks");


            if (lblOrgquestion_code.Text == selectquecode)
            {

                txtselectvalue.Text = SelectedOption;
                if (lblorg_anscode == txtselectvalue.Text && lblOrgquestion_code.Text == selectquecode)
                {
                    txtnegmarks.Text = "0";
                    txtmarks.Text = lblmarks.Text;

                }
                else
                {
                    txtmarks.Text = "0";
                    txtnegmarks.Text = lblnegmarks.Text;

                }
                break;
            }

        }
    }

    public void UpdateExamMarksSheetTable()
    {

    
        string mstrFieldList = "";
        for (int i = 0; i <= Rep_ExamSheetList.Items.Count-1; i++)
        {
            if (i == 88)
            {
                int k = 0;
            }
            int j = Convert.ToInt32((Rep_ExamSheetList.Items[i].FindControl("lblSeriesquestion_code") as Label).Text);

            string SelectAns = (Rep_ExamSheetList.Items[i].FindControl("txtselectopt") as TextBox).Text;
          string KeyAns = (Rep_ExamSheetList.Items[i].FindControl("lblanswer") as Label).Text;
          string Marks = (Rep_ExamSheetList.Items[i].FindControl("txtmarks") as TextBox).Text;
          string NegMarks = (Rep_ExamSheetList.Items[i].FindControl("txtnegmarks") as TextBox).Text;
          string ConsideredMarks = (Rep_ExamSheetList.Items[i].FindControl("txtmarks") as TextBox).Text;
          string ConsideredNegMarks = (Rep_ExamSheetList.Items[i].FindControl("txtnegmarks") as TextBox).Text;
            if (mstrFieldList == "")
            {
                mstrFieldList = " SelectAns" + j.ToString() + "='" + SelectAns + "', KeyAns" + j.ToString() + "='" +KeyAns + "', Marks" + j.ToString() + "='" + Marks + "',NegMarks" + j.ToString() + "='" + NegMarks + "',ConsideredMarks" + j.ToString() + "='" + ConsideredMarks + "',ConsideredNegMarks" + j.ToString() + "='" + ConsideredNegMarks + "'";
            }
            else
            {
                mstrFieldList = mstrFieldList + ",  SelectAns" + j.ToString() + "='" + SelectAns + "', KeyAns" + j.ToString() + "='" +KeyAns + "', Marks" + j.ToString() + "='" + Marks + "',NegMarks" + j.ToString() + "='" + NegMarks + "',ConsideredMarks" + j.ToString() + "='" + ConsideredMarks + "',ConsideredNegMarks" + j.ToString() + "='" + ConsideredNegMarks + "'";
            }
        }


        string cmdstr11 = "Update OLN_ExamMarksSheet  set " + mstrFieldList + " " +
                   " where Academicyear='"+year+"' and examcode='"+examcode+"' and SeriesCode='"+SeriesCode+"' and StudentIdNo='"+StudentIdNo+"'  ;";


        BL.Execute(cmdstr11);
    }
 
    private void GridTOAssigSelectedValue(string qpcode)
    {
        for (int j = 0; j < Rep_Exam.Items.Count; j++)
        {
            Rep_Exam.Items[j].Visible = false;

            Label lblquestion = (Label)Rep_Exam.Items[j].FindControl("lblquestion");


            string[] questioncode = lblquestion.Text.Split(':');
            if (qpcode == questioncode[0])
            {
                Rep_Exam.Items[j].Visible = true;

            }



        }
       
    }
    protected void rbnOptionA_CheckedChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as RadioButton).NamingContainer as RepeaterItem;
        string selectquecode = (item.FindControl("lblorg_quecode") as Label).Text;
        string lblorg_anscode = (item.FindControl("lblorg_anscode") as Label).Text;

        AssigSelectedValueToGrid(selectquecode, lblorg_anscode, "A");
       


    }
    protected void rbnOptionB_CheckedChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as RadioButton).NamingContainer as RepeaterItem;
        string selectquecode = (item.FindControl("lblorg_quecode") as Label).Text;
        string lblorg_anscode = (item.FindControl("lblorg_anscode") as Label).Text;

        AssigSelectedValueToGrid(selectquecode, lblorg_anscode, "B"); 

        
    }
    protected void rbnOptionC_CheckedChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as RadioButton).NamingContainer as RepeaterItem;
        string selectquecode = (item.FindControl("lblorg_quecode") as Label).Text;
        string lblorg_anscode = (item.FindControl("lblorg_anscode") as Label).Text;

        AssigSelectedValueToGrid(selectquecode, lblorg_anscode, "C");
       
          
        
    }
    protected void rbnOptionD_CheckedChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as RadioButton).NamingContainer as RepeaterItem;
        string selectquecode = (item.FindControl("lblorg_quecode") as Label).Text;
        string lblorg_anscode = (item.FindControl("lblorg_anscode") as Label).Text;
        AssigSelectedValueToGrid(selectquecode, lblorg_anscode, "D");
        
          
        
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        UpdateExamMarksSheetTable();
       // lblErrorMsg.Text = "SUCCESS : " + "Exam Submitted Successfully";
        lblErrorMsg.Text = "Total no of question :   89 <br> Attempt : 85 <br> Not Attempt : 4";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal()", true);
       
        
    }
    protected void btn_result_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
}
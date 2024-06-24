
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

public partial class Pages_Gen_Series : System.Web.UI.Page
{

    _BLOLN_QuestionPaperSeries BL=new _BLOLN_QuestionPaperSeries();

    Cookie_Manager CookieMaster = new Cookie_Manager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                BL.Fill_Exam_DropDown(ddlExam, "18", "2");



            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = "Error : " + ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal();", true);
            }
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            int num_of_series = 0;

            string ic = "18";
            string sc = "2";

            string result = BL.ReturnString(String.Format("select SeriesCount from OLN_ExamMaster where OLN_ExamMaster.IC='{0}' AND OLN_ExamMaster.SC='{1}' AND OLN_ExamMaster.exam_code='{2}'", ic, sc, ddlExam.SelectedValue));


           
            if (String.IsNullOrEmpty(result))
            {
                throw new Exception("SERIES COUNT IS NOT DEFINED . PLEASE DEFINE NO OF SERIES IN EXAM MASTER ");
            }
            else
            {
                num_of_series = Convert.ToInt32(result);
            }

            char[] alphabets = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            DataTable dt = BL.GetQuestions_For_Proccessing(ic, sc, ddlExam.SelectedValue, CookieMaster.Get_Year());
            if (dt.Rows.Count == 0)
            {
                throw new Exception("SORRY NO QUESTIONS FOUND FOR EXAM : "+ddlExam.SelectedItem.Text);
            }
            else
            {

                BL.DeleteExistingSeries(ddlExam.SelectedValue, CookieMaster.Get_Year(), ic, sc);

                for (int k = 0; k < num_of_series; k++)
                {
                    DataTable distinct_sections = BL.GetDistinctValues(dt, "subjectcode");
                    List<DataTable> Section_Wise_Question_List = new List<DataTable>();
                    List<DataTable> Randomed_Rows_List = new List<DataTable>();

                    for (int i = 0; i < distinct_sections.Rows.Count; i++)
                    {
                        string filter_condition = String.Format(@"subjectcode='{0}'", distinct_sections.Rows[i][0].ToString());
                        DataTable filtered_datable = BL.FilterDataTable(dt, filter_condition);
                        Section_Wise_Question_List.Add(filtered_datable);
                    }

                    for (int i = 0; i < Section_Wise_Question_List.Count; i++)
                    {
                        DataTable RandomizedRows = BL.RandomizeRows(Section_Wise_Question_List[i] as DataTable);
                        Randomed_Rows_List.Add(RandomizedRows);
                    }


                    BL.Save_Question_PaperSeries(Randomed_Rows_List, alphabets[k].ToString());
                }



                lblErrorMsg.Text = "SUCCESS : " +"SERIES GENERATED";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal();", true);


            }

        }
        catch(Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal();", true);
        }
    }
}
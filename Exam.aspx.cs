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

public partial class Exam : System.Web.UI.Page
{
    BusinessLayer BL = new BusinessLayer();
    DataLayer DL = new DataLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        form1.Disabled = true;
        if (!Page.IsPostBack)
        {
            try
            {
               
                Bind_Repeater();
                BL.select_Subjects(Rep_SubjectsSection,"2","18","1");
             
            }
            
            catch (Exception ex)
            {

            }
           

        }
    }


    [WebMethod]
    public static void FetchRepeaterData(Repeater rpt,Label lbl)
    {
        lbl.Text = rpt.Items.Count.ToString();
    }

    [WebMethod]
    public static string Test_EndPoint()
    {
        return "hello";
    }



    void Bind_Repeater()
    {
        try
        {
            DataSet ds=  BL.questionlistWithCount( "2", "1","1","105",Convert.ToInt32(lblCurrentPage.Text),1);
           BL.questionlist(rep_List,"2","18");

            Rep_Exam.DataSource =  ds.Tables[0];
            Rep_Exam.DataBind();

        

            string totalCount = ds.Tables[1].Rows[0][0].ToString();
            int totalRecordCount = 0;
            if (!String.IsNullOrEmpty(totalCount))
            {
                totalRecordCount = Convert.ToInt32(totalCount);
            }
            else
            {
                totalRecordCount = 0;
            }

            PopulatePager(totalRecordCount, Convert.ToInt32(lblCurrentPage.Text));


        }
        catch(Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    private void PopulatePager(int recordCount, int currentPage)
    {
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal("1"));


        int pageCount = (int)Math.Ceiling(dblPageCount);

        lblTotalRecords.Text = pageCount.ToString();
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            for (int i = 1; i <= pageCount; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
       

        

       
    }


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
            string currentPage = (sender as LinkButton).CommandArgument;
            lblCurrentPage.Text = currentPage;
            Bind_Repeater();
        
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

    }
}
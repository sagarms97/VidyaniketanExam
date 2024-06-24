using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_SeriesWiseReport : System.Web.UI.Page
{

    _BLOLN_QuestionPaperSeries BL = new _BLOLN_QuestionPaperSeries();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                BL.Fill_Exam_AndSeriesCode(ddlExam, ddlSeries, "18", "2");
            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = "Error : " + ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal();", true);
            }
        }
    }


    protected void Export_Click_Click(object sender, EventArgs e)
    {
        var sb = new StringBuilder();
        ExportDiv.RenderControl(new HtmlTextWriter(new StringWriter(sb)));

        string s = sb.ToString();
   
        string fileName = String.Format("{0}_{1}",ddlExam.SelectedItem.Text,ddlSeries.SelectedItem.Text);
        Response.AppendHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        this.EnableViewState = false;
        Response.Write(s);
        Response.End();

        //Response.Clear();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", "attachment;filename=TimeTable .xls");
        //Response.Charset = "";
        //Response.ContentType = "application/vnd.ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //replCourse.RenderControl(hw);
        //Response.Output.Write(sw.ToString());
        //Response.Flush();
        //Response.End();



    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            Cookie_Manager Cookies = new Cookie_Manager();

            BL.Bind_Series_Report(rptReportContent, "18", "2", ddlExam.SelectedValue, ddlSeries.SelectedValue, Cookies.Get_Year());

        }catch(Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "pop", "ErrorModal();", true);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string user = Erpusername();
            if (user != "")
            {
             
                Erpusername();
               
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }

    public string Erpusername()
    {
        try
        {
            HttpCookie Erpusername = HttpContext.Current.Request.Cookies["Erpusername"];
            return HttpContext.Current.Server.HtmlEncode(Erpusername.Value);
        }
        catch (Exception)
        {
            return "";
        }
    }
    
}

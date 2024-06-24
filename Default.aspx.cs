using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    BusinessLayer BL = new BusinessLayer();

    BLLogin LoginMaster = new BLLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
       
           
        if (!Page.IsPostBack)
        {
            HttpCookie Erpusername = new HttpCookie("Erpusername");
            Erpusername.Value ="";
            Erpusername.Expires = DateTime.Now.AddSeconds(-1);           
            Response.Cookies.Clear();
           
          

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
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            
         
            string strUserID = txtID.Text;
            DataSet ds = new DataSet();
            ds = LoginMaster.Get_LoginDetails(txtID.Text, txtPWD.Text,"18");
            if (ds.Tables[0].Rows.Count >= 1)
            {
                string usertype = ds.Tables[0].Rows[0][3].ToString();

                HttpCookie Erpuserid = new HttpCookie("Erpuserid");
                Erpuserid.Value = ds.Tables[0].Rows[0][0].ToString();
                Erpuserid.Expires = DateTime.Now.AddDays(2);
                Response.SetCookie(Erpuserid);

                HttpCookie Erpusername = new HttpCookie("Erpusername");
                Erpusername.Value = ds.Tables[0].Rows[0][2].ToString(); ;
                Erpusername.Expires = DateTime.Now.AddDays(2);
                Response.SetCookie(Erpusername);

                HttpCookie ErpUserType = new HttpCookie("ErpUserType");
                ErpUserType.Value = "A";
                ErpUserType.Expires = DateTime.Now.AddDays(2);
                Response.SetCookie(ErpUserType);

                HttpCookie Erp_IC = new HttpCookie("Erp_IC");
                Erp_IC.Value = "18";
                Erp_IC.Expires = DateTime.Now.AddDays(2);
                Response.SetCookie(Erp_IC);



                HttpCookie ErpuserYear = new HttpCookie("ErpAcademicYear");
                ErpuserYear.Value = ddlAcademicYear.SelectedValue;
                ErpuserYear.Expires = DateTime.Now.AddDays(2);
                Response.SetCookie(ErpuserYear);

                Response.Redirect("/Pages/QuestionMaster.aspx",true);


            }
            else if (ds.Tables[1].Rows.Count >= 1)
            {


                HttpCookie ErpuserYear = new HttpCookie("ErpAcademicYear");
                ErpuserYear.Value = ddlAcademicYear.SelectedValue;
                ErpuserYear.Expires = DateTime.Now.AddHours(4);
                Response.SetCookie(ErpuserYear);


                HttpCookie Erp_Slno = new HttpCookie("Erp_Slno");
                Erp_Slno.Value = ds.Tables[1].Rows[0]["Slno"].ToString();
                Erp_Slno.Expires = DateTime.Now.AddHours(4);
                Response.SetCookie(Erp_Slno);


                HttpCookie Erpuserid = new HttpCookie("Erp_StudentIdNo");
                Erpuserid.Value = ds.Tables[1].Rows[0]["StudentIdNo"].ToString();
                Erpuserid.Expires = DateTime.Now.AddHours(2);
                Response.SetCookie(Erpuserid);

                HttpCookie Erpusername = new HttpCookie("Erp_StudentName");
                Erpusername.Value = ds.Tables[1].Rows[0]["CandidateName"].ToString(); ;
                Erpusername.Expires = DateTime.Now.AddHours(2);
                Response.SetCookie(Erpusername);


                HttpCookie Erp_IC = new HttpCookie("Erp_IC");
                Erp_IC.Value = ds.Tables[1].Rows[0]["ic"].ToString(); 
                Erp_IC.Expires = DateTime.Now.AddHours(2);
                Response.SetCookie(Erp_IC);

                HttpCookie ErpuserCourse = new HttpCookie("Erp_StudentCourse");
                ErpuserCourse.Value = ds.Tables[1].Rows[0]["Course"].ToString(); ;
                ErpuserCourse.Expires = DateTime.Now.AddHours(2);
                Response.SetCookie(ErpuserCourse);



                HttpCookie ErpuserMobileNo = new HttpCookie("Erp_StudentMobileNo");
                ErpuserMobileNo.Value = ds.Tables[1].Rows[0]["MobileNo"].ToString(); ;
                ErpuserMobileNo.Expires = DateTime.Now.AddHours(2);
                Response.SetCookie(ErpuserMobileNo);


                HttpCookie ErpUserType = new HttpCookie("ErpUserType");
                ErpUserType.Value = "S";
                ErpUserType.Expires = DateTime.Now.AddDays(2);
                Response.SetCookie(ErpUserType);


                Response.Redirect("OnlineExam.aspx",true);

            }
            else
            {
                throw new Exception("INVALID USERID OR PASSWORD");
            }

             

         
        }
        catch (Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ErrorModal();", true);
        }
       
         
        
    }
    public string Encrypt(string password)
    {
        string strmsg = string.Empty;
        byte[] encode = new byte[password.Length];
        encode = Encoding.UTF8.GetBytes(password);
        strmsg = Convert.ToBase64String(encode);
        return strmsg;
    }
}
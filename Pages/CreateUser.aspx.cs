using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CreateUser : System.Web.UI.Page
{
    BusinessLayer BL = new BusinessLayer();
    DataLayer DL = new DataLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           
            //BL.FillDepartment2(ddlDeprtmnt);


            BL.FillUsers(Gridusers, College());
          
            Gridusers.Columns[1].Visible = false;
            
        }
    }




    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
               string qry = "";
               string pathLink = "", path = "";

               path =pathLink= "~/UserProfile/";

               string PhotoPath = "";
              // CreateFolderIfMissiong(path);
               if (avatarUpload.HasFile)
               {

                   int length = avatarUpload.PostedFile.ContentLength;
                   byte[] imgbyte = new byte[length];
                   avatarUpload.PostedFile.InputStream.Read(imgbyte, 0, length);

                   string extension = Path.GetExtension(avatarUpload.PostedFile.FileName);
                   string imgname =  extension;
                   path = Path.Combine(Server.MapPath(pathLink + imgname));
                   if (File.Exists(path))
                   {
                       File.Delete(path);
                   }
                   avatarUpload.SaveAs(path);
                  PhotoPath = pathLink+ imgname;
                   //BL.Update_Photo_Register(hfID.Value, extension, PhotoPath);
               }
               txtslno.Text = BL.ReturnString("select isnull(max(StaffSlno),0)+1 from Login ");
               BL.create_users(txtUserId.Text.Trim(), Encrypt(txtpassword.Text.Trim()), txtUserName.Text.Trim(),PhotoPath,txtslno.Text);

         
            txtUserId.Enabled = true;
            string msg = "Saved";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
          
            clear();
            BL.FillUsers(Gridusers, College());
        }
        catch (Exception ex)
        {
            lblErrorMsg.Text = "Error : " + ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ErrorModal();", true);
        }
       
    }

    public string College()
    {
        try
        {
            HttpCookie College = Request.Cookies["College"];
            return Server.HtmlEncode(College.Value);
        }
        catch (Exception)
        {
            return "";
        }

    }
    public void clear()
    {
        txtUserId.Text = "";
        txtUserName.Text = "";
        txtpassword.Text = "";
       
       
        ddlDeprtmnt.SelectedIndex = 0;
      


    }
    public string Encrypt(string password)
    {
        string strmsg = string.Empty;
        byte[] encode = new byte[password.Length];
        encode = Encoding.UTF8.GetBytes(password);
        strmsg = Convert.ToBase64String(encode);
        return strmsg;
    }
    protected void Select_OnClick(object sender, EventArgs e)
    {
        try
        {
            btnUser.Visible = false;
            btn_update.Visible = true;
            string userid = (sender as LinkButton).CommandArgument.ToString();

            SqlCommand cmd = new SqlCommand();
            string qry = "delete from Login where userid='" + userid + "'";
            cmd.CommandText = qry;
            DL.ExecuteCMD(cmd);
        

          string msg = "Deleted";
          Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);

          clear();
          BL.FillUsers(Gridusers, College());

                  
           
        }

        catch (Exception ex)
        {
            string msg = "error : " + ex.Message;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "error('" + msg + "')", true);
        }

    }




    protected void btn_update_Click(object sender, EventArgs e)
    {
       
       // BL.update_users(txtUserId.Text, txtUserName.Text, ddlDeprtmnt.SelectedValue, ddlDeprtmnt.SelectedItem.ToString());
        string msg = "Saved";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "success('" + msg + "')", true);
        clear();
        btnUser.Visible = true;
        btn_update.Visible = false;
        txtUserId.Enabled = true;
        txtpassword.Enabled = true;
       // BL.FillDepartmentUsers(Gridusers, College());

    }
}
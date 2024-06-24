using Org.BouncyCastle.Cms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for BLLogin
/// </summary>
public class BLLogin
{
    DataLayer DL = new DataLayer();


    public string Encrypt(string password)
    {
        string strmsg = string.Empty;
        byte[] encode = new byte[password.Length];
        encode = Encoding.UTF8.GetBytes(password);
        strmsg = Convert.ToBase64String(encode);
        return strmsg;
    }

    public DataSet Get_LoginDetails(string userid,string password,string ic)
    {
        string query = "select [userid] ,[password],[username]  ,[usertype] ,[DepartmentName],[DepartmentCode],[imagepath] from [Login] where userid=@userid and password=@password;";
        query += "select * from [StudentRegister] where  ic=@ic and LoginID=@userid and password=@StudentPassword"; 

        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddWithValue("@userid", userid);
        cmd.Parameters.AddWithValue("@StudentPassword", password);
        cmd.Parameters.AddWithValue("@password", Encrypt(password));
        cmd.Parameters.AddWithValue("@ic", ic);

        ds = DL.ReturnDataSet(cmd);



        return ds;



    }
}
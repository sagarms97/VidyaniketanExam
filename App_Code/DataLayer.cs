using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DataLayer
/// </summary>
public class DataLayer
{
	public DataLayer()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    SqlConnection conObjERP = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineExam"].ConnectionString.ToString());

    public DataSet GetRecordDataSet(string gstrQrystr)
    {
        IntializeConnection();
        SqlDataAdapter SQLDA = new SqlDataAdapter(gstrQrystr, conObjERP);
        DataSet ds = new DataSet();
        SQLDA.Fill(ds);

        return ds;

    }
    public SqlDataReader GetReader(string strQrystr)
    {
        IntializeConnection();
        SqlCommand cmdData = new SqlCommand(strQrystr, conObjERP);

        SqlDataReader read = cmdData.ExecuteReader();
        return read;

    }
    public void IntializeConnection()
    {
        if (conObjERP.State == ConnectionState.Open)
        {
            conObjERP.Close();
        }
        conObjERP.Open();
    }

    public void ExecuteCMD(SqlCommand cmd)
    {
        IntializeConnection();
        SqlTransaction sqlTrans = conObjERP.BeginTransaction();
        cmd.Connection = conObjERP;
        cmd.Transaction = sqlTrans;
        cmd.ExecuteNonQuery();
        sqlTrans.Commit();

    }
    public DataTable GetDataTable(SqlCommand cmd)
    {
        IntializeConnection();
        cmd.Connection = conObjERP;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt != null)
        {
            return dt;
        }
        else
        {
            return null;
        }

    }

    

    public DataSet ReturnDataSet(SqlCommand cmd)
    {
        IntializeConnection();
        cmd.Connection = conObjERP;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dt = new DataSet();
        da.Fill(dt);
        if (dt != null)
        {
            return dt;
        }
        else
        {
            return null;
        }
    }
    public SqlDataReader GetReader(SqlCommand cmd)
    {
        IntializeConnection();
        cmd.Connection = conObjERP;
        SqlDataReader read = cmd.ExecuteReader();
        return read;

    }
    public DataTable GetDataTable(string strQrystr)
    {
        IntializeConnection();
        SqlCommand cmd = new SqlCommand(strQrystr, conObjERP);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt != null)
        {
            return dt;
        }
        else
        {
            return null;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cookie_Manager
/// </summary>
public class Cookie_Manager
{
    public string Get_UserName()
    {
        try
        {
            HttpCookie hm_SocietyCode = HttpContext.Current.Request.Cookies["Erpusername"];
            return HttpContext.Current.Server.HtmlEncode(hm_SocietyCode.Value);
        }
        catch (Exception)
        {
            return "";
        }
    }


    public string Get_UserId()
    {
        try
        {
            HttpCookie hm_SocietyCode = HttpContext.Current.Request.Cookies["Erpuserid"];
            return HttpContext.Current.Server.HtmlEncode(hm_SocietyCode.Value);
        }
        catch (Exception)
        {
            return "";
        }
    }

    public string Get_Year()
    {
        try
        {
            HttpCookie hm_SocietyCode = HttpContext.Current.Request.Cookies["ErpAcademicYear"];
            return HttpContext.Current.Server.HtmlEncode(hm_SocietyCode.Value);
        }
        catch (Exception)
        {
            return "";
        }
    }


    public string Get_StudentIdno()
    {
        try
        {
            HttpCookie hm_SocietyCode = HttpContext.Current.Request.Cookies["Erp_StudentIdNo"];
            return HttpContext.Current.Server.HtmlEncode(hm_SocietyCode.Value);
        }
        catch (Exception)
        {
            return "";
        }
    }


    public string Get_UserType()
    {
        try
        {
            HttpCookie hm_SocietyCode = HttpContext.Current.Request.Cookies["ErpUserType"];
            return HttpContext.Current.Server.HtmlEncode(hm_SocietyCode.Value);
        }
        catch (Exception)
        {
            return "";
        }
    }

    public string Get_Erp_StudentName()
    {
        try
        {
            HttpCookie hm_SocietyCode = HttpContext.Current.Request.Cookies["Erp_StudentName"];
            return HttpContext.Current.Server.HtmlEncode(hm_SocietyCode.Value);
        }
        catch (Exception)
        {
            return "";
        }
    }


    public string Get_Erp_StudentCourse()
    {
        try
        {
            HttpCookie hm_SocietyCode = HttpContext.Current.Request.Cookies["Erp_StudentCourse"];
            return HttpContext.Current.Server.HtmlEncode(hm_SocietyCode.Value);
        }
        catch (Exception)
        {
            return "";
        }
    }



    public string Get_Erp_StudentMobileNo()
    {
        try
        {
            HttpCookie hm_SocietyCode = HttpContext.Current.Request.Cookies["Erp_StudentMobileNo"];
            return HttpContext.Current.Server.HtmlEncode(hm_SocietyCode.Value);
        }
        catch (Exception)
        {
            return "";
        }
    }

    public string Get_Erp_IC()
    {
        try
        {
            HttpCookie hm_SocietyCode = HttpContext.Current.Request.Cookies["Erp_IC"];
            return HttpContext.Current.Server.HtmlEncode(hm_SocietyCode.Value);
        }
        catch (Exception)
        {
            return "";
        }
    }



        public string Get_Erp_SeriesCode()
        {
            try
            {
                HttpCookie hm_SocietyCode = HttpContext.Current.Request.Cookies["Erp_SeriesCode"];
                return HttpContext.Current.Server.HtmlEncode(hm_SocietyCode.Value);
            }
            catch (Exception)
            {
                return "";
            }
        }

     public string Get_Erp_Slno()
    {
        try
        {
            HttpCookie hm_SocietyCode = HttpContext.Current.Request.Cookies["Erp_Slno"];
            return HttpContext.Current.Server.HtmlEncode(hm_SocietyCode.Value);
        }
        catch (Exception)
        {
            return "";
        }
    }

}
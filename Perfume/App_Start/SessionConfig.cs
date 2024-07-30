using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Perfume.Models;
namespace Perfume.App_Start
{
    public class SessionConfig
    {
        public static void SetUser(TaiKhoan user)
        {
            HttpContext.Current.Session["user"] = user;
        }
        public static TaiKhoan GetUser()
        {
            return (TaiKhoan)HttpContext.Current.Session["user"];

        }
    }
}
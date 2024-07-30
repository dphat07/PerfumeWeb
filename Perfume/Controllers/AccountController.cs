using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Perfume.Models;
using Perfume.App_Start;
using System.Web.Security;

namespace Perfume.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        Perfume4Entities2 db = new Perfume4Entities2();
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(TaiKhoan tk)
        {
            if(ModelState.IsValid)
            {
                if(db.TaiKhoans.Any(t=>t.tentaikhoan==tk.tentaikhoan))
                {
                    ViewBag.thongbao = "Đã tồn tại tên đăng nhập";
                    return View();
                }    
                else
                {
                    SessionConfig.SetUser(tk);
                    var us = SessionConfig.GetUser();
                    db.TaiKhoans.Add(tk);
                    db.SaveChanges();
                    return RedirectToAction("Product", "Product");
                }    
            }    
            else
            {
                return View(tk);
            }    
            
        }
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Login(TaiKhoan tk)
        {
            
            if (tk.tentaikhoan!=null && tk.matkhau!=null)
            {
                string username = tk.tentaikhoan;
                string password = tk.matkhau;
                string admin = "Admin";
                string UserName = Cookie.get("UserName");
                string PassWord = Cookie.get("PassWord");
                SessionConfig.SetUser(tk);
                var us = SessionConfig.GetUser();
                var user = db.TaiKhoans.FirstOrDefault(t => t.tentaikhoan == username & t.matkhau == password);
                if (user!=null)
                {
                    if (username.ToLower() ==admin.ToLower())
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }    
                    return RedirectToAction("Product", "Product");
                }
                else
                {
                    ViewBag.thongbao = "Tài khoản hoặc mật khẩu không đúng";
                    return View();
                }
            }
            else
            {
                return View(tk);
            }
        }
        public ActionResult Logout()
        {
            SessionConfig.SetUser(null);
            return RedirectToAction("Login", "Account");
        }
        
    }
}
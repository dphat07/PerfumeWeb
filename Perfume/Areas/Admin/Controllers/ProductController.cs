using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Perfume.Models;
using Perfume.Filters;
namespace Perfume.Areas.Admin.Controllers
{
    [AdminAuthorization]
    
    public class ProductController : Controller
    {
        // GET: Admin/Product
        Perfume4Entities2 db = new Perfume4Entities2();
        public ActionResult Product()
        {
                return View(db.sanPhams.ToList());
        }
    }
}
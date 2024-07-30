using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Perfume.Filters;
namespace Perfume.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        [MyAuthenFilter]
        public ActionResult Cart()
        {
            return View();
        }
    }
}
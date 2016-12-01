using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemeGenerator.UI.Controllers
{
    public class PersonalController : Controller
    {
        // GET: Personal
        public ActionResult Creator()
        {
            return View();
        }
    }
}
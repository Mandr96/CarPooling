using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarPooling.Controllers
{
    public class PasseggeriController : Controller
    {
        // GET: Passeggeri
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegistrazionePasseggero()
        {
            return View();
        }
        public ActionResult HomePasseggero()
        {
            return View();
        }
    }
}
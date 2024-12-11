using CarPooling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarPooling.Controllers
{
    public class PasseggeriController : Controller
    {
        // GET: Passeggeri
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePasseggero()
        {
            string emailPasseggero = User.Identity.Name; //"marcopuccio@gmail.com"; //da coniugare con login
            return View(Passeggero.SelectById(emailPasseggero));
        }

        public ActionResult PartialRicercaViaggi()
        {
            return PartialView("_PartialRicercaViaggi");
        }
        
        public ActionResult RegistrazionePasseggero()
        {
            return View();
        }
    }
}
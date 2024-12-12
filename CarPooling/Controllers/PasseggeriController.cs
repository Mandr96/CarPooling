using CarPooling.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace CarPooling.Controllers
{
    [Authorize(Roles = "Passeggero")]
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

        public ActionResult HomePasseggero(string email)
        {
            return View(Passeggero.SelectById(email));
        }

        public ActionResult PartialRicercaViaggi()
        {
            return PartialView("_PartialRicercaViaggi");
        }

        public ActionResult ViaggiPasseggero(string email)
        {
            return Json(Passeggero.GetViaggiByPasseggero(email).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrenotazioniTotali()
        {
            return Json(Passeggero.CountPrenotazioniTotali(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ModificaPasseggero(string email)
        {
            return View(Passeggero.SelectById(email));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificaPasseggero(Passeggero p)
        {
            if (ModelState.IsValid)
            {
                Passeggero.EditPasseggero(p);
                return RedirectToAction("HomePasseggero", new { email = p.EmailPasseggero });
            }
            else
            {
                return View(p);
            }
        }
    }
}
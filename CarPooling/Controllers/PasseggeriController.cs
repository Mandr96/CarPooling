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
    public class PasseggeriController : Controller
    {
        // GET: Passeggeri
        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult HomePasseggero()
        {
            //string emailPasseggero = User.Identity.Name; //da coniugare con login
            string emailPasseggero = "marcopuccio@gmail.com";
            return View(Passeggero.SelectById(emailPasseggero));
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

        [HttpGet]

        public ActionResult RegistrazionePasseggero()
        {


            return View();
        }

        [HttpPost]

        public ActionResult RegistrazionePasseggero(Passeggero p)
        {
            ModelState.Remove("Credenziali.Email");
            p.Credenziali.Ruolo = "Passeggero";
            

            if (ModelState.IsValid)
            {
                Utente utente = Utente.SelectByEmail(p.EmailPasseggero);
                if (utente.Username == null)
                {
                    
                    Passeggero.InsertPasseggero(p);

                    return RedirectToAction("Login", "Login");

                }
                else
                {
                    TempData["RegistrazioneError"] = "Email già presente nel sistema!";
                    return View(p);

                }
            }
            else
            {
                TempData["RegistrazioneError"] = "Dati non corretti/mancanti!";
                return View(p);
            }

            
        }

    }
}
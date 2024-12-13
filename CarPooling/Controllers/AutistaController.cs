using CarPooling.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace CarPooling.Controllers
{
    [Authorize(Roles = "Autista")]
    public class AutistaController : Controller
    {
        public ActionResult HomeAutista(string email)
        {
            List<Viaggio> viaggi = Viaggio.SelectByAutista(email);

            ViewBag.Email = email;
            return View(viaggi);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult RegistrazioneAutista()
        {
            ViewBag.Registrazione = null;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrazioneAutista(Autista p)

        {
            p.Credenziali.Ruolo = "Autista";
            ModelState.Remove("Credenziali.Email");
            if (ModelState.IsValid)
            {
                Autista autista = new Autista();
                autista = Autista.SelectById(p.EmailAutista);

                if (autista.EmailAutista == null)
                {

                    if (p.File != null && p.File.ContentLength > 0)
                    {

                        string fileExt = Path.GetExtension(p.File.FileName);
                        string fileNameUnique = $"{DateTime.Now.Ticks}{fileExt}";
                        string PathFile = Path.Combine(Server.MapPath("~/Content/imgUpload"), fileNameUnique);


                        p.File.SaveAs(PathFile);
                        p.PhotoFileName = fileNameUnique;
                        p.PathFile = PathFile;
                    }
                    
                    Autista.InsertAutista(p);
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

                TempData["RegistrazioneError"] = "Dati non corretti!";
                return View(p);
            }

            
        }

        public ActionResult AggiornaPrenotazione(int id, string autista, int state)
        {
            Viaggio.AggiornaDisp(id, state);
            return RedirectToAction("HomeAutista", "Autista", new { email = autista });
        }


    }
}
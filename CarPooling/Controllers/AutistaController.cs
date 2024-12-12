using CarPooling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace CarPooling.Controllers
{
    public class AutistaController : Controller
    {
        public ActionResult HomeAutista(string email)
        {         
            List<Viaggio> viaggi = Viaggio.SelectByAutista(email);
            ViewBag.Email = email;  

           
            return View(viaggi);
        }

        public ActionResult ChiudiPrenotazione(int id, string autista)
        {

            Viaggio.UpdateDisponibilita(id);
            return RedirectToAction("HomeAutista", "Autista", new { email = autista });
        }
       
    }
}
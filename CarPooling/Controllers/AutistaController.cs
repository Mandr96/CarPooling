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

        public ActionResult AggiornaPrenotazione(int id, string autista, int state)
        {
            Viaggio.AggiornaDisp(id, state);
            return RedirectToAction("HomeAutista", "Autista", new { email = autista });
        }
    }
}
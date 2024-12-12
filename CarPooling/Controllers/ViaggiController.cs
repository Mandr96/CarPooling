using CarPooling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace CarPooling.Controllers
{
    public class ViaggiController : Controller
    {
        public ActionResult Dettagli(int id)
        {
            Viaggio v = Viaggio.SelectById(id);
            //List<Prenotazione> prenotazioni = Prenotazione.SelectBy
            return View(v);
        }
        //TODO Parametrizzare
        public ActionResult ViaggiDisponibili()
        {
            var viaggiList = Viaggio.SelectByTratta("Torino", "Milano");
            return View(viaggiList);
        }
    }
}
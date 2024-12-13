using CarPooling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarPooling.Controllers
{
    [Authorize(Roles="Passeggero")]
    public class PrenotazioniController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreaPrenotazione(Prenotazione p)
        {
            Prenotazione.InsertPrenotazione(p);
            return RedirectToAction("HomePasseggero", "Passeggeri", new {email=p.EmailPasseggero});
        }
    }
}
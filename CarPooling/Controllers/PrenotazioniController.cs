using CarPooling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarPooling.Controllers
{
    public class PrenotazioniController : Controller
    {
        [HttpPost]
        public ActionResult CreaPrenotazione(Prenotazione p)
        {
            Prenotazione.InsertPrenotazione(p);
            return Redirect(FormsAuthentication.DefaultUrl);
        }
    }
}
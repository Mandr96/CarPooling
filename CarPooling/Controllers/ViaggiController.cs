using CarPooling.Models;
using Microsoft.Ajax.Utilities;
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
        
        public ActionResult ViaggiDisponibili(Viaggio viaggio)
        {
            List<Viaggio> viaggiList;
            if (viaggio.DataOraPartenza != null)
            {
                viaggiList = Viaggio.SelectByTratta(viaggio.DataOraPartenza, viaggio.CittaPartenza, viaggio.CittaArrivo);
            }
            else
            {
                viaggiList = Viaggio.SelectByTratta(DateTime.Now, viaggio.CittaPartenza, viaggio.CittaArrivo);
            }
            return View(viaggiList);
        }

        public ActionResult GetViaggiatori(int id)
        {
            List<Passeggero> passeggeri = Viaggio.GetPasseggeriByViaggio(id);
            return Json(passeggeri, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreaViaggio()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreaViaggio(Viaggio viaggio, TimeSpan timePartenza, TimeSpan timeArrivo)
        {
            if (ModelState.IsValid && timePartenza != null && timeArrivo != null)
            {
                viaggio.Disponibile = true;
                viaggio.DataOraPartenza = viaggio.DataOraPartenza.Add(timePartenza);
                viaggio.DataOraArrivo = viaggio.DataOraArrivo.Add(timeArrivo);
                Viaggio.InsertViaggio(viaggio);
                return RedirectToAction("HomeAutista", "Autista");
            }
            return View(viaggio);
        }
    }
}
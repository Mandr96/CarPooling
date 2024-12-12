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
    }
}
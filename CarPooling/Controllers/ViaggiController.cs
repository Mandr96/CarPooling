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
            List<Prenotazione> prenotazioni = Prenotazione.SelectByIdViaggio(id);
            //Dalle prenotazioni, recuperare nome e cognome dei passeggeri 
           
            return View(v);
        }
        
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
    }
}
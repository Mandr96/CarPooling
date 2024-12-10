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
        public ActionResult TestViaggi()
        {
            var listaViaggi = new List<Viaggio>() { Viaggio.SelectById(1) };
            return View(listaViaggi);
        }
    }
}
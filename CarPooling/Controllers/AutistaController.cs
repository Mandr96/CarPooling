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

        [HttpGet]

        public ActionResult RegistrazioneAutista()
        {
            return View();
        }

        [HttpPost]

        public ActionResult RegistrazioneAutista(Autista p)
        {

            object Autista = null;

            Autista = SelectById(string p.EmailAutista);

            if (Autista == null)
            {
                if (p.File != null && p.Foto.ContentLength > 0)
                {

                    string fileExt = Path.GetExtension(p.Foto.FileName);
                    string fileNameUnique = $"{DateTime.Now.Ticks}{fileExt}";
                    string PathFile = Path.Combine(Server.MapPath("~/Content/imgUpload"), fileNameUnique);


                    p.Foto.SaveAs(PathFile);
                    p.PhotoFileName = fileNameUnique;
                    p.PathFile = PathFile;


                }

                InsertAutista(p);

            }
            else
            {
                ViewBag.Registrazione = "Email già registrata!"

            }

            return View();
        }


    }
}
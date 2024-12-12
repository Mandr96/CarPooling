using CarPooling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarPooling.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Utente u)
        {
            if (ModelState.IsValid)
            {
                Utente utente = Utente.SelectUtenteById(u.Email, u.Password);
                if (utente != null)
                {//TODO da sistemare
                    FormsAuthentication.SetAuthCookie(utente.Email, false);
                    if(User.IsInRole("Passeggero")){
                        return RedirectToAction("HomePasseggero", "Passeggeri");
                    }else
                    {
                        return RedirectToAction("HomeAutista", "Autista");
                    }
                }else
                {
                    TempData["LoginError"] = "Utente non trovato";

                }
            } else
            {
                TempData["LoginError"] = "Inserisci email e password";
            }

            return View(u);
        }
    }
}
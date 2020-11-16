using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gästebuch.Models;

namespace GaesteBuch.Controllers
{
    public class LoginSysController : Controller
    {
        // GET: LoginSys
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autho(tbl_Admin admin, tbl_Log tbl_Log)
        {
            using (GästebuchEntities1 db = new GästebuchEntities1())
            {
                var X = admin.Passwort.GetHashCode();
                string y = X.ToString();
                var userInhalt = db.tbl_Admin.Where(x => x.Benutzername == admin.Benutzername && x.Passwort == y && admin.twoStep == "133").FirstOrDefault();
                if (userInhalt == null )
                {
                    admin.ErrorMsg = "Invalid Data";
                    return View("~/Views/Home/Contact.cshtml", admin);
                }
                else
                {
                    Session["userID"] = userInhalt.ID;
                    Session["userName"] = admin.Benutzername;
                    
                    
                    tbl_Log.ID = Guid.NewGuid();
                    tbl_Log.Vorgang = "Admin *"+ userInhalt.Benutzername +"* hat sich eingeloggt";
                    tbl_Log.Datum = DateTime.Now;
                    tbl_Log.IP_Adresse = Request.UserHostAddress;
                    db.tbl_Log.Add(tbl_Log);

                    db.SaveChanges();

                    return RedirectToAction("Index", "tbl_Eintrag");
                }
            }
        }
        public ActionResult LogOut(tbl_Log tbl_Log)
        {
            using (GästebuchEntities1 db = new GästebuchEntities1())
            {
                tbl_Log.ID = Guid.NewGuid();
                tbl_Log.Vorgang = "Admin *" + Session["userName"] + "* hat sich ausgeloggt";
                tbl_Log.Datum = DateTime.Now;
                tbl_Log.IP_Adresse = Request.UserHostAddress;
                db.tbl_Log.Add(tbl_Log);

                db.SaveChanges();
            }
                Session.Abandon();

            return RedirectToAction("Index", "Home");
        }
    }
}
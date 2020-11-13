using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gästebuch.Models;

namespace Gästebuch.Controllers

{
    public class tbl_EintragController : Controller
    {
        private GästebuchEntities1 db = new GästebuchEntities1();

        // GET: tbl_Eintrag
        public ActionResult Index()
        {
            var tbl_Eintrag = db.tbl_Eintrag.Include(t => t.tbl_Admin);
            return View(tbl_Eintrag.ToList());
        }

        // GET: tbl_Eintrag/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Eintrag tbl_Eintrag = db.tbl_Eintrag.Find(id);
            if (tbl_Eintrag == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Eintrag);
        }

        // GET: tbl_Eintrag/Create
        public ActionResult Create()
        {
            ViewBag.autorisiert_von = new SelectList(db.tbl_Admin, "ID", "Benutzername");
            return View();
        }

        // POST: tbl_Eintrag/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Vorname,Nachname,Detailtext,Verbesserungen,Datum,Bewertung")] tbl_Eintrag tbl_Eintrag, tbl_Log tbl_Log)
        {
           

            if (ModelState.IsValid)
            {
                tbl_Eintrag.ID = Guid.NewGuid();
                tbl_Eintrag.Datum = DateTime.Now; 
                db.tbl_Eintrag.Add(tbl_Eintrag);
                

                tbl_Log.ID = Guid.NewGuid();
                tbl_Log.Vorgang = "neuer Eintrag wurde erstellt";
                tbl_Log.Datum = DateTime.Now;
                tbl_Log.IP_Adresse = Request.UserHostAddress;
                db.tbl_Log.Add(tbl_Log);

                db.SaveChanges();
                return RedirectToAction("Index"); 
            }

            ViewBag.autorisiert_von = new SelectList(db.tbl_Admin, "ID", "Benutzername", tbl_Eintrag.autorisiert_von);
            return View(tbl_Eintrag);
            
        }



        // GET: tbl_Eintrag/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Eintrag tbl_Eintrag = db.tbl_Eintrag.Find(id);
            if (tbl_Eintrag == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Eintrag);
        }

        // POST: tbl_Eintrag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tbl_Eintrag tbl_Eintrag = db.tbl_Eintrag.Find(id);
            db.tbl_Eintrag.Remove(tbl_Eintrag);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
               
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Eintrag tbl_Eintrag = db.tbl_Eintrag.Find(id);
            if (tbl_Eintrag == null)
            {
                return HttpNotFound();
            }
            ViewBag.autorisiert_von = new SelectList(db.tbl_Admin, "ID", "Benutzername", tbl_Eintrag.autorisiert_von);
            return View(tbl_Eintrag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Vorname,Nachname,Detailtext,Verbesserungen,Datum,Bewertung,autorisiert_von")] tbl_Eintrag tbl_Eintrag, tbl_Log tbl_Log)
        {
            if (ModelState.IsValid)
            {
               db.Entry(tbl_Eintrag).State = EntityState.Modified;
          
                tbl_Log.ID = Guid.NewGuid();
                tbl_Log.Vorgang = "Admin *" + Session["userName"] + "* hat einen Eintrag autorisiert";
                tbl_Log.Datum = DateTime.Now;
                tbl_Log.IP_Adresse = Request.UserHostAddress;
                db.tbl_Log.Add(tbl_Log);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Eintrag);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

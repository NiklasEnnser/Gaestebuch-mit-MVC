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
    public class tbl_AdminController : Controller
    {
        private GästebuchEntities db = new GästebuchEntities();

        // GET: tbl_Admin
        public ActionResult Index()
        {
            return View(db.tbl_Admin.ToList());
        }

        // GET: tbl_Admin/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Admin tbl_Admin = db.tbl_Admin.Find(id);
            if (tbl_Admin == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Admin);
        }

        // GET: tbl_Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_Admin/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Benutzername,Passwort")] tbl_Admin tbl_Admin)
        {
            if (ModelState.IsValid)
            {
                tbl_Admin.ID = Guid.NewGuid();
                db.tbl_Admin.Add(tbl_Admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Admin);
        }

        // GET: tbl_Admin/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Admin tbl_Admin = db.tbl_Admin.Find(id);
            if (tbl_Admin == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Admin);
        }

        // POST: tbl_Admin/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Benutzername,Passwort")] tbl_Admin tbl_Admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Admin);
        }

        // GET: tbl_Admin/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Admin tbl_Admin = db.tbl_Admin.Find(id);
            if (tbl_Admin == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Admin);
        }

        // POST: tbl_Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tbl_Admin tbl_Admin = db.tbl_Admin.Find(id);
            db.tbl_Admin.Remove(tbl_Admin);
            db.SaveChanges();
            return RedirectToAction("Index");
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

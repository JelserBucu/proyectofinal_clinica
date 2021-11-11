using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Comprension_Lectora.Models;

namespace Comprension_Lectora.Controllers
{
    public class USERsController : Controller
    {
        private DESARROLLO_WEBEntities db = new DESARROLLO_WEBEntities();

        // GET: USERs
        public ActionResult Index()
        {
            var uSER = db.USER.Include(u => u.CSTATE).Include(u => u.PERSONAL_ADMISTRATIVO);
            return View(uSER.ToList());
        }

        // GET: USERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USER.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // GET: USERs/Create
        public ActionResult Create()
        {
            ViewBag.IdEstado = new SelectList(db.CSTATE, "Id", "Estado");
            ViewBag.IdEmpleado = new SelectList(db.PERSONAL_ADMISTRATIVO, "CodigoEmpleado", "Nombre");
            return View();
        }

        // POST: USERs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Passwords,IdEmpleado,IdEstado")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.USER.Add(uSER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEstado = new SelectList(db.CSTATE, "Id", "Estado", uSER.IdEstado);
            ViewBag.IdEmpleado = new SelectList(db.PERSONAL_ADMISTRATIVO, "CodigoEmpleado", "Nombre", uSER.IdEmpleado);
            return View(uSER);
        }

        // GET: USERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USER.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEstado = new SelectList(db.CSTATE, "Id", "Estado", uSER.IdEstado);
            ViewBag.IdEmpleado = new SelectList(db.PERSONAL_ADMISTRATIVO, "CodigoEmpleado", "Nombre", uSER.IdEmpleado);
            return View(uSER);
        }

        // POST: USERs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Passwords,IdEmpleado,IdEstado")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEstado = new SelectList(db.CSTATE, "Id", "Estado", uSER.IdEstado);
            ViewBag.IdEmpleado = new SelectList(db.PERSONAL_ADMISTRATIVO, "CodigoEmpleado", "Nombre", uSER.IdEmpleado);
            return View(uSER);
        }

        // GET: USERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USER.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: USERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            USER uSER = db.USER.Find(id);
            db.USER.Remove(uSER);
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

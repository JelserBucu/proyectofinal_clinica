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
    public class PERSONAL_ADMISTRATIVOController : Controller
    {
        private DESARROLLO_WEBEntities db = new DESARROLLO_WEBEntities();

        // GET: PERSONAL_ADMISTRATIVO
        public ActionResult Index()
        {
            var pERSONAL_ADMISTRATIVO = db.PERSONAL_ADMISTRATIVO.Include(p => p.PUESTO1);
            return View(pERSONAL_ADMISTRATIVO.ToList());
        }

        // GET: PERSONAL_ADMISTRATIVO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONAL_ADMISTRATIVO pERSONAL_ADMISTRATIVO = db.PERSONAL_ADMISTRATIVO.Find(id);
            if (pERSONAL_ADMISTRATIVO == null)
            {
                return HttpNotFound();
            }
            return View(pERSONAL_ADMISTRATIVO);
        }

        // GET: PERSONAL_ADMISTRATIVO/Create
        public ActionResult Create()
        {
            ViewBag.Puesto = new SelectList(db.PUESTO, "CodigoPuesto", "Nombre_Puesto");
            return View();
        }

        // POST: PERSONAL_ADMISTRATIVO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoEmpleado,Nombre,Apellido,Direccion,Email,Telefono,Puesto")] PERSONAL_ADMISTRATIVO pERSONAL_ADMISTRATIVO)
        {
            if (ModelState.IsValid)
            {
                db.PERSONAL_ADMISTRATIVO.Add(pERSONAL_ADMISTRATIVO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Puesto = new SelectList(db.PUESTO, "CodigoPuesto", "Nombre_Puesto", pERSONAL_ADMISTRATIVO.Puesto);
            return View(pERSONAL_ADMISTRATIVO);
        }

        // GET: PERSONAL_ADMISTRATIVO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONAL_ADMISTRATIVO pERSONAL_ADMISTRATIVO = db.PERSONAL_ADMISTRATIVO.Find(id);
            if (pERSONAL_ADMISTRATIVO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Puesto = new SelectList(db.PUESTO, "CodigoPuesto", "Nombre_Puesto", pERSONAL_ADMISTRATIVO.Puesto);
            return View(pERSONAL_ADMISTRATIVO);
        }

        // POST: PERSONAL_ADMISTRATIVO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoEmpleado,Nombre,Apellido,Direccion,Email,Telefono,Puesto")] PERSONAL_ADMISTRATIVO pERSONAL_ADMISTRATIVO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pERSONAL_ADMISTRATIVO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Puesto = new SelectList(db.PUESTO, "CodigoPuesto", "Nombre_Puesto", pERSONAL_ADMISTRATIVO.Puesto);
            return View(pERSONAL_ADMISTRATIVO);
        }

        // GET: PERSONAL_ADMISTRATIVO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONAL_ADMISTRATIVO pERSONAL_ADMISTRATIVO = db.PERSONAL_ADMISTRATIVO.Find(id);
            if (pERSONAL_ADMISTRATIVO == null)
            {
                return HttpNotFound();
            }
            return View(pERSONAL_ADMISTRATIVO);
        }

        // POST: PERSONAL_ADMISTRATIVO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PERSONAL_ADMISTRATIVO pERSONAL_ADMISTRATIVO = db.PERSONAL_ADMISTRATIVO.Find(id);
            db.PERSONAL_ADMISTRATIVO.Remove(pERSONAL_ADMISTRATIVO);
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

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
    public class HISTORIAL_CLINICOController : Controller
    {
        private DESARROLLO_WEBEntities db = new DESARROLLO_WEBEntities();

        // GET: HISTORIAL_CLINICO
        public ActionResult Index()
        {
            var hISTORIAL_CLINICO = db.HISTORIAL_CLINICO.Include(h => h.PACIENTE1);
            return View(hISTORIAL_CLINICO.ToList());
        }

        // GET: HISTORIAL_CLINICO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HISTORIAL_CLINICO hISTORIAL_CLINICO = db.HISTORIAL_CLINICO.Find(id);
            if (hISTORIAL_CLINICO == null)
            {
                return HttpNotFound();
            }
            return View(hISTORIAL_CLINICO);
        }

        // GET: HISTORIAL_CLINICO/Create
        public ActionResult Create()
        {
            ViewBag.Paciente = new SelectList(db.PACIENTE, "CodigoPasiente", "Nombre_Completo");
            return View();
        }

        // POST: HISTORIAL_CLINICO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoHistorial,Paciente,Examentes_Realizados,DiagnosticoPasiente,Fecha")] HISTORIAL_CLINICO hISTORIAL_CLINICO)
        {
            if (ModelState.IsValid)
            {
                db.HISTORIAL_CLINICO.Add(hISTORIAL_CLINICO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Paciente = new SelectList(db.PACIENTE, "CodigoPasiente", "Nombre_Completo", hISTORIAL_CLINICO.Paciente);
            return View(hISTORIAL_CLINICO);
        }

        // GET: HISTORIAL_CLINICO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HISTORIAL_CLINICO hISTORIAL_CLINICO = db.HISTORIAL_CLINICO.Find(id);
            if (hISTORIAL_CLINICO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Paciente = new SelectList(db.PACIENTE, "CodigoPasiente", "Nombre_Completo", hISTORIAL_CLINICO.Paciente);
            return View(hISTORIAL_CLINICO);
        }

        // POST: HISTORIAL_CLINICO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoHistorial,Paciente,Examentes_Realizados,DiagnosticoPasiente,Fecha")] HISTORIAL_CLINICO hISTORIAL_CLINICO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hISTORIAL_CLINICO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Paciente = new SelectList(db.PACIENTE, "CodigoPasiente", "Nombre_Completo", hISTORIAL_CLINICO.Paciente);
            return View(hISTORIAL_CLINICO);
        }

        // GET: HISTORIAL_CLINICO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HISTORIAL_CLINICO hISTORIAL_CLINICO = db.HISTORIAL_CLINICO.Find(id);
            if (hISTORIAL_CLINICO == null)
            {
                return HttpNotFound();
            }
            return View(hISTORIAL_CLINICO);
        }

        // POST: HISTORIAL_CLINICO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HISTORIAL_CLINICO hISTORIAL_CLINICO = db.HISTORIAL_CLINICO.Find(id);
            db.HISTORIAL_CLINICO.Remove(hISTORIAL_CLINICO);
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

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
    public class SOLICITUD_CISTASController : Controller
    {
        private DESARROLLO_WEBEntities db = new DESARROLLO_WEBEntities();

        // GET: SOLICITUD_CISTAS
        public ActionResult Index()
        {
            return View(db.SOLICITUD_CISTAS.ToList());
        }

        // GET: SOLICITUD_CISTAS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOLICITUD_CISTAS sOLICITUD_CISTAS = db.SOLICITUD_CISTAS.Find(id);
            if (sOLICITUD_CISTAS == null)
            {
                return HttpNotFound();
            }
            return View(sOLICITUD_CISTAS);
        }

        // GET: SOLICITUD_CISTAS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SOLICITUD_CISTAS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoCitas,Cita_Paciente,Fecha_Cita,Hora_Cita,Tipo_Consulta")] SOLICITUD_CISTAS sOLICITUD_CISTAS)
        {
            if (ModelState.IsValid)
            {
                db.SOLICITUD_CISTAS.Add(sOLICITUD_CISTAS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sOLICITUD_CISTAS);
        }

        // GET: SOLICITUD_CISTAS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOLICITUD_CISTAS sOLICITUD_CISTAS = db.SOLICITUD_CISTAS.Find(id);
            if (sOLICITUD_CISTAS == null)
            {
                return HttpNotFound();
            }
            return View(sOLICITUD_CISTAS);
        }

        // POST: SOLICITUD_CISTAS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoCitas,Cita_Paciente,Fecha_Cita,Hora_Cita,Tipo_Consulta")] SOLICITUD_CISTAS sOLICITUD_CISTAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sOLICITUD_CISTAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sOLICITUD_CISTAS);
        }

        // GET: SOLICITUD_CISTAS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOLICITUD_CISTAS sOLICITUD_CISTAS = db.SOLICITUD_CISTAS.Find(id);
            if (sOLICITUD_CISTAS == null)
            {
                return HttpNotFound();
            }
            return View(sOLICITUD_CISTAS);
        }

        // POST: SOLICITUD_CISTAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SOLICITUD_CISTAS sOLICITUD_CISTAS = db.SOLICITUD_CISTAS.Find(id);
            db.SOLICITUD_CISTAS.Remove(sOLICITUD_CISTAS);
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

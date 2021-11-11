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
    public class HABITACIONsController : Controller
    {
        private DESARROLLO_WEBEntities db = new DESARROLLO_WEBEntities();

        // GET: HABITACIONs
        public ActionResult Index()
        {
            var hABITACION = db.HABITACION.Include(h => h.DISPONIBLES);
            return View(hABITACION.ToList());
        }

        // GET: HABITACIONs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HABITACION hABITACION = db.HABITACION.Find(id);
            if (hABITACION == null)
            {
                return HttpNotFound();
            }
            return View(hABITACION);
        }

        // GET: HABITACIONs/Create
        public ActionResult Create()
        {
            ViewBag.Disponible = new SelectList(db.DISPONIBLES, "CodigoEstado", "Estado");
            return View();
        }

        // POST: HABITACIONs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoHabitacion,Habitacion1,Observaciones,Disponible")] HABITACION hABITACION)
        {
            if (ModelState.IsValid)
            {
                db.HABITACION.Add(hABITACION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Disponible = new SelectList(db.DISPONIBLES, "CodigoEstado", "Estado", hABITACION.Disponible);
            return View(hABITACION);
        }

        // GET: HABITACIONs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HABITACION hABITACION = db.HABITACION.Find(id);
            if (hABITACION == null)
            {
                return HttpNotFound();
            }
            ViewBag.Disponible = new SelectList(db.DISPONIBLES, "CodigoEstado", "Estado", hABITACION.Disponible);
            return View(hABITACION);
        }

        // POST: HABITACIONs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoHabitacion,Habitacion1,Observaciones,Disponible")] HABITACION hABITACION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hABITACION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Disponible = new SelectList(db.DISPONIBLES, "CodigoEstado", "Estado", hABITACION.Disponible);
            return View(hABITACION);
        }

        // GET: HABITACIONs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HABITACION hABITACION = db.HABITACION.Find(id);
            if (hABITACION == null)
            {
                return HttpNotFound();
            }
            return View(hABITACION);
        }

        // POST: HABITACIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HABITACION hABITACION = db.HABITACION.Find(id);
            db.HABITACION.Remove(hABITACION);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult habitacionesDisponibles() {
            List<HABITACION> lsDisponibles = new List<HABITACION>();

            lsDisponibles = (from d in db.HABITACION where d.Disponible == 1 select d).ToList<HABITACION>();

            List<object> ls = new List<object>();

            ls.Add(new { id = 0, nombre = " -- Seleccione Una --" });

            foreach (HABITACION h in lsDisponibles) {
                ls.Add(new { id = h.CodigoHabitacion, nombre = h.Habitacion1 });
            }

            return Json(ls, JsonRequestBehavior.AllowGet);

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

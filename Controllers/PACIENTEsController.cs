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
    public class PACIENTEsController : Controller
    {
        private DESARROLLO_WEBEntities db = new DESARROLLO_WEBEntities();

        // GET: PACIENTEs
        public ActionResult Index()
        {
            var pACIENTE = db.PACIENTE.Include(p => p.ENFERMEDAD1).Include(p => p.HABITACION1).Include(p => p.SEXO1);
            return View(pACIENTE.ToList());
        }

        // GET: PACIENTEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACIENTE pACIENTE = db.PACIENTE.Find(id);
            if (pACIENTE == null)
            {
                return HttpNotFound();
            }
            return View(pACIENTE);
        }

        // GET: PACIENTEs/Create
        public ActionResult Create()
        {
            List<HABITACION> lsHabitacionesDisponibles = new List<HABITACION>();

            lsHabitacionesDisponibles = (from d in db.HABITACION where d.Disponible == 1 select d).ToList<HABITACION>();

            lsHabitacionesDisponibles.Insert(0, new HABITACION { CodigoHabitacion = 0, Habitacion1 = " -- Seleccione Una --", Disponible = 1 });

            ViewBag.Enfermedad = new SelectList(db.ENFERMEDAD, "CodigoEnfermedad", "Nombre_Enfermedad");
            ViewBag.Habitacion = new SelectList(lsHabitacionesDisponibles, "CodigoHabitacion", "Habitacion1");
            ViewBag.Sexo = new SelectList(db.SEXO, "CodigoSexo", "Sexo1");
            return View();
        }

        // POST: PACIENTEs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoPasiente,Nombre_Completo,Sexo,Telefono,Enfermedad,Observaciones,Habitacion")] PACIENTE pACIENTE)
        {
            if (ModelState.IsValid)
            {
                db.PACIENTE.Add(pACIENTE);
                HABITACION habitacion = new HABITACION();

                habitacion = (from h in db.HABITACION where h.CodigoHabitacion == pACIENTE.Habitacion select h).FirstOrDefault();
                habitacion.Disponible = 2;

                db.Entry(habitacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Enfermedad = new SelectList(db.ENFERMEDAD, "CodigoEnfermedad", "Nombre_Enfermedad", pACIENTE.Enfermedad);
            ViewBag.Habitacion = new SelectList(db.HABITACION, "CodigoHabitacion", "Habitacion1", pACIENTE.Habitacion);
            ViewBag.Sexo = new SelectList(db.SEXO, "CodigoSexo", "Sexo1", pACIENTE.Sexo);
            return View(pACIENTE);
        }

        // GET: PACIENTEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACIENTE pACIENTE = db.PACIENTE.Find(id);
            if (pACIENTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.Enfermedad = new SelectList(db.ENFERMEDAD, "CodigoEnfermedad", "Nombre_Enfermedad", pACIENTE.Enfermedad);
            ViewBag.Habitacion = new SelectList(db.HABITACION, "CodigoHabitacion", "Habitacion1", pACIENTE.Habitacion);
            ViewBag.Sexo = new SelectList(db.SEXO, "CodigoSexo", "Sexo1", pACIENTE.Sexo);
            return View(pACIENTE);
        }

        // POST: PACIENTEs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoPasiente,Nombre_Completo,Sexo,Telefono,Enfermedad,Observaciones,Habitacion")] PACIENTE pACIENTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pACIENTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Enfermedad = new SelectList(db.ENFERMEDAD, "CodigoEnfermedad", "Nombre_Enfermedad", pACIENTE.Enfermedad);
            ViewBag.Habitacion = new SelectList(db.HABITACION, "CodigoHabitacion", "Habitacion1", pACIENTE.Habitacion);
            ViewBag.Sexo = new SelectList(db.SEXO, "CodigoSexo", "Sexo1", pACIENTE.Sexo);
            return View(pACIENTE);
        }

        // GET: PACIENTEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACIENTE pACIENTE = db.PACIENTE.Find(id);
            if (pACIENTE == null)
            {
                return HttpNotFound();
            }
            return View(pACIENTE);
        }

        // POST: PACIENTEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PACIENTE pACIENTE = db.PACIENTE.Find(id);
            db.PACIENTE.Remove(pACIENTE);
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

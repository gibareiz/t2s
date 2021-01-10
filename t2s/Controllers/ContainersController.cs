using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using t2s.Models;

namespace t2s.Controllers
{
    public class ContainersController : Controller
    {
        private t2sEntities2 db = new t2sEntities2();

        // GET: Containers
        public ActionResult Index()
        {
            var container = db.container.Include(c => c.idClienteContainer).Include(c => c.idCategoriaContainer).Include(c => c.idStatusContainer).Include(c => c.idTipoContainer);
            return View(container.ToList());
        }

        // GET: Containers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            container container = db.container.Find(id);
            if (container == null)
            {
                return HttpNotFound();
            }
            return View(container);
        }

        // GET: Containers/Create
        public ActionResult Create()
        {
            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "nmCliente");
            ViewBag.idCategoria = new SelectList(db.status.Where(s => s.idTipoStatus == 3), "idStatus", "nmStatus");
            ViewBag.idStatus = new SelectList(db.status.Where(s => s.idTipoStatus == 2), "idStatus", "nmStatus");
            ViewBag.idTipo = new SelectList(db.status.Where(s => s.idTipoStatus == 1), "idStatus", "nmStatus");
            return View();
        }

        // POST: Containers/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idContainer,idCliente,nmNumeroCntr,idTipo,idStatus,idCategoria")] container container)
        {

            var validation = db.container.Where(c => c.nmNumeroCntr == container.nmNumeroCntr);
            if (validation.Count() > 0)
            {
                ModelState.AddModelError("nmNumeroCntr", "Já existe cadastrado um container com esse nome!");
            }

            if (ModelState.IsValid)
            {
                db.container.Add(container);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "nmCliente", container.idCliente);
            ViewBag.idCategoria = new SelectList(db.status.Where(s => s.idTipoStatus == 3), "idStatus", "nmStatus");
            ViewBag.idStatus = new SelectList(db.status.Where(s => s.idTipoStatus == 2), "idStatus", "nmStatus");
            ViewBag.idTipo = new SelectList(db.status.Where(s => s.idTipoStatus == 1), "idStatus", "nmStatus");
            return View(container);
        }

        // GET: Containers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            container container = db.container.Find(id);
            if (container == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "nmCliente", container.idCliente);
            ViewBag.idCategoria = new SelectList(db.status.Where(s => s.idTipoStatus == 3), "idStatus", "nmStatus");
            ViewBag.idStatus = new SelectList(db.status.Where(s => s.idTipoStatus == 2), "idStatus", "nmStatus");
            ViewBag.idTipo = new SelectList(db.status.Where(s => s.idTipoStatus == 1), "idStatus", "nmStatus");
            return View(container);
        }

        // POST: Containers/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idContainer,idCliente,nmNumeroCntr,idTipo,idStatus,idCategoria")] container container)
        {
            var validation = db.container.Where(c => c.nmNumeroCntr == container.nmNumeroCntr && c.idContainer != container.idContainer);
            if (validation.Count() > 0)
            {
                ModelState.AddModelError("nmNumeroCntr", "Já existe cadastrado um container com esse nome!");
            }

            if (ModelState.IsValid)
            {
                db.Entry(container).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.cliente, "idCliente", "nmCliente", container.idCliente);
            ViewBag.idCategoria = new SelectList(db.status.Where(s => s.idTipoStatus == 3), "idStatus", "nmStatus");
            ViewBag.idStatus = new SelectList(db.status.Where(s => s.idTipoStatus == 2), "idStatus", "nmStatus");
            ViewBag.idTipo = new SelectList(db.status.Where(s => s.idTipoStatus == 1), "idStatus", "nmStatus");
            return View(container);
        }

        // GET: Containers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            container container = db.container.Find(id);
            if (container == null)
            {
                return HttpNotFound();
            }
            return View(container);
        }

        // POST: Containers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            container container = db.container.Find(id);
            db.container.Remove(container);
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

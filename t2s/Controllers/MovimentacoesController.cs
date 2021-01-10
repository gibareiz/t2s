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
    public class MovimentacoesController : Controller
    {
        private t2sEntities2 db = new t2sEntities2();

        // GET: Movimentacoes
        public ActionResult Index()
        {
            var movimentacao = db.movimentacao.Include(m => m.idContainerMovimentacao).Include(m => m.idStatusTipoMovimentacao).Include(m => m.idNavioMovimentacao);
            return View(movimentacao.ToList());
        }

        // GET: Movimentacoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            movimentacao movimentacao = db.movimentacao.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            return View(movimentacao);
        }

        // GET: Movimentacoes/Create
        public ActionResult Create()
        {

            ViewBag.idContainer = new SelectList(db.container, "idContainer", "nmNumeroCntr");
            ViewBag.idTipoMovimentacao = new SelectList(db.status.Where(s => s.idTipoStatus == 4), "idStatus", "nmStatus");
            ViewBag.idNavio = new SelectList(db.navio, "idNavio", "nmNavio");
            return View();
        }

        // POST: Movimentacoes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMovimentacao,idContainer,idNavio,idTipoMovimentacao,dtInicio,dtFim")] movimentacao movimentacao)
        {
            if (movimentacao.dtInicio >= movimentacao.dtFim)
            {
                ModelState.AddModelError("dtFim", "Data final da movimentação inválida!");
            }
            
            if (ModelState.IsValid)
            {
                db.movimentacao.Add(movimentacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idContainer = new SelectList(db.container, "idContainer", "nmNumeroCntr", movimentacao.idContainer);
            ViewBag.idTipoMovimentacao = new SelectList(db.status.Where(s => s.idTipoStatus == 4), "idStatus", "nmStatus");
            ViewBag.idNavio = new SelectList(db.navio, "idNavio", "nmNavio", movimentacao.idNavio);
            return View(movimentacao);
        }

        // GET: Movimentacoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            movimentacao movimentacao = db.movimentacao.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.idContainer = new SelectList(db.container, "idContainer", "nmNumeroCntr", movimentacao.idContainer);
            ViewBag.idTipoMovimentacao = new SelectList(db.status.Where(s => s.idTipoStatus == 4), "idStatus", "nmStatus");
            ViewBag.idNavio = new SelectList(db.navio, "idNavio", "nmNavio", movimentacao.idNavio);
            return View(movimentacao);
        }

        // POST: Movimentacoes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMovimentacao,idContainer,idNavio,idTipoMovimentacao,dtInicio,dtFim")] movimentacao movimentacao)
        {
            if (movimentacao.dtInicio >= movimentacao.dtFim)
            {
                ModelState.AddModelError("dtFim", "Data final da movimentação inválida!");
            }

            if (ModelState.IsValid)
            {
                db.Entry(movimentacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idContainer = new SelectList(db.container, "idContainer", "nmNumeroCntr", movimentacao.idContainer);
            ViewBag.idTipoMovimentacao = new SelectList(db.status.Where(s => s.idTipoStatus == 4), "idStatus", "nmStatus");
            ViewBag.idNavio = new SelectList(db.navio, "idNavio", "nmNavio", movimentacao.idNavio);
            return View(movimentacao);
        }

        // GET: Movimentacoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            movimentacao movimentacao = db.movimentacao.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            return View(movimentacao);
        }

        // POST: Movimentacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            movimentacao movimentacao = db.movimentacao.Find(id);
            db.movimentacao.Remove(movimentacao);
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

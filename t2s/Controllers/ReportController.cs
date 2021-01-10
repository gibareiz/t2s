using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using t2s.Models;

namespace Teste.Controllers
{
    public class ReportController : Controller
    {
        private t2sEntities2 db = new t2sEntities2();

        public ActionResult Index()
        {

            var result = db.vwMovimentacoes.ToList();

            var exportacoes = from mv in db.movimentacao
                          join ct in db.container on mv.idContainer equals ct.idContainer
                          join st in db.status on ct.idCategoria equals st.idStatus
                          where st.idStatus == 6
                          group new { st } by new { st.nmStatus } into qtde
                          select new { valor = qtde.Count() };

            var importacoes = from mv in db.movimentacao
                              join ct in db.container on mv.idContainer equals ct.idContainer
                              join st in db.status on ct.idCategoria equals st.idStatus
                              where st.idStatus == 5
                              group new { st } by new { st.nmStatus } into qtde
                              select new { valor = qtde.Count() };


            ViewBag.Exportacoes = (exportacoes.Count() == 0) ? 0 : exportacoes.FirstOrDefault().valor;
            ViewBag.Importacoes = (importacoes.Count() == 0) ? 0 : importacoes.FirstOrDefault().valor;

            return View(result);
        }
    }
}
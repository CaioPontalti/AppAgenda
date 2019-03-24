using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppAgenda.Models;
using AppAgenda.Controllers;

namespace AppAgenda.Controllers
{
    public class PessoasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private LogsController _c;

        public PessoasController()
        {
            _c = new LogsController();
        }

        // GET: Pessoas
        public ActionResult Index(string nome = null)
        {
            if (nome != null)
            {
                var contato = db.Clientes.Where(c => c.Nome.Contains(nome)).ToList();
                return View(contato);
            }
            return View(db.Clientes.ToList());
        }

        //[HttpGet]
        //public ActionResult BuscarContato()
        //{
        //    var contato = db.Clientes.Where(c => c.Nome.Contains(nome)).ToList();
        //    return View(contato);
            
        //}

        // GET: Pessoas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Clientes.Include(c => c.Telefones).FirstOrDefault(c => c.Id == id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // GET: Pessoas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome, Telefones, Emails")] Pessoa pessoa)
        {
            
            if (ModelState.IsValid)
            {
                db.Clientes.Add(pessoa);
                db.SaveChanges();

                _c.GravarLog(pessoa, TipoOperacao.CREATE);

                return RedirectToAction("Index");
            }

            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Clientes.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome, Telefones, Emails")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                if (pessoa.Telefones != null)
                {
                    //Percorre todos os telefones da lista
                    foreach (var tel in pessoa.Telefones)
                    {
                        //Se o id for > 0, já existe então atualiza.
                        if (tel.Id > 0)
                        {
                            db.Entry(tel).State = EntityState.Modified;
                        }
                        else // Se não, é um novo telefone
                        {
                            db.Entry(tel).State = EntityState.Added;
                        }
                    }
                }

                if (pessoa.Emails != null)
                {
                    //Percorre todos os eMails da lista
                    foreach (var mail in pessoa.Emails)
                    {
                        //Se o id for > 0, já existe então atualiza.
                        if (mail.Id > 0)
                        {
                            db.Entry(mail).State = EntityState.Modified;
                        }
                        else // Se não, é um novo eMail
                        {
                            db.Entry(mail).State = EntityState.Added;
                        }
                    }
                }


                db.Entry(pessoa).State = EntityState.Modified;
                db.SaveChanges();

                _c.GravarLog(pessoa, TipoOperacao.UPDATE);

                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Clientes.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pessoa pessoa = db.Clientes.Find(id);

            //Remove primeiro todos os telefones relacionados a ele.
            db.Telefones.RemoveRange(pessoa.Telefones);

            //Remove primeiro todos os E-mails relacionados a ele.
            db.Emails.RemoveRange(pessoa.Emails);

            //Depois efetivamente excluir o Contato.
            db.Clientes.Remove(pessoa);
            db.SaveChanges();

            _c.GravarLog(pessoa, TipoOperacao.DELETE);

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

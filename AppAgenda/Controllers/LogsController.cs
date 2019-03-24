using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppAgenda.Models;
using System.Data.Entity;


namespace AppAgenda.Controllers
{
    public class LogsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Logs.ToList());
        }
        
        public void GravarLog(Pessoa pessoa, TipoOperacao tipoOperacao)
        {
            TipoContato tipo = TipoContato.DEFAULT;

            MontarLog(pessoa, ref tipo);

            Log log = new Log()
            {
                PessoaId = pessoa.Id,
                TipoContato = tipo.ToString(),
                Operacao = tipoOperacao.ToString(), 
                Data = DateTime.Now.ToShortDateString()
            };

            db.Entry(log).State = EntityState.Added;
            db.SaveChanges();
        }

        private void MontarLog(Pessoa pessoa, ref TipoContato tipo)
        {
         
            if (pessoa.Telefones != null && pessoa.Emails != null)
            {
                tipo = TipoContato.AMBOS;
            }
            else if (pessoa.Telefones != null)
            {
                tipo = TipoContato.TELEFONE;
            }
            else if (pessoa.Emails != null)
            {
                tipo = TipoContato.EMAIL;
            }
            else
            {
                tipo = TipoContato.DEFAULT;
            }
        }

        
    }
}
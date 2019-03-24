using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppAgenda.Models;

namespace AppAgenda.Controllers
{
    public class TelefonesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        [HttpPost]
        public void DeletetarTelefone(int id)
        {
            Telefone telefone = db.Telefones.Find(id);
            db.Telefones.Remove(telefone);
            db.SaveChanges();
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

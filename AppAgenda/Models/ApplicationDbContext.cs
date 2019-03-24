using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AppAgenda.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() :base ("sqlConn")
        {
            //Mostra na janela Output a instrução SQL executada pelo Entity.
            Database.Log = commSql => System.Diagnostics.Debug.WriteLine(commSql);
        }

        public DbSet<Pessoa> Clientes { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
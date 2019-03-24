using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppAgenda.Models
{
    public class Pessoa
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        public virtual List<Telefone> Telefones { get; set; }

        public virtual List<Email> Emails { get; set; }
    }
}
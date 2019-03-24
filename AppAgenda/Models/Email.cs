using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppAgenda.Models
{
    public class Email
    {
        public int Id { get; set; }

        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Descricao { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AppAgenda.Models;

namespace AppAgenda.Models
{
    public class Log
    {

        public int Id { get; set; }

        [Display(Name = "IdContato")]
        public int PessoaId { get; set; }

        [Display(Name = "Tipo Contato")]
        public string TipoContato { get; set; }

        public string Operacao { get; set; }

        public string Data { get; set; }
    }
}
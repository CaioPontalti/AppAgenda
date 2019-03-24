using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppAgenda.Models
{
    public class Telefone
    {
        public int Id { get; set; }

        public int DDD { get; set; }

        public string Numero { get; set; }

        public TipoTelefone Tipo { get; set; }
    }
}
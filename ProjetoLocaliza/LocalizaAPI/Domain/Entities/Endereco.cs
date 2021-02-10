using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaAPI.Domain.Entities
{

    [Table("endereco")]
    public class Endereco
    {
        public int Cep { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public Cidade Cidade { get; set; }
        public Estado Estado { get; set; }
    }
}

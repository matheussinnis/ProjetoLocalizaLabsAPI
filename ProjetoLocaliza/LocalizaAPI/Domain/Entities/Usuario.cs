using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalizaAPI.Domain.Entities
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Documento { get; set; } //Serve para Matricula em caso de Operador, e CPF em caso de cliente

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }

        public Perfil Perfil { get; set; }
        public Endereco Endereco { get; set; }
    }
}

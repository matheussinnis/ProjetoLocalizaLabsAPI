using System;
using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.Entities
{
    public class User : BaseEntity
    {
        [Required]
        //Serve para Matricula em caso de Operador, e CPF em caso de cliente
        public string Document { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        // [JsonIgnore]
        public string Password { get; set; }

        public DateTime BirthdayDate { get; set; }

        public Address Address { get; set; }

        public UserType Type { get; set; }
    }
}

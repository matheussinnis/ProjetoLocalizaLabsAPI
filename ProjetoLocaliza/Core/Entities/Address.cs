using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Address : BaseEntity
    {
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Number { get; set; }
        public string Complement { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Address : BaseEntity
    {
        [Key]
        [ForeignKey("User")]
        public new Guid Id { get; set; }

        public virtual User User { get; set; }

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

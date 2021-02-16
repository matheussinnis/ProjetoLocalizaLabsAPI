using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Checklist : BaseEntity
    {
        [Key]
        [ForeignKey("Schedule")]
        public new Guid Id { get; set; }

        public virtual Schedule Schedule { get; set; }

        public bool Clean { get; set; }

        public bool TankFull { get; set; }

        public bool Dents { get; set; }

        public bool Scratches { get; set; }

        public int MissingLiters { get; set; }
    }
}

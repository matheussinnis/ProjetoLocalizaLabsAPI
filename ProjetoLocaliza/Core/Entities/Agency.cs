using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Agency : BaseEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public new Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }


    }
}

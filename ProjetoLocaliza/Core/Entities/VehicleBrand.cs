using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class VehicleBrand : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}

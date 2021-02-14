using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Quotation : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public new Guid Id { get; set; }

        public Guid VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        public decimal HourlyPrice { get; set; }

        public decimal Total =>
            ExpectedReturnDate.Subtract(ExpectedWithdrawalDate).Hours * HourlyPrice;

        [Required]
        public DateTime ExpectedWithdrawalDate { get; set; }

        [Required]
        public DateTime ExpectedReturnDate { get; set; }
    }
}

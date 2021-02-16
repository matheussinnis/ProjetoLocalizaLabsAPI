using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Schedule : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        public virtual Checklist Checklist { get; set; }

        public Guid? QuotationId { get; set; }
        public virtual Quotation Quotation { get; set; }

        [Required]
        public DateTime ExpectedWithdrawalDate { get; set; }

        public DateTime? RealWithdrawalDate { get; set; }

        [Required]
        public DateTime ExpectedReturnDate { get; set; }

        public DateTime? RealReturnDate { get; set; }

        public decimal HourlyPrice { get; set; }

        public decimal ExpectedSubtotal =>
            ExpectedReturnDate.Subtract(ExpectedWithdrawalDate).Hours * HourlyPrice;

        public decimal? SubtotalAfterInspection =>
            RealReturnDate == null || RealWithdrawalDate == null ? null : (
                ((DateTime) RealReturnDate).Subtract((DateTime) RealWithdrawalDate).Hours * HourlyPrice
                + (Checklist == null ? 0 : (
                    Checklist.Dents ? (decimal) 0.3 * ExpectedSubtotal : 0)
                    + (Checklist.Scratches ? (decimal) 0.3 * ExpectedSubtotal : 0)
                    + (Checklist.TankFull ? (decimal) 0.3 * ExpectedSubtotal : 0)
                    + (Checklist.Clean ? 0 : (decimal) 0.3 * ExpectedSubtotal)
                )
            );

        public decimal ExtraCosts { get; set; }

        public decimal ExpectedTotal => ExpectedSubtotal + ExtraCosts;

        public decimal? TotalAfterInspection => SubtotalAfterInspection + ExtraCosts;

        public bool InspectionDone { get; set; }
    }
}

using System.Threading.Tasks;
using Core.Entities;
using Domain.Interfaces;
using Infrastructure.Database.Interfaces;

namespace Domain.Services
{
    public class QuotationService : BaseService<Quotation>, IQuotationService
    {
        public QuotationService(ICrudRepository<Quotation> repository) : base(repository) {}

        public new async Task<Quotation> AddAsync(Quotation quotation)
        {
            quotation.HourlyPrice = quotation.Vehicle.HourlyPrice;
            await _repository.AddAsync(quotation);
            await _repository.SaveChangesAsync();
            return quotation;
        }

        public new async Task<Quotation> UpdateAsync(Quotation quotation)
        {
            quotation.HourlyPrice = quotation.Vehicle.HourlyPrice;
            _repository.Update(quotation);
            await _repository.SaveChangesAsync();
            return quotation;
        }
    }
}

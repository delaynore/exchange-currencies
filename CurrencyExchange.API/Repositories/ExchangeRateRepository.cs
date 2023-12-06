using CurrencyExchange.API.Data;
using CurrencyExchange.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.API.Repositories
{
    public sealed class ExchangeRateRepository(CurrencyExchangeDbContext context) : IExchangeRateRepository
    {
        private readonly CurrencyExchangeDbContext _context = context;

        public void Create(ExchangeRate rate)
        {
            _context.ExchangeRates.Add(rate);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.ExchangeRates.Where(x => x.Id.Equals(id)).ExecuteDelete();
            _context.SaveChanges();
        }

        public IQueryable<ExchangeRate> GetAll()
        {
            return _context.ExchangeRates.AsNoTracking();
        }

        public ExchangeRate? GetByCodes(string baseCode, string targetCode)
        {
            return _context.ExchangeRates
                .FirstOrDefault(x => x.BaseCurrencyId.Equals(baseCode) && x.TargetCurrencyId.Equals(targetCode));
        }

        public ExchangeRate? GeteById(int id)
        {
            return _context.ExchangeRates.Find(id);
        }

        public void Update(ExchangeRate rate)
        {
            var rateToUpdate = _context.ExchangeRates.Find(rate.Id);
            if (rateToUpdate == null) return;

            rateToUpdate.Rate = rate.Rate;
            rateToUpdate.BaseCurrencyId = rate.BaseCurrencyId;
            rateToUpdate.TargetCurrencyId = rate.TargetCurrencyId;

            _context.SaveChanges();
        }
    }
}

using CurrencyExchange.API.Data;
using CurrencyExchange.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.API.Repositories
{
    public sealed class ExchangeRateRepository(CurrencyExchangeDbContext context) : IExchangeRateRepository
    {
        private readonly CurrencyExchangeDbContext _context = context;

        public void Create(ExchangeRate exchangeRate)
        {
            _context.ExchangeRates.Add(exchangeRate);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.ExchangeRates.Where(x => x.Id.Equals(id)).ExecuteDelete();
            _context.SaveChanges();
        }

        public IQueryable<ExchangeRate> GetAll()
        {
            return _context.ExchangeRates
                .Include(x => x.BaseCurrency)
                .Include(x => x.TargetCurrency)
                .AsNoTracking();
        }

        public ExchangeRate? GetByCodes(string baseCode, string targetCode)
        {
            return _context.ExchangeRates
                .Include(x => x.BaseCurrency)
                .Include(x => x.TargetCurrency)
                .SingleOrDefault(x => x.BaseCurrencyId.Equals(baseCode) && x.TargetCurrencyId.Equals(targetCode));
        }

        public ExchangeRate? GetById(int id)
        {
            return _context.ExchangeRates
                .Include(x=>x.BaseCurrency)
                .Include(x=>x.TargetCurrency)
                .SingleOrDefault(x => x.Id.Equals(id));
        }

        public void Update(ExchangeRate exchangeRate)
        {
            _context.ExchangeRates.Update(exchangeRate);
            _context.SaveChanges();
        }
    }
}

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
            baseCode = baseCode.ToUpper();
            targetCode = targetCode.ToUpper();
            return _context.ExchangeRates
                .Include(x => x.BaseCurrency)
                .Include(x => x.TargetCurrency)
                .SingleOrDefault(x => 
                    x.BaseCurrency.Code.Equals(baseCode) && 
                    x.TargetCurrency.Code.Equals(targetCode));
        }

        public decimal? FindSimilarRate(string baseCode, string targetCode)
        {
            baseCode = baseCode.ToUpper();
            targetCode = targetCode.ToUpper();
            var query = _context.ExchangeRates
                .Include(x => x.BaseCurrency)
                .Include(x => x.TargetCurrency)
                .AsNoTracking();
            foreach (var a in query)
            {
                foreach (var b in query)
                {
                    if (b.BaseCurrency.Code == targetCode && b.TargetCurrency.Code == baseCode)
                        return 1 / b.Rate;
                    if (b.BaseCurrency.Code == baseCode && b.TargetCurrency.Code == targetCode)
                        return b.Rate;
                    if (a.Id != b.Id)
                    {
                        if(a.BaseCurrency.Code == baseCode && b.BaseCurrency.Code == targetCode &&
                            a.TargetCurrencyId == b.TargetCurrencyId)
                        {
                            return a.Rate / (1 / b.Rate);
                            // goal: a -> b
                            //a -> c
                            // b -> c
                        }
                        if(a.TargetCurrency.Code == baseCode && b.TargetCurrency.Code == targetCode &&
                           a.BaseCurrencyId == b.BaseCurrencyId)
                        {
                            return (1 / a.Rate) * b.Rate;
                            // goal: a -> b
                            //c -> a
                            // c -> b
                        }
                        if(a.BaseCurrency.Code == baseCode && 
                           a.TargetCurrencyId == b.BaseCurrencyId && 
                           b.BaseCurrencyId == a.TargetCurrencyId && 
                           b.TargetCurrency.Code == targetCode)
                        {
                            return a.Rate * b.Rate;
                            // goal: a -> b
                            //a -> c
                            // c -> b
                        }
                        if(a.BaseCurrency.Code == targetCode && 
                           a.TargetCurrencyId == b.BaseCurrencyId && 
                           b.BaseCurrencyId == a.TargetCurrencyId && 
                           b.TargetCurrency.Code == baseCode)
                        {
                            return 1 / (a.Rate * b.Rate);
                            // goal: a -> b
                            //b -> c
                            // c -> a
                        }
                    }
                }
            }

            return default;
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

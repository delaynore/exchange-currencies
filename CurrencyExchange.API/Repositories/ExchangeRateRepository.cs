using CurrencyExchange.API.Data;
using CurrencyExchange.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.API.Repositories
{
    public sealed class ExchangeRateRepository(CurrencyExchangeDbContext context) : IExchangeRateRepository
    {
        public IQueryable<ExchangeRate> GetAll()
        {
            return context.ExchangeRates
                .Include(x => x.BaseCurrency)
                .Include(x => x.TargetCurrency)
                .AsNoTracking();
        }

        public async Task<ExchangeRate?> GetByCodes(string baseCode, string targetCode)
        {
            baseCode = baseCode.ToUpper();
            targetCode = targetCode.ToUpper();
            return await context.ExchangeRates
                .Include(x => x.BaseCurrency)
                .Include(x => x.TargetCurrency)
                .SingleOrDefaultAsync(x => 
                    x.BaseCurrency.Code.Equals(baseCode) && 
                    x.TargetCurrency.Code.Equals(targetCode));
        }

        public async Task<decimal?> FindSimilarRate(string baseCode, string targetCode)
        {
            return await Task.Run(() =>
            {
                baseCode = baseCode.ToUpper();
                targetCode = targetCode.ToUpper();
                var query = context.ExchangeRates
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
                    if (a.Id == b.Id) continue;
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
            return default;
            });
        }
        
        
        public async Task<ExchangeRate?> GetById(int id)
        {
            return await context.ExchangeRates
                .Include(x=>x.BaseCurrency)
                .Include(x=>x.TargetCurrency)
                .SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task Create(ExchangeRate exchangeRate)
        {
            await context.ExchangeRates.AddAsync(exchangeRate);
            await context.SaveChangesAsync();
        }
        
        public async Task Update(ExchangeRate exchangeRate)
        {
            context.ExchangeRates.Update(exchangeRate);
            await context.SaveChangesAsync();
        }
        
        public async Task Delete(int id)
        {
            await context.ExchangeRates.Where(x => x.Id.Equals(id)).ExecuteDeleteAsync();
            await context.SaveChangesAsync();
        }
    }
}

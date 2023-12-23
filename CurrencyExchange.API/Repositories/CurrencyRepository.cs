using CurrencyExchange.API.Data;
using CurrencyExchange.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.API.Repositories
{
    public sealed class CurrencyRepository(CurrencyExchangeDbContext context) 
        : ICurrencyRepository
    {
        public IQueryable<Currency> GetAll()
        {
            return context.Currencies.AsNoTracking();
        }

        public async Task<Currency?> GetCurrencyByCode(string code)
        {
            code = code.ToUpper();
            return await context
                .Currencies
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Code.Equals(code));
        }

        public async Task<Currency?> GetCurrencyById(int id)
        {
            return await context
                .Currencies
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task CreateCurrency(Currency currency)
        {
            await context.Currencies.AddAsync(currency);
            await context.SaveChangesAsync();
        }
        
        public async Task UpdateCurrency(Currency currency)
        {
            context.Currencies.Update(currency);
            await context.SaveChangesAsync();
        }
        
        public async Task DeleteCurrency(int id)
        {
            await context.Currencies.Where(x=>x.Id.Equals(id)).ExecuteDeleteAsync();
            await context.SaveChangesAsync();
        }
    }
}

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

        public Currency? GetCurrencyByCode(string code)
        {
            code = code.ToUpper();
            return context.Currencies.AsNoTracking().FirstOrDefault(x => x.Code.Equals(code));
        }

        public Currency? GetCurrencyById(int id)
        {
            return context.Currencies.AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
        }

        public void CreateCurrency(Currency currency)
        {
            context.Currencies.Add(currency);
            context.SaveChanges();
        }
        
        public void UpdateCurrency(Currency currency)
        {
            context.Currencies.Update(currency);
            context.SaveChanges();
        }
        
        public void DeleteCurrency(int id)
        {
            context.Currencies.Where(x=>x.Id.Equals(id)).ExecuteDelete();
            context.SaveChanges();
        }
    }
}

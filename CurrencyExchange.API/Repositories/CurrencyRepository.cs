using CurrencyExchange.API.Data;
using CurrencyExchange.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.API.Repositories
{
    public sealed class CurrencyRepository(CurrencyExchangeDbContext context) 
        : ICurrencyRepository
    {
        private readonly CurrencyExchangeDbContext _context = context;

        public void CreateCurrency(Currency currency)
        {
            _context.Currencies.Add(currency);
            _context.SaveChanges();
        }

        public void DeleteCurrency(int id)
        {
            _context.Currencies.Where(x=>x.Id.Equals(id)).ExecuteDelete();
            _context.SaveChanges();
        }

        public IQueryable<Currency> GetAll()
        {
            return _context.Currencies.AsNoTracking();
        }

        public Currency? GetCurrencyByCode(string code)
        {
            return _context.Currencies.AsNoTracking().FirstOrDefault(x => x.Code.Equals(code));
        }

        public Currency? GetCurrencyById(int id)
        {
            return _context.Currencies.AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
        }

        public void UpdateCurrency(Currency currency)
        {
            try
            {
                _context.Currencies.Update(currency);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

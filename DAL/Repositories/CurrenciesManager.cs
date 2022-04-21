using AutoMapper;
using BLL.Dtos;
using BLL.Models;
using BOL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CurrenciesManager : ICurrenciesServices<CurrencyDto>
    {

        private readonly CurrencyDbContext _currency;
        private readonly IMapper _mapper;
        public CurrenciesManager(CurrencyDbContext currency, IMapper mapper)
        {
            _currency = currency;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CurrencyDto>> GetAllAsync()
        {
            var get_all_coins = await _currency.Currencies.Where(coin=>coin.IsActive).ToListAsync();
            return _mapper.Map<IEnumerable<CurrencyDto>>(get_all_coins);
        }

        public async Task<CurrencyDto> GetByNameAsync(string Name)
        {
            var get_coinbyname = await _currency.Currencies.SingleOrDefaultAsync(coin => coin.Name.ToLower() == Name.ToLower());
            if (get_coinbyname != null)
                return _mapper.Map<CurrencyDto>(get_coinbyname);

            return null;
        }

        public async Task<int> CreateAsync(CurrencyDto currency)
        {
            var set_coin = _mapper.Map<Currency>(currency);
           
            await _currency.Currencies.AddAsync(set_coin);
            return await _currency.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(CurrencyDto currency)
        {
            var get_coin = _mapper.Map<Currency>(currency);
            var data = await _currency.Currencies.SingleOrDefaultAsync(coin => coin.Name.ToLower() == get_coin.Name.ToLower());
            if (data != null)
            {
                data.IsActive = false;
                return await _currency.SaveChangesAsync();
            }
            return 0;

        }



        public async Task<CurrencyDto> EditAsync(int Id, CurrencyDto currency)
        {
            var get_currency = await _currency.Currencies.FindAsync(Id);
            get_currency.Name = currency.Name;
            get_currency.Sign = currency.Sign;
            await _currency.SaveChangesAsync();
            return currency;
        }


    }
}

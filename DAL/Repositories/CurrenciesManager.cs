using AutoMapper;
using BLL.Dtos;
using BLL.Models;
using BOL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CurrenciesManager : ICurrenciesServices
    {

        private readonly CurrencyDbContext _currency;
        private readonly IMapper _mapper;
        public CurrenciesManager(CurrencyDbContext currency, IMapper mapper)
        {
            _currency = currency;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CurrencyExchangebyRateDto>> GetAllAsync()
        {
            try
            {

                var get_all_coins = _currency.Currencies.Where(coin => coin.IsActive);
                var getbyNameAndSignAndRate = await get_all_coins.Select(currencyHistory => new CurrencyExchangebyRateDto
                {
                    Name = currencyHistory.Name,
                    Sign = currencyHistory.Sign,
                    Rate = currencyHistory.ExchangeHistories.OrderBy(ex => ex.ExchangeDate).LastOrDefault().Rate
                }).ToListAsync();
                return getbyNameAndSignAndRate ?? null;
            }
            catch (Exception)
            {
                throw;
            }


        }

        public async Task<CurrencyExchangebyRateDto> GetByNameAsync(string Name)
        {
            var get_coin = await _currency.Currencies.SingleOrDefaultAsync(coin => coin.Name.ToLower() == Name.ToLower());
            if (get_coin != null)
            {
                return new CurrencyExchangebyRateDto
                {
                    Name = get_coin.Name,
                    Sign = get_coin.Sign,
                    Rate = get_coin.ExchangeHistories
                            .OrderBy(rate => rate.Rate).LastOrDefault().Rate
                };
            }
            return null;
        }
        public async Task<int> CreateAsync(CurrencyExchangeValueDto currencyExchange)
        {
            bool Istrue = await _currency.Currencies.AllAsync(coin => coin.Name != currencyExchange.Name);
            if (Istrue)
            {
                try
                {
                    var currency = new Currency
                    {
                        Name = currencyExchange.Name,
                        Sign = currencyExchange.Sign
                    };
                    await _currency.Currencies.AddAsync(currency);
                    var Exchange = new ExchangeHistory
                    {
                        Currency = currency,
                        Rate = (1 / currencyExchange.Value)
                    };
                    await _currency.ExchangeHistories.AddAsync(Exchange);
                    return await _currency.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            return 0;
        }

        public async Task<int> DeleteAsync(string Name)
        {
            if (Name != null)
            {
                var coin = await _currency.Currencies.SingleOrDefaultAsync(coin => coin.Name.ToLower() == Name.ToLower());
                if (coin != null)
                {
                    coin.IsActive = false;
                    return await _currency.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<ExchangeHistory> EditAsync(int Id, CurrencyExchangeValueDto currencyExchangeDto)
        {
            try
            {
                var coin = await _currency.Currencies.FindAsync(Id);
                coin.Name = currencyExchangeDto.Name;
                coin.Sign = currencyExchangeDto.Sign;
                var exchangeHistory = new ExchangeHistory
                {
                    Currency = coin,
                    Rate = (1 / currencyExchangeDto.Value)
                };
                await _currency.ExchangeHistories.AddAsync(exchangeHistory);
                await _currency.SaveChangesAsync();
                return exchangeHistory;
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }

        public IEnumerable<CurrencyExchangebyRateDto> GetHighest(int Num)
        {

            var get_Last_rate = GetAllAsync().Result.OrderByDescending(order => order.Rate);
            if (get_Last_rate.Count() >= Num)
                return get_Last_rate.Take(Num);
            return null;
        }


        public IEnumerable<CurrencyExchangebyRateDto> GetLowest(int Num)
        {
            var get_Last_rate = GetAllAsync().Result.OrderBy(order => order.Rate);
            if (get_Last_rate.Count() >= Num)
                return get_Last_rate.Take(Num);
            return null;
        }

        public IEnumerable<CurrencyExchangebyRateDto> GetMostNImprovedCurrencies(DateTime from, DateTime to, int Num)
        {
            //  Test Date 
            //var IsSelected = _currency.ExchangeHistories.ToList().GroupBy(CurId => CurId.CurID).Any(date => date.FirstOrDefault().ExchangeDate >= from && date.LastOrDefault().ExchangeDate <= to);
            // Test Date
            //if (IsSelected)
            //{
                IEnumerable<IEnumerable<ExchangeHistory>> CurrenicesFromTo = _currency.Currencies.Select(related => related.ExchangeHistories).Where(exchange => exchange.OrderBy(date => date.ExchangeDate).Any(Dateofrate => Dateofrate.ExchangeDate == from || Dateofrate.ExchangeDate == to));
                var val = CurrenicesFromTo.Where(b => b.FirstOrDefault().Rate - b.LastOrDefault().Rate > 0).Select(obj => obj.FirstOrDefault());
            return val.Select(currency => new CurrencyExchangebyRateDto
            {
                Name = currency.Currency.Name,
                Sign = currency.Currency.Sign,
                Rate = currency.Rate
            }).Take(Num) ?? null;
           

        }

    }




}

























/*
 * .GroupBy(i => i.CurID).Select(Ex => Ex.OrderBy(data => data.ExchangeDate).LastOrDefault()).Reverse().ToListAsync();
 * .Select(ex => ex.ExchangeHistories.OrderBy(b => b.ExchangeDate).LastOrDefault()).AsEnumerable().Reverse();
 * */











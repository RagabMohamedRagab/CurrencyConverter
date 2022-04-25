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
                            .OrderBy(rate => rate.ExchangeDate).LastOrDefault().Rate
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

        public async Task<int> DeleteAsync(int Id)
        {

            var coin = await _currency.Currencies.FindAsync(Id);
            if (coin != null)
            {
                coin.IsActive = false;
                return await _currency.SaveChangesAsync();
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

        public async Task<string> ConvertAmountAsync(ConverterDto converter)
        {

            var first_rate = GetLastRate(converter.FromCurrency);
            var second_rate = GetLastRate(converter.ToCurrency);
            var sign_coin = await _currency.Currencies.FindAsync(converter.ToCurrency);
            if (first_rate != 0 && second_rate != 0)
            {
                var ratio_currency = (converter.Value / first_rate);
                return $"{ratio_currency * second_rate} {sign_coin.Sign}";
            }
            return null;
        }
        public decimal GetLastRate(int Id)
        {
            var rate = _currency.Currencies.Where(coin => coin.IsActive).SingleOrDefault(coin => coin.Id == Id).ExchangeHistories.OrderBy(date => date.ExchangeDate).LastOrDefault().Rate;
            return rate;
        }
        public IList<Currency> GetAllCurrenices()
        {
            return _currency.Currencies.Where(coin => coin.IsActive).ToList();
        }
        public IList<DateTime> GetAllDates(IEnumerable<Currency> currenies)
        {
            var all_Dates = currenies.SelectMany(coin => coin.ExchangeHistories).OrderBy(date => date.ExchangeDate).Select(date => date.ExchangeDate.Date).ToList();
            return all_Dates;
        }
    //    public IList<ExchangeHistory> GetCurrency(int CurId,DateTime from,DateTime to)
    //    {
    //        var adat = GetAllCurrenices().SingleOrDefault(currency => currency.Id == CurId).ExchangeHistories.Where(date=>date.ExchangeDate.Date==from.Date).OrderBy(date => date.ExchangeDate).LastOrDefault();
    //        var ad3at = GetAllCurrenices().SingleOrDefault(currency => currency.Id == CurId).ExchangeHistories.Where(date => date.ExchangeDate.Date == to.Date).OrderBy(date => date.ExchangeDate).LastOrDefault();
    //        if()
    //    }
    //    public IEnumerable<CurrencyExchangebyRateDto> GetMostNImproved(ChangeOfRateDto changeOfRate)
    //    {

    //        //var get_all_dates = GetAllDates(GetAllCurrenices());
    //        //if (get_all_dates.Contains(changeOfRate.From.Date) && get_all_dates.Contains(changeOfRate.To.Date))
    //        //{
    //        //    var get_all_Currenies = GetAllCurrenices().Select(currenies => new CurrencyExchangebyRateDto 
    //        //    {
    //        //     Name=currenies.Name,
    //        //     Sign=currenies.Sign,
    //        //     Rate=GetCurrency(currenies.Id,changeOfRate.From,changeOfRate.To)
                
    //        //    });

    //        //}

    //        //return null;
    //    }
    //}









}



























/*
 * .GroupBy(i => i.CurID).Select(Ex => Ex.OrderBy(data => data.ExchangeDate).LastOrDefault()).Reverse().ToListAsync();
 * .Select(ex => ex.ExchangeHistories.OrderBy(b => b.ExchangeDate).LastOrDefault()).AsEnumerable().Reverse();
 * */











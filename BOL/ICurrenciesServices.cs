using BLL.Dtos;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public interface ICurrenciesServices
    {
        Task<IEnumerable<CurrencyExchangebyRateDto>> GetAllAsync();
        Task<CurrencyExchangebyRateDto> GetByNameAsync(string Name);
       
        Task<int> CreateAsync(CurrencyExchangeValueDto currencyExchange);
        Task<ExchangeHistory> EditAsync(int Id, CurrencyExchangeValueDto currencyExchangeHistoryDto);
        Task<int> DeleteAsync(string Name);

      IEnumerable<CurrencyExchangebyRateDto> GetHighest(int Num);
      IEnumerable<CurrencyExchangebyRateDto> GetLowest(int Num);
      IEnumerable<CurrencyExchangebyRateDto> GetMostNImprovedCurrencies(DateTime from,DateTime to, int Num);
    }
}








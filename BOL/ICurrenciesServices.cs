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
        Task<int> DeleteAsync(int Id);

      IEnumerable<CurrencyExchangebyRateDto> GetHighest(int Num);
      IEnumerable<CurrencyExchangebyRateDto> GetLowest(int Num);
        IEnumerable<CurrencyDto> GetMostNImprovedAndLowest(ChangeOfRateDto changeOfRate,string order);
       Task<string> ConvertAmountAsync(ConverterDto converter);
    }
}








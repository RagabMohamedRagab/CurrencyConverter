using AutoMapper;
using BLL.Dtos;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapper
{
   public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<CurrencyDto, Currency>().ReverseMap();
            CreateMap<CurrencyExchangebyRateDto, ExchangeHistory>().ReverseMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
   public class CurrencyExchangeValueDto
    {
        public string Name { get; set; }
        public string Sign { get; set; }
        public decimal Value { get; set; }
    }
}

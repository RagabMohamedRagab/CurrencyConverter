using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
   public class ConverterDto
    {
        public int FromCurrency { get; set; }
        public int ToCurrency { get; set; }
        public decimal Value { get; set; }
        
    }
}

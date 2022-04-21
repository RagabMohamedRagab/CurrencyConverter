using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
   public class ExchangeHistoryDto
    {
        [DataType(DataType.DateTime)]
        public DateTime ExchangeDate { get; set; }
        public double Rate { get; set; }
    }
}

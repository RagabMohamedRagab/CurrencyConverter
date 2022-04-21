using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ExchangeHistory
    {
        public int ID { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ExchangeDate { get; set; } = DateTime.Now;
        public double Rate { get; set; }
        public virtual Currency Currency{ get; set; }
    }
}

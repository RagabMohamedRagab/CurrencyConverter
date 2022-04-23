using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Column(TypeName ="decimal(4,2)")]
        public double Rate { get; set; }
        [ForeignKey("Currency")]
        public int? CurID { get; set; }
        public virtual Currency Currency{ get; set; }
    }
}

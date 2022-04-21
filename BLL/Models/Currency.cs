using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
   public class Currency
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Sign { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual ICollection<ExchangeHistory> ExchangeHistories { get; set; }
    }
}

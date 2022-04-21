using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
   public interface IRegisterServices<T> where T:class
    {
        Task<int> CreateAsync(T admin); 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
   public interface ILoginServices<T> where T:class
    {
        Task<int> LoginAsync(T login);
    }
}

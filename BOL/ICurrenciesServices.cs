using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public interface ICurrenciesServices<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByNameAsync(string Name);
        Task<int> CreateAsync(T currency);
        Task<T> EditAsync(int Id, T currency);
        Task<int> DeleteAsync(T currency);

    }
}



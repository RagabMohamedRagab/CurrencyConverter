using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
   public interface IRegisterAndLoginServices: ILoginServices<LoginDto>, IRegisterServices<RegisterDTO>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextApi.Services
{
    public interface IService<T> where T: class
    {
        T GetNext();
    }
}

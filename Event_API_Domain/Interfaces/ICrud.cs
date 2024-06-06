using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_API_Domain.Interfaces
{
    public interface ICrud<T, TReturn, TCreate> 
        where T : class
    {
        int Create(TCreate entity);
        List<T> GetAll();
        TReturn GetById(int id);
        void Update(T entity);
        void Delete(int id);

    }
}

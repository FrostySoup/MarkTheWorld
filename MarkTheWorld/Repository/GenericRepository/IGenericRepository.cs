using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.GenericRepository
{
    public interface IGenericRepository
    {
        List<T> GetAll<T>();

        T GetOne<T>(string id);

        T Add<T>(T objectName);

        T Delete<T>(string id);

        T Edit<T>(T objectName);

    }
}
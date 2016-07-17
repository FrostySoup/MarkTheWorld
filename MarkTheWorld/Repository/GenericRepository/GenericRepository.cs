using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.GenericRepository
{
    public class GenericRepository : IGenericRepository
    {
        public List<T> GetAll<T>()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var returnedObj = session
                    .Query<T>()
                    .Take(10000)
                    .ToList();
                return returnedObj;
            }
        }

        public T GetOne<T>(string id)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                T oneObject = session.Load<T>(id);

                return oneObject;
            }
        }

        public T Add<T>(T addObject)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(addObject);
                session.SaveChanges();
            }
            return addObject;
        }

        public T Delete<T>(string id)
        {
            T deletedObject;
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var objectRemoved = session.Load<T>(id);
                if (objectRemoved != null)
                {
                    session.Delete(objectRemoved);
                    session.SaveChanges();
                    deletedObject = objectRemoved;
                }
                else
                {
                    throw new Exception("Something went wrong... please try again");
                }
            }
            return deletedObject;
        }

        public T Edit<T>(T objectToEdit)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(objectToEdit);
                session.SaveChanges();
            }
            return objectToEdit;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data.Database;

namespace Repository
{
    public class RavenDbRepository<T> where T : class
    {

        public static List<T> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                var list = new List<T>();
                try
                {
                    list = session.Query<T>()
                        .Where(predicate)
                        .ToList();
                }
                catch 
                {

                }
                return list;
            }
        }

        public static List<T> GetAllItems()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {

                return session.Query<T>()
                    .ToList();
            }
        }

        public static T GetItemAsync(Expression<Func<T, bool>> predicate)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                return session.Query<T>()
                    .Where(predicate)
                    .FirstOrDefault();
            }   
        }

        public static List<T> GetItemAsyncPages(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> orderBy, int howMany, int skipNum)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                return session.Query<T>()
                    .Where(predicate)
                    .Where(predicate)
                    .OrderBy(orderBy)
                    .Skip(skipNum)
                    .Take(howMany)
                    .ToList();
            }           
        }

        public static T UpdateItemAsync(string id, T item)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(item);
                session.SaveChanges();
                return item;
            }
        }

        public static T GetItemAsyncByID(string id)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                return session.Load<T>(id);
            }
        }

        public static T CreateItemAsync(T newItem)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(newItem);
                session.SaveChanges();
                return newItem;
            }
        }
    }
}
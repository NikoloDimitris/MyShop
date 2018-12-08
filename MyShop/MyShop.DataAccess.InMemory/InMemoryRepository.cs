using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    //defining <> a place holder, we create the generic class, it is a referencethrought the rest of the code
    public class InMemoryRepository <T> where T : BaseEntity     
    {
        ObjectCache cache = MemoryCache.Default;
        //next we want the internal list of T
        List<T> items;
        string className;

        //constructor to initialize oyr repository
        public InMemoryRepository() {
            className = typeof(T).Name;     //getting the actual name of our class
            //then initialise items
            items = cache[className] as List<T>;
            if (items == null) {
                items = new List<T>();
            }
        }

        //commit function for storing our items in memory
        public void Commit() {
            cache[className] = items;
        }


        //& now we want the standard methods for the insert, update, edit, delete etc..
        public void Insert(T t) {
            items.Add(t);
        }

        public void Update(T t) {
            T tToUpdate = items.Find(i => i.Id == t.Id);

            if (tToUpdate != null) {
                tToUpdate = t;
            }
            else {
                throw new Exception(className + " Not Found");
            }
        }

        public T Find (string Id) {
            T t = items.Find(i => i.Id == Id);
            if (t != null) {
                return t;
            }
            else {
                throw new Exception(className + "Not Found");
            }
        }

        public IQueryable<T> Collection() {
            return items.AsQueryable();
        }

        public void Delete (string Id) {
            T tToDelete= items.Find(i => i.Id == Id);

            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + " Not Found");
            }
        }
    }
}

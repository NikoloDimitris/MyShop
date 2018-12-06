using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        //create an internal list of products
        List<Product> products; // = new List<Product>(); to afairei dioti to prosthetei parakatw, line22

        //create a constructor
        public ProductRepository() {

            products = cache["products"] as List<Product>;
            if(products==null) {
                products = new List<Product>();
                //whenever we launch this, we are going to try to look in the cache to see if there is a cache call products
            }     
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        //create an individual end point to insert, delete, find, edit, return with list fuctionallity
        public void Insert (Product p) {
            products.Add(p);
        }

        public void Update (Product product) {              //take in a product
            Product productToUpdate = products.Find(p => p.Id == product.Id);   //find the product we want to update

            if (productToUpdate != null) {      //make sure we have a product
                productToUpdate = product;      //if true we copy the information from the product we send in
            }
            else {
                throw new Exception("Product no found");
            }
        }

        public Product Find (string Id) {
            Product product = products.Find(p => p.Id == Id);   

            if (product != null)
            {      
                return product;      
            }
            else
            {
                throw new Exception("Product no found");
            }
        }


        //return a list of products queried!!
        public IQueryable<Product> Collection() {
            return products.AsQueryable();
        }

        public void Delete (string Id) {
            Product productToDelete = products.Find(p => p.Id == Id);   

            if (productToDelete != null)
            {
                products.Remove(productToDelete);      
            }
            else
            {
                throw new Exception("Product no found");
            }
        }


    }
}

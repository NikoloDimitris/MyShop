using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        //create an internal list of products
        List<ProductCategory> productsCategories; // = new List<Product>(); to afairei dioti to prosthetei parakatw, line22

        //create a constructor
        public ProductCategoryRepository()
        {

            productsCategories = cache["productsCategories"] as List<ProductCategory>;
            if (productsCategories == null)
            {
                productsCategories = new List<ProductCategory>();
                //whenever we launch this, we are going to try to look in the cache to see if there is a cache call products
            }
        }

        public void Commit()
        {
            cache["productsCategories"] = productsCategories;
        }

        //create an individual end point to insert, delete, find, edit, return with list fuctionallity
        public void Insert(ProductCategory p)
        {
            productsCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {              //take in a product
            ProductCategory productCategoryToUpdate = productsCategories.Find(p => p.Id == productCategory.Id);   //find the product we want to update

            if (productCategoryToUpdate != null)
            {      //make sure we have a product
                productCategoryToUpdate = productCategory;      //if true we copy the information from the product we send in
            }
            else
            {
                throw new Exception("Product Category no found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productsCategories.Find(p => p.Id == Id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category no found");
            }
        }


        //return a list of products queried!!
        public IQueryable<ProductCategory> Collection()
        {
            return productsCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productsCategories.Find(p => p.Id == Id);

            if (productCategoryToDelete != null)
            {
                productsCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category no found");
            }
        }

    }
}

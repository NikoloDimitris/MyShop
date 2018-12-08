using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//add for using references to our models & in memory repository
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        //at th top of the controller we need to create an instance of the product repository
        InMemoryRepository<Product> context;
        //to load our product categories from the database
        InMemoryRepository<ProductCategory> productCategories;    //& we need to initializ that with the constructor

        //we need to create a construct for our controller for initiating the repository
        public ProductManagerController() {
            context = new InMemoryRepository<Product>();
            productCategories = new InMemoryRepository<ProductCategory>();
        }
         
        // GET: ProductManager
        public ActionResult Index() {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        //add a method that create our product
        public ActionResult Create() {
            //for creating the list categories, first create a reference to the product manager view model
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();      //initially an empty product
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        //the second page will be to have those details posted
        [HttpPost]
        public ActionResult Create (Product product) {
            if (!ModelState.IsValid) {
                return View(product);
            }
            else {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        //add the edit code
        public ActionResult Edit(string Id) {
            Product product = context.Find(Id);
            if (product == null) {
                return HttpNotFound();
            }
            else {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();

                return View(viewModel);
            }
        }

        //next edit page which will take in the product we are editing
        [HttpPost]
        public ActionResult Edit(Product product, string Id) {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid) {
                    return View(product);
                }
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }

        }

        //create delete method
        public ActionResult Delete (string Id) {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id) {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }

    }
}
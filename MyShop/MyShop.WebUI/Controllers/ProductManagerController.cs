using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
//add for using references to our models & in memory repository
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]        //Lecture 89, 09:30++, security reasons of admin
    public class ProductManagerController : Controller
    {
        //at th top of the controller we need to create an instance of the product repository
        //InMemoryRepository<Product> context;    //change it at Lecture 65, 08:18
        IRepository<Product> context;

        //to load our product categories from the database
        /*InMemoryRepository<ProductCategory> productCategories; */   //& we need to initializ that with the constructor //change it at Lecture 65, 08:18
        IRepository<ProductCategory> productCategories;

        //we need to create a construct for our controller for initiating the repository
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext) {
            //context = new InMemoryRepository<Product>();      changed lines 26.27 at lecture 65,10:54
            //productCategories = new InMemoryRepository<ProductCategory>();
            context = productContext;
            productCategories = productCategoryContext;
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
        public ActionResult Create (Product product, HttpPostedFileBase file) {
            if (!ModelState.IsValid) {
                return View(product);
            }
            else {

                if (file != null) {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }

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
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file) {
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

                if (file != null) {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                //productToEdit.Image = product.Image;  //due to HttpPostedFileBase, Lecture 69, 12:10
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
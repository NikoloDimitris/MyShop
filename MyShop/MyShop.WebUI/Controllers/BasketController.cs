using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IRepository<Customer> customers;        //added at lecture 86, 01:30
        IBasketService basketService;
        IOrderService orderService;
        //create a constructor to allow to inject in the basket service
        public BasketController(IBasketService BasketService, IOrderService OrderService, IRepository<Customer> Customers) {
            this.basketService = BasketService;
            this.orderService = OrderService;
            this.customers = Customers;
        }


        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket (string Id) {
            basketService.AddToBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string Id)
        {       //remove the basket item id
            basketService.RemoveFromBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        //Partial view
        public PartialViewResult BasketSummary() {
            var basketSummary = basketService.GetBacketSummary(this.HttpContext);

            return PartialView (basketSummary);
        }

        [Authorize]         //because we need to ensure that the user is logged in
        //adding now 3 ended points for the OrderService
        public ActionResult Checkout()
        {               //retrieve the customer from the database, lecture 86, 02:22
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);

            if (customer != null)                   //the concept is that if the customer exits, we create a new order.....
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    City = customer.City,
                    State = customer.State,
                    Street = customer.Street,
                    FirstName = customer.FirstName,
                    Surname = customer.LastName,
                    ZipCode = customer.ZipCode
                };

                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }
           
        }

        [HttpPost]      //reminder why we have the HttpPost, cause the above method is the same : checkout
        [Authorize]
        public ActionResult Checkout(Order order)
        {

            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";        //update the order status
            order.Email = User.Identity.Name;           //here is to check that the user is logged in!!!!!!!!!!!!!!1

            //process payment

            order.OrderStatus = "Payment Processed";
            orderService.CreateOrder(order, basketItems);
            basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("Thankyou", new { OrderId = order.Id });
        }

        public ActionResult ThankYou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }

    }
}
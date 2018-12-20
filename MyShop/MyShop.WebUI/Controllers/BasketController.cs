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
        IBasketService basketService;
        IOrderService orderService;
        //create a constructor to allow to inject in the basket service
        public BasketController(IBasketService BasketService, IOrderService OrderService) {
            this.basketService = BasketService;
            this.orderService = OrderService;
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

        //adding now 3 ended points for the OrderService
        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]      //reminder why we have the HttpPost, cause the above method is the same : checkout
        public ActionResult Checkout(Order order)
        {

            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";        //update the order status

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
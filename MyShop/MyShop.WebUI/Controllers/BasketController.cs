using MyShop.Core.Contracts;
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
        //create a constructor to allow to inject in the basket service
        public BasketController(IBasketService BasketService) {
            this.basketService = BasketService;
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

    }
}
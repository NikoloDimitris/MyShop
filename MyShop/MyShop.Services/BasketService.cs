using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;
        //when we are writing in cookingg we use a string to identify a particular cookie->enforce consistency, Lecture 74,04:50
        public const string BasketSessionName = "eCommerceBasket";  //const->because this string is not going to update within our code

        public BasketService(IRepository<Product> ProductContext, IRepository<Basket> BasketContext) {
            this.basketContext = BasketContext;
            this.productContext = ProductContext;
        }

        //internal method, we want to use thiss method from various places within our service
        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull) {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName); //read the cookie

            Basket basket = new Basket();       //create a new basket

            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))        //double check
                {
                    basket = basketContext.Find(basketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }

            return basket;
        }


        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);       //expiration
            httpContext.Response.Cookies.Add(cookie);       //we are sending back to the user->response, Lecture 74,12:45

            return basket;
        }

        //method to add a basket item
        public void AddToBasket(HttpContextBase httpContext, string productId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = productId,
                    Quantity = 1
                };

                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + 1;
            }

            basketContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)        //the difference is that we remove the itemId
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }


    }
}

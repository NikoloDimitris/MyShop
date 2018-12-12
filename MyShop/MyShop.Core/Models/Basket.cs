using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Basket : BaseEntity
    {
        public virtual ICollection<BasketItem> BasketItems { get; set; }
        //create a constructor that will create an empty list of basket items of creation
        public Basket() {
            this.BasketItems = new List<BasketItem>();
        }
    }
}

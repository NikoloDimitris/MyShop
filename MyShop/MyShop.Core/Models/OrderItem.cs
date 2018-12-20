using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class OrderItem : BaseEntity     //inherit from BaseEntity
    {
        public string OrderId { get; set; }         //aal order items are going to be linked to an ynderlying order, the OrderId
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory : BaseEntity
    {
        //public string Id { get; set; }        //remove it at lecture 64, 17:20
        public string Category { get; set; }

        ////create a constructor to generate the Id whenever a new model is created
        //public ProductCategory() {
        //    this.Id = Guid.NewGuid().ToString();
        //} //remove it at lecture 64, 17:20
    }
}

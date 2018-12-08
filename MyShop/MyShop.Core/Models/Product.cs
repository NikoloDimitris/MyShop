using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Product : BaseEntity
    {
        //public string Id { get; set; } // remove it cause we have lecture 64 BaseEntity with Id

        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Range (0,1000)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }           //will contain a url
        //also remove it at lecture 64, 17:00, it is handle at BaseEntity
        ////create a constructor so that very time we create an instance of product we automatically generate an id
        //public Product() {
        //    this.Id = Guid.NewGuid().ToString();        //initially a generate Id & turns that ToString the Id
        //}

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetProducts.Models
{
    public class Product
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public Double Price { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public int ProductBrandID { get; set; }
        public ProductBrand ProductBrand { get; set; }
    }
}

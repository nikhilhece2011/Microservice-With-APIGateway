using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Infrastructure.Entities
{
    public class Products
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string image { get; set; }
        public string code { get; set; }
        public decimal price { get; set; }
    }
}

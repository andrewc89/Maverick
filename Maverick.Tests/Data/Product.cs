using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maverick.Tests.Data
{
    class Product : Maverick.Models.ModelBase<Product>
    {
        public long ID { get; set; }
        public string DisplayName { get; set; }
        public double Price { get; set; }

        public static IList<Product> GetProducts ()
        {
            return new List<Product>
            {
                new Product 
                {
                    ID = 1,
                    DisplayName = "Buck 110",
                    Price = 19.99
                },
                new Product
                {
                    ID = 2,
                    DisplayName = "Ka-Bar USMC",
                    Price = 49.99
                },
                new Product
                {
                    ID = 3,
                    DisplayName = "Smith & Wesson Extreme Ops",
                    Price = 19.99
                }
            };
        }

        public override void Save ()
        {
            throw new NotImplementedException();
        }
    }
}
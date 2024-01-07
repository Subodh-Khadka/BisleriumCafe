using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumCafe.Data.Models
{
    public class CoffeeTypes
    {
        public Guid CoffeeID { get; set; }

        public string? CoffeeName { get; set; }

        public decimal CoffeePrice { get; set; }

        public string? ImageLocation { get; set; }
    }
}

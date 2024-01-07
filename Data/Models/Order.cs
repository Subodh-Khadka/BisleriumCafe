using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumCafe.Data.Models
{
    public class Order
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public Guid CustomerId{ get; set; }
        public List<CoffeeTypes> SelectedCoffee {  get; set; } = new List<CoffeeTypes>();
        public List<AddIns> SelcectedAddIns { get; set; } = new List<AddIns>();

        public decimal TotalPrice;

    }
}

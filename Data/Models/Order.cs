using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumCafe.Data.Models
{
    public class Order
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();
        [Required]
        public Customer Customer { get; set; } // Reference to Customer model
        [Required]
        public List<CoffeeTypes> SelectedCoffee { get; set; } = new List<CoffeeTypes>();

        public List<AddIns> SelectedAddIns { get; set; } = new List<AddIns>();

        public decimal TotalPrice { get; set; }
        public DateTime OrderTimestamp { get; set; }

        public bool CoffeeRedeemed { get; set; }
    }

}

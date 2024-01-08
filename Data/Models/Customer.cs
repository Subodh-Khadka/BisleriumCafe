using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumCafe.Data.Models
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string CustomerName { get; set; }    
        public string PhoneNumber { get; set; }
        public bool IsMember { get; set; }
        public int PurchaseCount { get; set; }
    }
}

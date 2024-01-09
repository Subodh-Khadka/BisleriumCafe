using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumCafe.Data.Models
{
    public class Customer
    {       
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Customer Name is a required field.")]
        public string CustomerName { get; set; }
        
        [Required(ErrorMessage = "Phone Number is a required field.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone Number must contain only numerical digits.")]
        [StringLength(10, ErrorMessage = "Phone Number must be up to 10 digits.")]
        public string PhoneNumber { get; set; }
        public bool IsMember { get; set; }
        public int PurchaseCount { get; set; } = 0;
    }
}

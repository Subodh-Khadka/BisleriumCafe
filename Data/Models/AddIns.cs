using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumCafe.Data.Models
{
    public class AddIns
    {
        public Guid AddInID { get; set; }
        [Required]
        public String? AddInName { get; set; }
        [Required]
        public decimal AddInPrice { get; set; }
        public String AddInImageUrl { get; set; }  
    }
}

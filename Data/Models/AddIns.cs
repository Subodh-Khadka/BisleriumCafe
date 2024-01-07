using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumCafe.Data.Models
{
    public class AddIns
    {
        public Guid AddInID { get; set; }
        public String? AddInName { get; set; }
        public decimal AddInPrice { get; set; }
    }
}

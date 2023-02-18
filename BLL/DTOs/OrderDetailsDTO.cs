using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class OrderDetailsDTO
    {
        public int id { get; set; }
        public Nullable<int> Productid { get; set; }
        public Nullable<int> Orderid { get; set; }
        public Nullable<int> Unitprice { get; set; }
        public Nullable<int> Qty { get; set; }
    }
}

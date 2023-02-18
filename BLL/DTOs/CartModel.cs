using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class CartModel
    {
        public int tempid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Qty { get; set; }
        public string Descriptiion { get; set; }
    }
}

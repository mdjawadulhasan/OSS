using DAL.DB;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccessFactory
    {
        public static IRepo<Product, int> ProductDataAccess()
        {
            return new ProductRepo();
        }

        public static IRepo<Order, int> OrderDataAccess()
        {
            return new OrderRepo();
        }

        public static IRepo<Orderdetail, int> OrderDetailDataAccess()
        {
            return new OrderDetailsRepo();
        }
    }
}

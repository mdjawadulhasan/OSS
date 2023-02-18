using DAL.DB;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class OrderRepo : Repo, IRepo<Order, int>
    {
        public int Add(Order obj)
        {
            db.Orders.Add(obj);
            db.SaveChanges();
            return obj.Id; 
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> Get()
        {
            throw new NotImplementedException();
        }

        public Order Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Order obj)
        {
            throw new NotImplementedException();
        }
    }
}

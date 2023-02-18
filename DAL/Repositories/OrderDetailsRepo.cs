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
    internal class OrderDetailsRepo : Repo, IRepo<Orderdetail, int>
    {
        public int Add(Orderdetail obj)
        {
            db.Orderdetails.Add(obj);
            db.SaveChanges();
            return obj.id;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Orderdetail> Get()
        {
            throw new NotImplementedException();
        }

        public Orderdetail Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Orderdetail obj)
        {
            throw new NotImplementedException();
        }
    }
}

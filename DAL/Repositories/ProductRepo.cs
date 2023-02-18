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
    internal class ProductRepo :Repo,IRepo<Product, int>
    {
        public int Add(Product obj)
        {
            db.Products.Add(obj);
            db.SaveChanges();
            return obj.Id;
        }

        public bool Delete(int id)
        {
            var product = Get(id);
            db.Products.Remove(product);
            return db.SaveChanges() > 0;
        }

        public List<Product> Get()
        {
            return db.Products.ToList();
        }

        public Product Get(int id)
        {
            return db.Products.Find(id);
        }

        public bool Update(Product obj)
        {
            var product = Get(obj.Id);
            db.Entry(product).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;
        }
    }
}

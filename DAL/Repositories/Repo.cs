using DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class Repo
    {
        protected ShoppingSystemEntities db;
        internal Repo()
        {
            db = new ShoppingSystemEntities();
        }
    }
}

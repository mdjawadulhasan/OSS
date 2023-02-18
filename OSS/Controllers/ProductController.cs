using OSS.DB;
using OSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OSS.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var db = new ShoppingSystemEntities();
            var products = db.Products.ToList();
            return View(products);

        }


        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Product p)
        {

            var db = new ShoppingSystemEntities();
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new ShoppingSystemEntities();
            var p = (from b in db.Products
                     where b.Id == id
                     select b).SingleOrDefault();
            return View(p);
        }


        [HttpPost]
        public ActionResult Edit(Product p)
        {
            var db = new ShoppingSystemEntities();
            var ext = (from b in db.Products
                       where b.Id == p.Id
                       select b).SingleOrDefault();


            db.Entry(ext).CurrentValues.SetValues(p);

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var db = new ShoppingSystemEntities();
            Product exp = db.Products.Where(temp => temp.Id == id).FirstOrDefault();
            db.Products.Remove(exp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Addcart()
        {
            var db = new ShoppingSystemEntities();
            int id = Convert.ToInt32(Request.Form["id"]);
            int quantity = Convert.ToInt32(Request.Form["quantity"]);

            if (Session["p"] == null)
            {
                Product exp = db.Products.Where(temp => temp.Id == id).FirstOrDefault();
                ProductModelClass pr = new ProductModelClass();
                Random rnd = new Random();
                int randomNum = rnd.Next(100000, 999999);

                pr.tempid = randomNum;
                pr.Id = exp.Id;
                pr.Name = exp.Name;
                pr.Price = (int)exp.Price;
                pr.Descriptiion = exp.Descriptiion;
                pr.Qty = quantity;

                List<ProductModelClass> p = new List<ProductModelClass>();
                p.Add(pr);

                string json = new JavaScriptSerializer().Serialize(p);
                Session["p"] = json;
            }
            else
            {
                string json = Session["p"].ToString();
                var d = new JavaScriptSerializer().Deserialize<List<ProductModelClass>>(json);
                Product exp = db.Products.Where(temp => temp.Id == id).FirstOrDefault();

                ProductModelClass pr = new ProductModelClass();

                Random rnd = new Random();
                int randomNum = rnd.Next(100000, 999999);

                pr.tempid = randomNum;
                pr.Id = exp.Id;
                pr.Name = exp.Name;
                pr.Price = (int)exp.Price;
                pr.Descriptiion = exp.Descriptiion;
                pr.Qty = quantity;

                d.Add(pr);

                json = new JavaScriptSerializer().Serialize(d);
                Session["p"] = json;
            }

            return RedirectToAction("Index");
        }



        public ActionResult removefromcart(int id)
        {
            string json = Session["p"].ToString();
            var d = new JavaScriptSerializer().Deserialize<List<ProductModelClass>>(json);
            d.RemoveAll(p => p.tempid == id);

            json = new JavaScriptSerializer().Serialize(d);
            Session["p"] = json;


            return RedirectToAction("Show");
        }


        public ActionResult Show()
        {
            if (Session["p"] == null)
            {
                return RedirectToAction("Index");
            }

            string json = Session["p"].ToString();
            var products = new JavaScriptSerializer().Deserialize<List<ProductModelClass>>(json);

            int totalPrice = products.Sum(p => p.Price * p.Qty);

            ViewBag.TotalPrice = totalPrice;

            return View(products);
        }



        public ActionResult confirm()
        {
            var db = new ShoppingSystemEntities();


            string json = Session["p"].ToString();
            var p = new JavaScriptSerializer().Deserialize<List<ProductModelClass>>(json);
            int totalPrice = p.Sum(pp => pp.Price * pp.Qty);
            Order or = new Order();
            or.Amount = totalPrice;
            db.Orders.Add(or);
            db.SaveChanges();

            foreach (var b in p)
            {
                Orderdetail od = new Orderdetail();
                od.Orderid = or.Id;
                od.Productid = b.Id;
                od.Unitprice = b.Price;
                od.Qty=b.Qty;

                db.Orderdetails.Add(od);
                db.SaveChanges();

            }

            Session["p"] = null;

            return RedirectToAction("Index");
        }

    }
}
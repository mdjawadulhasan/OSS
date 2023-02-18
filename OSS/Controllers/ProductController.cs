﻿using OSS.DB;
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



        public ActionResult Addcart(int id)
        {
            var db = new ShoppingSystemEntities();
            if (Session["p"] == null)
            {


                Product exp = db.Products.Where(temp => temp.Id == id).FirstOrDefault();
                ProductModelClass pr = new ProductModelClass();
                pr.Id = exp.Id;
                pr.Name = exp.Name;
                pr.Price = exp.Price;
                pr.Descriptiion = exp.Descriptiion;



                List<ProductModelClass> p = new List<ProductModelClass>();
                p.Add(pr);

                string json = new JavaScriptSerializer().Serialize(p);
                Session["p"] = json;
                return RedirectToAction("Index");
            }
            else
            {
                string json = Session["p"].ToString();
                var d = new JavaScriptSerializer().Deserialize<List<ProductModelClass>>(json);
                Product exp = db.Products.Where(temp => temp.Id == id).FirstOrDefault();

                ProductModelClass pr = new ProductModelClass();
                pr.Id = exp.Id;
                pr.Name = exp.Name;
                pr.Price = exp.Price;
                pr.Descriptiion = exp.Descriptiion;
                d.Add(pr);


                json = new JavaScriptSerializer().Serialize(d);
                Session["p"] = json;
                return RedirectToAction("Index");
            }


        }


        public ActionResult Show()
        {

            if (Session["p"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string json = Session["p"].ToString();
                var p = new JavaScriptSerializer().Deserialize<List<ProductModelClass>>(json);
                return View(p);
            }


        }


        public ActionResult confirm()
        {
            var db = new ShoppingSystemEntities();


            string json = Session["p"].ToString();
            var p = new JavaScriptSerializer().Deserialize<List<ProductModelClass>>(json);

            Order or = new Order();
            or.Amount = 10000.ToString();
            db.Orders.Add(or);
            db.SaveChanges();

            foreach (var b in p)
            {
                Orderdetail od = new Orderdetail();
                od.Orderid = or.Id;
                od.Productid = b.Id;
                od.Unitprice = b.Price;

                db.Orderdetails.Add(od);
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }

    }
}
using BLL;
using BLL.DTOs;
using BLL.Services;
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

        public ActionResult Index()
        {

            var products = ProductService.GetProducts();
            return View(products);

        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(ProductDTO p)
        {

            ProductService.Add(p);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {

            var p = ProductService.Get(id);
            return View(p);
        }


        [HttpPost]
        public ActionResult Edit(ProductDTO p)
        {
            ProductService.Update(p);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            ProductService.Delete(id);
            return RedirectToAction("Index");
        }




        public ActionResult Addcart()
        {
           

            int id = Convert.ToInt32(Request.Form["id"]);
            int quantity = Convert.ToInt32(Request.Form["quantity"]);

            if (Session["p"] == null)
            {

                var prod = ProductService.AddToCart(id, quantity);
                List<CartModel> p = new List<CartModel>();
                p.Add(prod);
                string json = new JavaScriptSerializer().Serialize(p);
                Session["p"] = json;
            }
            else
            {
                string json = Session["p"].ToString();
                var d = new JavaScriptSerializer().Deserialize<List<CartModel>>(json);
               
                var prod = ProductService.AddToCart(id, quantity);
                d.Add(prod);
                json = new JavaScriptSerializer().Serialize(d);
                Session["p"] = json;
            }

            return RedirectToAction("Index");
        }



        public ActionResult Removefromcart(int id)
        {
            string json = Session["p"].ToString();
            var d = new JavaScriptSerializer().Deserialize<List<CartModel>>(json);          
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
            var products = new JavaScriptSerializer().Deserialize<List<CartModel>>(json);
            int totalPrice = products.Sum(p => p.Price * p.Qty);
            ViewBag.TotalPrice = totalPrice;
            return View(products);
        }


        
        public ActionResult confirm()
        {
    
            string json = Session["p"].ToString();
            var p = new JavaScriptSerializer().Deserialize<List<CartModel>>(json);
            OrderService.ConfirmOrder(p);
            Session["p"] = null;
            return RedirectToAction("Index");
        }
        
    }
}



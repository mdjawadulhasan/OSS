using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService
    {
        public static List<ProductDTO> GetProducts()
        {
            var data = DataAccessFactory.ProductDataAccess().Get();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>());
            var mapper = new Mapper(config);
            var products = mapper.Map<List<ProductDTO>>(data);
            return products;

        }

        public static int Add(ProductDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>());
            var mapper = new Mapper(config);
            var data = mapper.Map<Product>(dto);
            var result = DataAccessFactory.ProductDataAccess().Add(data);
            return result;
        }


        public static bool Update(ProductDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>());
            var mapper = new Mapper(config);
            var data = mapper.Map<Product>(dto);
            var result = DataAccessFactory.ProductDataAccess().Update(data);
            return result;

        }


        public static ProductDTO Get(int id)
        {
            var data = DataAccessFactory.ProductDataAccess().Get(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>());
            var mapper = new Mapper(config);
            var res = mapper.Map<ProductDTO>(data);
            return res;
        }

        public static bool Delete(int id)
        {

            var result = DataAccessFactory.ProductDataAccess().Delete(id);
            return result;

        }

        public static CartModel AddToCart(int id, int quantity)
        {
            ProductDTO exp = ProductService.Get(id);
            CartModel cartmodel = new CartModel();

            Random rnd = new Random();
            int randomNum = rnd.Next(100000, 999999);

            cartmodel.tempid = randomNum;
            cartmodel.Id = exp.Id;
            cartmodel.Name = exp.Name;
            cartmodel.Price = (int)exp.Price;
            cartmodel.Descriptiion = exp.Descriptiion;
            cartmodel.Qty = quantity;    
            return cartmodel;
        }



    }
}

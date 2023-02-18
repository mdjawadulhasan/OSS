using AutoMapper;
using DAL.DB;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Services
{
    public class OrderService
    {
        public static int Add(OrderDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, Order>());
            var mapper = new Mapper(config);
            var data = mapper.Map<Order>(dto);
            var result = DataAccessFactory.OrderDataAccess().Add(data);
            return result;
        }

        public static bool ConfirmOrder(List<CartModel> cartModels)
        {

            int totalPrice = cartModels.Sum(pp => pp.Price * pp.Qty);
            OrderDTO or = new OrderDTO();
            or.Amount = totalPrice;
            int order_id =Add(or);

            foreach (var b in cartModels)
            {
                OrderDetailsDTO od = new OrderDetailsDTO();
                od.Orderid = order_id;
                od.Productid = b.Id;
                od.Unitprice = b.Price;
                od.Qty = b.Qty;

                OrderDetailService.Add(od);
            }



            return true;

        }

    }
}

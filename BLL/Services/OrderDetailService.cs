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
    public class OrderDetailService
    {
        public static int Add(OrderDetailsDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderDetailsDTO, Orderdetail>());
            var mapper = new Mapper(config);
            var data = mapper.Map<Orderdetail>(dto);
            var result = DataAccessFactory.OrderDetailDataAccess().Add(data);
            return result;
        }
    }
}

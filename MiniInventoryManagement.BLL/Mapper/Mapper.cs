using AutoMapper;
using MiniInventoryManagement.BLL.DTOs;
using MiniInventoryManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniInventoryManagement.BLL.Mapper
{
    public class Mapper:Profile
    {
        public Mapper()
        {
           CreateMap<ProductInformation,ProductInfoDTO>().ReverseMap();
              CreateMap<OrderInformation, OrderInfoDTO>().ReverseMap();
            CreateMap<OrderHistory, OrderHistoryDTO>().ReverseMap();
        }
    }
}

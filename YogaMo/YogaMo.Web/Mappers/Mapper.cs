using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model;
using YogaMo.Model.Requests;
using YogaMo.Web.Areas.Instructor.ViewModels;
using YogaMo.Web.Areas.Administrator.ViewModels;
using YogaMo.Web.ViewModels;
using YogaMo.WebAPI.Database;
using static YogaMo.Web.Areas.Administrator.ViewModels.ProductsIndexVM;

namespace YogaMo.Web
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<RegisterVM, WebAPI.Database.Client>().ReverseMap();
            CreateMap<YogaClassCreateVM, WebAPI.Database.YogaClass>().ReverseMap();
            CreateMap<InstructorVM, WebAPI.Database.Instructor>().ReverseMap();
            CreateMap<CreateInstructorVM, WebAPI.Database.Instructor>().ReverseMap();

            CreateMap<WebAPI.Database.Client, ProfileVM>().ReverseMap();
            CreateMap<WebAPI.Database.Instructor, ProfileVM>().ReverseMap();
            CreateMap<WebAPI.Database.Administrator, ProfileVM>().ReverseMap();

            CreateMap<WebAPI.Database.Product, WebAPI.Database.Product>().ReverseMap();
            CreateMap<WebAPI.Database.Product, ProductsVM>().ReverseMap();
            CreateMap<WebAPI.Database.Product, ProductVM>().ReverseMap();

            CreateMap<WebAPI.Database.OrderItem, ProductVM>().ReverseMap();

        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model.Requests;

namespace YogaMo.WebAPI.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Database.City, Model.Cities>();
            CreateMap<Database.City, CitiesInsertRequest>().ReverseMap();

            CreateMap<Database.Instructor, Model.Instructors>();
            CreateMap<Database.Instructor, InstructorsInsertRequest>().ReverseMap();

            CreateMap<Database.Instructor, InstructorSearchRequest>().ReverseMap();


        }
    }
}

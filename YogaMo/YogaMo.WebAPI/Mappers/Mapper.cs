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
            CreateMap<Database.Instructor, Model.Instructor>();
            CreateMap<Database.Instructor, InstructorInsertRequest>().ReverseMap();
            CreateMap<Database.Instructor, InstructorSearchRequest>().ReverseMap();

        }
    }
}

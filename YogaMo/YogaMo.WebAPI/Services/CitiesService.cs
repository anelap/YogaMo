using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Database;

namespace YogaMo.WebAPI.Services
{
    public class CitiesService : ICitiesService
    {
        private readonly YogaMoContext _context;
        private readonly IMapper _mapper;

        public CitiesService(YogaMoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.Cities> Get()
        {
            var list = _context.City.ToList();

            return _mapper.Map<List<Model.Cities>>(list);
        }

        public Model.Cities GetById(int id)
        {
            var entity = _context.City.Find(id);

            return _mapper.Map<Model.Cities>(entity);
        }

        public Model.Cities Insert(CitiesInsertRequest request)
        {
            // TODO: implementirati provjeru ako grad vec postoji
            var entity = _mapper.Map<Database.City>(request);


            _context.City.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Cities>(entity);
        }
    }
}

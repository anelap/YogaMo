using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Exceptions;

namespace YogaMo.WebAPI.Services
{
    public class YogaService : IYogaService
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;
        public YogaService(_150222Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Delete(int id)
        {
            var entity = _context.Yoga.Find(id);

            _context.Yoga.Remove(entity);
            _context.SaveChanges();
        }

        public List<Model.Yoga> Get(YogaSearchRequest request)
        {
            var query = _context.Yoga.AsQueryable();

            if (!string.IsNullOrEmpty(request?.Name))
            {
                query = query.Where(x => x.Name.ToUpper().Contains(request.Name.ToUpper()));
            }

            var list = query.ToList();

            return _mapper.Map<List<Model.Yoga>>(list);
        }

        public Model.Yoga Get(int id)
        {
            var entity = _context.Yoga.Find(id);

            if (entity == null)
            {
                throw new UserException("Yoga not found");
            }

            return _mapper.Map<Model.Yoga>(entity);
        }

        public Model.Yoga Insert(YogaInsertRequest request)
        {
            var entity = _mapper.Map<Database.Yoga>(request);

            _context.Yoga.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Yoga>(entity);
        }

        public Model.Yoga Update(int id, YogaInsertRequest request)
        {
            var entity = _context.Yoga.Find(id);

            if (entity == null) throw new UserException("Yoga does not exist");

            Mapper.Map<YogaInsertRequest, Database.Yoga>(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Yoga>(entity);
        }
    }
}

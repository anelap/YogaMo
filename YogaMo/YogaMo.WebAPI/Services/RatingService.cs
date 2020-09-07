using AutoMapper;
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
    public class RatingService : IRatingService
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;
        public RatingService(_150222Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Delete(int id)
        {
            var entity = _context.Rating.Find(id);
            _context.Rating.Remove(entity);
            _context.SaveChanges();
        }

        public List<Model.Rating> Get()
        {
            var list = _context.Rating.ToList();

            return _mapper.Map<List<Model.Rating>>(list);
        }

        public Model.Rating Get(int id)
        {
            var entity = _context.Rating.Find(id);

            return _mapper.Map<Model.Rating>(entity);
        }

        public Model.Rating GetByProductAndClient(int productId, int clientId)
        {
            var entity = _context.Rating.Where(x => x.ProductId == productId).Where(c => c.ClientId == clientId).FirstOrDefault();

            if (entity == null) throw new UserException("Rating not found");

            return _mapper.Map<Model.Rating>(entity);

        }

        public Model.Rating Insert(RatingInsertRequest request)
        {
            var entity = _mapper.Map<Database.Rating>(request);

            _context.Rating.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Rating>(entity);
        }

        public Model.Rating Update(int id, RatingInsertRequest request)
        {
            var entity = _context.Rating.Find(id);

            if (entity == null) throw new UserException("Rating not found");

            Mapper.Map<RatingInsertRequest, Database.Rating>(request, entity);

            _context.SaveChanges();
            return _mapper.Map<Model.Rating>(entity);
        }
    }
}

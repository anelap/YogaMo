using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public class InstructorService : IInstructorService
    {
        private readonly YogaMoContext _context;
        private readonly IMapper _mapper;

        public InstructorService(YogaMoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.Instructor> Get(InstructorSearchRequest request)
        {
            var query = _context.Instructor.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request?.FirstName))
            {
                query = query.Where(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));
            }
            if (!string.IsNullOrWhiteSpace(request?.LastName))
            {
                query = query.Where(x => x.LastName.ToUpper().Contains(request.LastName.ToUpper()));
            }

            var list = query.ToList();

            return _mapper.Map<List<Model.Instructor>>(list);

        }

        public Model.Instructor GetById(int id)
        {
            var entity = _context.Instructor.Find(id);

            return _mapper.Map<Model.Instructor>(entity);
        }

        public Model.Instructor Insert(InstructorInsertRequest request)
        {
            var entity = _mapper.Map<Database.Instructor>(request);

            if(request.Password != request.PasswordConfirmation)
            {
                throw new UserException("Passwords don't match!");
            }

            entity.PasswordHash = "test";
            entity.PasswordSalt = "test";

            _context.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Instructor>(entity);
        }

    }
}

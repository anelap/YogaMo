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
    public class InstructorsService : IInstructorsService
    {
        private readonly YogaMoContext _context;
        private readonly IMapper _mapper;

        public InstructorsService(YogaMoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.Instructors> Get(InstructorsSearchRequest request)
        {
            var query = _context.Instructor.AsQueryable();

            /*    if (!string.IsNullOrWhiteSpace(request?.FirstName) || !string.IsNullOrWhiteSpace(request?.LastName))
                {
                    query = query.Where(x => 
                    x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()) || x.LastName.ToUpper().Contains(request.LastName.ToUpper()));
                }*/
            if (!string.IsNullOrWhiteSpace(request?.FirstName) )
            {
                query = query.Where(x =>
                x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));
            }
            else if(!string.IsNullOrWhiteSpace(request?.LastName))
            {
                query = query.Where(x =>
                x.LastName.ToUpper().Contains(request.LastName.ToUpper()));
            }

            var list = query.ToList();

            return _mapper.Map<List<Model.Instructors>>(list);

        }

        public Instructors GetById(int id)
        {
            var entity = _context.Instructor.Find(id);

            return _mapper.Map<Instructors>(entity);
        }

        public Instructors Insert(InstructorsInsertRequest request)
        {
            var entity = _mapper.Map<Database.Instructor>(request);

            if(request.Password != request.PasswordConfirmation)
            {
                throw new UserException("Passwords don't match!");
            }

            //TODO password authentication

            entity.PasswordHash = "test";
            entity.PasswordSalt = "test";

            _context.Instructor.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Instructors>(entity);
        }

        public Instructors Update(int id, InstructorsInsertRequest request)
        {
            var entity = _context.Instructor.Find(id);
            _mapper.Map(request, entity);

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                if (request.Password != request.PasswordConfirmation)
                {
                    throw new Exception("Passwords don't match");
                    //TODO: update password
                }
            }
            _context.SaveChanges();
            return _mapper.Map<Instructors>(entity);
        }
    }
}

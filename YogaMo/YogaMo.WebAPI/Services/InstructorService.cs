using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using YogaMo.Model;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Exceptions;
using YogaMo.WebAPI.Helpers;

namespace YogaMo.WebAPI.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;
        private Model.Instructor _currentUser;

        public InstructorService(_150222Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Model.Instructor Authenticate(string username, string password)
        {
            var user = _context.Instructor.FirstOrDefault(x => x.Username == username);

            if (user != null)
            {
                var newHash = PasswordHelper.GenerateHash(user.PasswordSalt, password);

                if (newHash == user.PasswordHash) 
                {
                    return _mapper.Map<Model.Instructor>(user);
                }
            }

            return null;
        }

      
        public List<Model.Instructor> Get(InstructorsSearchRequest request)
        {
            var query = _context.Instructor.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request?.Name))
            {
                query = query.Where(x => (x.FirstName.ToUpper().Trim() + " " + x.LastName.ToUpper().Trim()).Trim().Contains(request.Name.ToUpper()));
            }

            var list = query.ToList();

            return _mapper.Map<List<Model.Instructor>>(list);

        }

        public Model.Instructor Get(int id)
        {
            var entity = _context.Instructor.Find(id);

            return _mapper.Map<Model.Instructor>(entity);
        }

        public Model.Instructor Insert(InstructorsInsertRequest request)
        {
            var entity = _mapper.Map<Database.Instructor>(request);

            if (request.Password != request.PasswordConfirmation)
            {
                throw new UserException("Passwords don't match");
            }

            entity.PasswordSalt = PasswordHelper.GenerateSalt();
            entity.PasswordHash = PasswordHelper.GenerateHash(entity.PasswordSalt, request.Password);

            _context.Instructor.Add(entity); 
            _context.SaveChanges();

            return _mapper.Map<Model.Instructor>(entity);
        }

        public Model.Instructor Update(int id, InstructorsUpdateRequest request)
        {
            var entity = _context.Instructor.Find(id);

            Mapper.Map<InstructorsUpdateRequest, Database.Instructor>(request, entity);

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                if (request.Password != request.PasswordConfirmation)
                {
                    throw new UserException("Passwods don't match!");
                }
                entity.PasswordSalt = PasswordHelper.GenerateSalt();
                entity.PasswordHash = PasswordHelper.GenerateHash(entity.PasswordSalt, request.Password);
            }

            _context.SaveChanges();

            return _mapper.Map<Model.Instructor>(entity);
        }

        public Model.Instructor GetCurrentInstructor()
        {
            return _currentUser;
        }

        public void SetCurrentInstructor(Model.Instructor user)
        {
            _currentUser = user;
        }

        public Model.Instructor MyProfile()
        {
            if (_currentUser == null)
                return null;

            var query = _context.Instructor.AsQueryable();

            query = query.Where(x => x.InstructorId == _currentUser.InstructorId);

            var entity = query.FirstOrDefault();

            return _mapper.Map<Model.Instructor>(entity);
        }

    }
}

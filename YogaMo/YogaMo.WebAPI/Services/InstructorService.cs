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
            // checking if the user exists in the database
            var user = _context.Instructor.FirstOrDefault(x => x.Username == username);

            if (user != null)
            {
                var newHash = GenerateHash(user.PasswordSalt, password);

                if (newHash == user.PasswordHash) // if the password is correct
                {
                    return _mapper.Map<Model.Instructor>(user);
                }
            }

            return null;
        }

        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

        public List<Model.Instructor> Get(InstructorsSearchRequest request)
        {
            var query = _context.Instructor.AsQueryable();

            /*   if (!string.IsNullOrWhiteSpace(request?.FirstName) || !string.IsNullOrWhiteSpace(request?.LastName))
               {
                   query = query.Where(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()) || x.LastName.ToUpper().Contains(request.LastName.ToUpper()));
               }*/

            if (!string.IsNullOrWhiteSpace(request?.FirstName))
            {
                query = query.Where(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));
            }
            else if (!string.IsNullOrWhiteSpace(request?.LastName))
            {
                query = query.Where(x => x.LastName.ToUpper().Contains(request.LastName.ToUpper()));
            }

            var list = query.ToList();

            return _mapper.Map<List<Model.Instructor>>(list);

        }

        public Model.Instructor Get(int id)
        {
            var entity = _context.Instructor.Find(id);

            return _mapper.Map<Model.Instructor>(entity);
        }

        public List<Model.Yoga> GetYogaByInstructor(int id) // maybe include this??
        {
            var query = _context.Yoga.AsQueryable();

            query = query.Where(x => x.Instructor.InstructorId == id);

            var list = query.ToList();

            return _mapper.Map<List<Model.Yoga>>(list);
        }

        public Model.Instructor Insert(InstructorsInsertRequest request)
        {
            var entity = _mapper.Map<Database.Instructor>(request);

            if (request.Password != request.PasswordConfirmation)
            {
                throw new UserException("Passwords don't match");
            }

            entity.PasswordSalt = GenerateSalt();
            entity.PasswordHash = GenerateHash(entity.PasswordSalt, request.Password);

            _context.Instructor.Add(entity); // nota bene da treba i Instructor
            _context.SaveChanges();

            return _mapper.Map<Model.Instructor>(entity);
        }

        public Model.Instructor Update(int id, InstructorsInsertRequest request)
        {
            var entity = _context.Instructor.Find(id);

            Mapper.Map<InstructorsInsertRequest, Database.Instructor>(request, entity); // Nota bene?

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                if (request.Password != request.PasswordConfirmation)
                {
                    throw new Exception("Passwods don't match!");
                }
                entity.PasswordSalt = GenerateSalt();
                entity.PasswordHash = GenerateHash(entity.PasswordSalt, request.Password);
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
            var query = _context.Instructor.AsQueryable();

            query = query.Where(x => x.InstructorId == _currentUser.InstructorId);

            var entity = query.FirstOrDefault();

            return _mapper.Map<Model.Instructor>(entity);
        }

        /*   public void Add<T>(T entity) where T : class
           {
               _context.Add(entity);
           }

           public void Delete<T>(T entity) where T : class
           {
               _context.Remove(entity);
           }

           public async Task<Instructor[]> GetAllInstructorsAsync(bool includeClasses = false)
           {
               IQueryable<Instructor> query = _context.Instructor;

               query = query.OrderByDescending(c => c.FirstName);

               return await query.ToArrayAsync();
           }

           public async Task<Instructor[]> GetInstructorAsync(string name, bool includeClasses = false)
           {
               IQueryable<Instructor> query = _context.Instructor.Where(x => x.FirstName.ToUpper().Contains(name) || x.LastName.ToUpper().Contains(name));

               return await query.ToArrayAsync();
           }

           public async Task<Instructor> GetInstructorByIdAsync(int id)
           {
               var query = _context.Instructor.Where(x => x.InstructorId.Equals(id));

               return await query.FirstOrDefaultAsync();
           }

           public async Task<Yoga[]> GetYogas(int id)
           {
               var query = _context.Yoga;

               var yogas = query.Where(x => x.InstructorId.Equals(id)).OrderByDescending(y => y.Name);

               return await yogas.ToArrayAsync();
           }

           public async Task<bool> SaveChangesAsync()
           {
               // returns true if at least one row was changed
               return (await _context.SaveChangesAsync()) > 0;
           }*/
    }
}

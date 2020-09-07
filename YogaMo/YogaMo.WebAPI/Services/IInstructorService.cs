using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Database;

namespace YogaMo.WebAPI.Services
{
    public interface IInstructorService
    {

        List<Model.Instructor> Get(InstructorsSearchRequest reuqest);
        Model.Instructor Get(int id);
        Model.Instructor Insert(InstructorsInsertRequest request);
        Model.Instructor Update(int id, InstructorsInsertRequest request);

        List<Model.Yoga> GetYogaByInstructor(int id); //maybe include this?

        // Authentication 
        Model.Instructor Authenticate(string username, string password);


        Model.Instructor GetCurrentInstructor();
        void SetCurrentInstructor(Model.Instructor user);
        Model.Instructor MyProfile();

        // show yoga classes held by instructor?

        // TODO: delete

        /* void Add<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChangesAsync();


         Task<Instructor[]> GetAllInstructorsAsync(bool includeClasses = false);
         Task<Instructor[]> GetInstructorAsync(string name, bool includeClasses = false);
         Task<Instructor> GetInstructorByIdAsync(int id);
         Task<Yoga[]> GetYogas(int id);*/

    }
}

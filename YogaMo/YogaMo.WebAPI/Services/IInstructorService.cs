using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model.Requests;

namespace YogaMo.WebAPI.Services
{
    public interface IInstructorService
    {
        List<Model.Instructor> Get(InstructorSearchRequest request);
        Model.Instructor Insert(InstructorInsertRequest request);
        Model.Instructor GetById(int id);
    }
}

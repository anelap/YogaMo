using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model.Requests;

namespace YogaMo.WebAPI.Services
{
    public interface IInstructorsService
    {
        List<Model.Instructors> Get(InstructorsSearchRequest request);
        Model.Instructors GetById(int id);
        Model.Instructors Insert(InstructorsInsertRequest request);
        Model.Instructors Update(int id, InstructorsInsertRequest request);
    }
}

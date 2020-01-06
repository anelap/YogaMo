using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Services;

namespace YogaMo.WebAPI.Controllers
{
    [Route("api/instruktori")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _service;

        public InstructorController(IInstructorService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Model.Instructor>> Get([FromQuery] InstructorSearchRequest request)
        {
            return _service.Get(request);

        }

        [HttpGet("{id}")]
        public Model.Instructor GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost]
        public Model.Instructor Insert(InstructorInsertRequest request)
        {
            return _service.Insert(request);
        }
    }
}

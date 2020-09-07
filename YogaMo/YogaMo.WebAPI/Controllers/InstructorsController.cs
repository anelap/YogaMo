using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YogaMo.Model;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Services;

namespace YogaMo.WebAPI.Controllers
{
    [Route("api/instruktori")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorsService _service;
        public InstructorsController(IInstructorsService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Instructors>> Get([FromQuery] InstructorsSearchRequest request)
        {
            return _service.Get(request);
        }

        [HttpGet("{id}")]
        public Instructors GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost]
        public Instructors Insert(InstructorsInsertRequest request)
        {
            return _service.Insert(request);
        } 

        [HttpPut("{id}")]
        public Instructors Update(int id, InstructorsInsertRequest request)
        {
            return _service.Update(id, request);
        }
    }
}
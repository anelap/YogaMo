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
    [Route("api/gradovi")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesService _service;

        public CitiesController(ICitiesService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Model.Cities>> Get()//[FromQuery] InstructorSearchRequest request
        {
            return _service.Get();
        }

        [HttpGet("{id}")]
        public Model.Cities GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost]
        public Model.Cities Insert(CitiesInsertRequest request)
        {
            return _service.Insert(request);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Services;

namespace YogaMo.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiController]
    public class InstructorsController : ControllerBase // ne zaboraviti ControllerBase
    {
        private readonly IInstructorService _service;
        private readonly IMapper _mapper;

        public InstructorsController(IInstructorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Model.Instructor>> Get([FromQuery]InstructorsSearchRequest request)
        {
            return _service.Get(request);
        }

        [HttpGet("{id}")]
        public Model.Instructor GetById(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        public ActionResult<Model.Instructor> Insert(InstructorsInsertRequest request)
        {
            return _service.Insert(request);
        }

        [HttpPut("{id}")]
        public Model.Instructor Update(int id, InstructorsInsertRequest request)
        {
            return _service.Update(id, request);
        }

        [HttpGet("MyProfile")]
        public Model.Instructor MyProfile()
        {
            return _service.MyProfile();
        }
        /*      [HttpGet]
              public async Task<ActionResult<Model.Instructor[]>> GetInstructors()
              {
                  try
                  {
                      var results = await _service.GetAllInstructorsAsync();

                      return _mapper.Map<Model.Instructor[]>(results);
                  }
                  catch (Exception)
                  {
                      return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                  }
              }

              [HttpGet("{name}")]
              public async Task<ActionResult<Model.Instructor[]>> Get(string name)
              {
                  try
                  {
                      var result = await _service.GetInstructorAsync(name);
                      if (result == null) return NotFound();

                      return _mapper.Map<Model.Instructor[]>(result);
                  }
                  catch (Exception)
                  {
                      return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                  }
              }

              [HttpGet("{id}")]
              public async Task<ActionResult<Model.Instructor>> Get(int id)
              {
                  try
                  {
                      var result = await _service.GetInstructorByIdAsync(id);
                      if (result == null) return NotFound();

                      return _mapper.Map<Model.Instructor>(result);
                  }
                  catch (Exception)
                  {
                      return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                  }
              }

              [HttpPost]
              public async Task<ActionResult<Model.Instructor>> Post(Model.Instructor model)
              {
               //   try
              //    {
                      var instructor = _mapper.Map<Database.Instructor>(model);
                      _service.Add(instructor);
                      if(await _service.SaveChangesAsync())
                      {
                          return Ok(instructor);
                      }
                      // hash password i salt password trebaju
            //      }
                 catch (Exception)
                  {
                      return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                  }

                  return BadRequest();
              }*/
    }
}

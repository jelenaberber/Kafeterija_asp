using API.Extensions;
using Application.DTO;
using Application.UseCases.Commands.Containers;
using Application.UseCases.Queries;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainersController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;
        private Context _context;

        public ContainersController(UseCaseHandler commandHandler, Context context)
        {
            _useCaseHandler = commandHandler;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] ContainerSearch search, [FromServices] IGetContainersQuery query)
            => Ok(_useCaseHandler.HandleQuery(query, search));

        // GET api/<ContainersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateContainerDto dto, [FromServices] ICreateContainerCommand cmd)
        {
            try
            {
                _useCaseHandler.HandleCommand(cmd, dto);

                return StatusCode(201);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return UnprocessableEntity(ex.Errors.Select(x => new
                {
                    Error = x.ErrorMessage,
                    Property = x.PropertyName
                }));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return this.InternalServerError(new { error = "An error has occured..." });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateContainerDto dto, [FromServices] IUpdateContainerCommand command)
        {
            try
            {
                dto.Id = id;
                Container c = _context.Containers.FirstOrDefault(c => c.Id == id);
                if (c == null)
                {
                    return NotFound();
                }
                _useCaseHandler.HandleCommand(command, dto);
                return StatusCode(201);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return UnprocessableEntity(ex.Errors.Select(x => new
                {
                    Error = x.ErrorMessage,
                    Property = x.PropertyName
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Container c = _context.Containers.Find(id);
            if (c == null || c.IsActive == false)
            {
                return NotFound();
            }
            c.IsActive = false;
            _context.SaveChanges();
            return NoContent();
        }
    }
}

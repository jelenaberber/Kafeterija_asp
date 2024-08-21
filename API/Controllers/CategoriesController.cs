using API.Extensions;
using Application.DTO;
using Application.UseCases.Commands.Categories;
using Application.UseCases.Queries;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;
        private Context _context;

        public CategoriesController(UseCaseHandler commandHandler, Context context)
        {
            _useCaseHandler = commandHandler;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] CategorySearch search, [FromServices] IGetCategoriesQuery query)
            => Ok(_useCaseHandler.HandleQuery(query, search));

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateCategoryDto dto, [FromServices] ICreateCategoryCommand cmd)
        {
            try
            {
                _useCaseHandler.HandleCommand(cmd, dto);

                return StatusCode(201);
            }
            catch (ValidationException ex)
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
        public IActionResult Put(int id, [FromBody] UpdateCategoryDto dto, [FromServices] IUpdateCategoryCommand command)
        {
            try
            {
                dto.Id = id;
                Category c = _context.Categories.FirstOrDefault(c => c.Id == id);
                if (c == null)
                {
                    return NotFound();
                }
                _useCaseHandler.HandleCommand(command, dto);
                return StatusCode(201);
            }
            catch (ValidationException ex)
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
                Category c = _context.Categories.Find(id);
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

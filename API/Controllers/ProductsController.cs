using API.Extensions;
using Application.DTO;
using Application.UseCases.Commands.Categories;
using Application.UseCases.Commands.Containers;
using Application.UseCases.Commands.Products;
using Application.UseCases.Queries;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;
        private Context _context;

        public ProductsController(UseCaseHandler commandHandler, Context context)
        {
            _useCaseHandler = commandHandler;
            _context = context;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        [HttpGet]
        public IActionResult Get([FromQuery] ProductSearch search, [FromServices] IGetProductsQuery query)
            => Ok(_useCaseHandler.HandleQuery(query, search));

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateProduct dto, [FromServices] ICreateProductCommand cmd)
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
        public IActionResult Put(int id, [FromBody] UpdateProductDto dto, [FromServices] IUpdateProductsCommand command)
        {
            try
            {
                dto.Id = id;
                Product p = _context.Products.FirstOrDefault(p => p.Id == id);
                if (p == null)
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
            Product p = _context.Products.Find(id);
            if (p == null || p.IsActive == false)
            {
                return NotFound();
            }
            p.IsActive = false;
            _context.SaveChanges();
            return NoContent();
        }
    }
}

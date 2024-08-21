using Application.DTO;
using Application.UseCases.Commands.Products;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Products
{
    public class EfCreateProductCommand : ICreateProductCommand
    {
        public int Id => 11;

        public string Name => "Create product";

        private Context _context;
        private CreateProductValidator _validator;

        public EfCreateProductCommand(Context context, CreateProductValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(CreateProduct data)
        {
            _validator.ValidateAndThrow(data);

            Product product = new Product
            {
                Name = data.Name,
                Description = data.Description,
                CategoryId = data.CategoryId,
            };

            _context.Products.Add(product);
            _context.SaveChanges();
        }
    }
}

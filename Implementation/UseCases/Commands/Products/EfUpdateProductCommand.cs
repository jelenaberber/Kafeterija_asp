using Application.DTO;
using Application.UseCases.Commands.Containers;
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
    public class EfUpdateProductCommand : IUpdateProductsCommand
    {
        public int Id => 12;

        public string Name => "Update product";

        private Context _context;
        private UpdateProductValidator _validator;

        public EfUpdateProductCommand(Context context, UpdateProductValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(UpdateProductDto data)
        {
            _validator.ValidateAndThrow(data);

            Product product = _context.Products.FirstOrDefault(x => x.Id == data.Id);

            product.Name = data.Name;
            product.Description = data.Description;
            product.CategoryId = data.CategoryId;

            _context.SaveChanges();
        }
    }
}

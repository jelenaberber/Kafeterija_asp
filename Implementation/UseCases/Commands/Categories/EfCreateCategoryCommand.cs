using Application.DTO;
using Application.UseCases.Commands.Categories;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Categories
{
    public class EfCreateCategoryCommand : ICreateCategoryCommand
    {
        public int Id => 3;

        public string Name => "CreateCategory";

        private Context _context;
        private CreateCategoryValidator _validator;

        public EfCreateCategoryCommand(Context context, CreateCategoryValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public void Execute(CreateCategoryDto data)
        {
            _validator.ValidateAndThrow(data);

            Category category = new Category
            {
                Name = data.Name
            };

            _context.Categories.Add(category);

            _context.SaveChanges();
        }
    }
}

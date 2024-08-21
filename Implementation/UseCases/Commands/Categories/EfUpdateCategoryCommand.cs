using Application.DTO;
using Application.UseCases.Commands.Categories;
using DataAccess;
using Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Implementation.UseCases.Commands.Categories
{
    public class EfUpdateCategoryCommand : IUpdateCategoryCommand
    {
        public int Id => 4;

        public string Name => "UpdateCategory";

        private Context _context;
        private UpdateCategoryValidator _validator;

        public EfUpdateCategoryCommand(Context context, UpdateCategoryValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public void Execute(UpdateCategoryDto data)
        {
            _validator.ValidateAndThrow(data);

            Category category = _context.Categories.FirstOrDefault(x => x.Id == data.Id);

            category.Name = data.Name;

            _context.SaveChanges();
        }
    }
}

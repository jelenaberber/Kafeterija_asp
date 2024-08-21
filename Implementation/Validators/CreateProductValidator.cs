using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProduct>
    {
        public CreateProductValidator(Context ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches("(?=.{4,15}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
                .WithMessage("Invalid product name format.")
                .Must(x => !ctx.Products.Any(c => c.Name == x))
                .WithMessage("Product name already exist.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty.");

            RuleFor(x => x.CategoryId)
                .Must(id => ctx.Categories.Any(c => c.Id == id))
                .WithMessage("Category ID does not exist.");
        }


    }
}

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
    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator(Context ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches("(?=.{4,15}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
                .WithMessage("Product name must start with a capital letter and can be up to 40 characters long.")
                .Must((product, name) =>
                {
                    // Proveri da li već postoji proizvod sa istim imenom, ali sa drugačijim ProductId
                    return !ctx.Products.Any(p => p.Name == name && p.Id != product.Id);
                })
                .WithMessage("Product name must be unique.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty.");

            RuleFor(x => x.CategoryId)
                .Must(id => ctx.Categories.Any(c => c.Id == id))
                .WithMessage("Category ID does not exist.");
        }
    }
}

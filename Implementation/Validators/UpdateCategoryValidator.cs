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
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator(Context ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
                                .Must((dto, n) => !ctx.Categories.Any(c => c.Name == n && dto.Id != c.Id))
                                .WithMessage("Name is already in use.");
        }
    }
}

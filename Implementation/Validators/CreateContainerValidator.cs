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
    public class CreateContainerValidator : AbstractValidator<CreateContainerDto>
    {
        public CreateContainerValidator(Context ctx) 
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches("(?=.{4,15}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
                .WithMessage("Invalid category format.")
                .Must(x => !ctx.Containers.Any(c => c.Name == x))
                .WithMessage("Container name already exist.");
        }
    }
}

using System;
using System.Collections.Generic;
using Application.UseCases.Commands.Containers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Application.DTO;
using DataAccess;
using Implementation.Validators;
using FluentValidation;

namespace Implementation.UseCases.Commands.Containers
{
    public class EfCreateContainerCommand : ICreateContainerCommand
    {
        public int Id => 5;

        public string Name => "CreateContainer";

        private Context _context;
        private CreateContainerValidator _validator;

        public EfCreateContainerCommand(Context context, CreateContainerValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(CreateContainerDto data)
        {
            _validator.ValidateAndThrow(data);

            Container container = new Container
            {
                Name = data.Name
            };

            _context.Containers.Add(container);
            _context.SaveChanges();
        }
    }
}

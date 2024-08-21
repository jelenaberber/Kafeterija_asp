using Application.DTO;
using Application.UseCases.Commands.Containers;
using System;
using DataAccess;
using Implementation.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using FluentValidation;

namespace Implementation.UseCases.Commands.Containers
{
    public class EfUpdateContainerCommand : IUpdateContainerCommand
    {
        public int Id => 6;

        public string Name => "UpdateContainer";

        private Context _context;
        private UpdateContainerValidator _validator;

        public EfUpdateContainerCommand(Context context, UpdateContainerValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(UpdateContainerDto data)
        {
            _validator.ValidateAndThrow(data);

            Container container = _context.Containers.FirstOrDefault(x => x.Id == data.Id);

            container.Name = data.Name;

            _context.SaveChanges();
        }
    }
}

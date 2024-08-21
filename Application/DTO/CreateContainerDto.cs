using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CreateContainerDto
    {
        public string Name { get; set; }
    }

    public class UpdateContainerDto : CreateContainerDto
    {
        public int Id { get; set; }
    }

    public class ContainterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Message : Entity
    {
        public string Text { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

    }
}

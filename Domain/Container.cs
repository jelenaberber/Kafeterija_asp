﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Container : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Packing> Packings { get; set; }
    }
}

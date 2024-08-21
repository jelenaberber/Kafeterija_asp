using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OrderDetail : Entity
    {
        public int OrderId { get; set; }
        public int PackingId { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Packing Packing { get; set; }


    }
}

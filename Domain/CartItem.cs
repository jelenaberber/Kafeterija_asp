using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CartItem : Entity
    {
        public int UserId { get; set; }
        public int PackingId { get; set; }
        public int Quantity { get; set; }

        public virtual Packing Packing { get; set; }
        public virtual User User { get; set; }

    }
}

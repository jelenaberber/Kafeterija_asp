using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Packing : Entity
    {
        public int ProductId { get; set; }
        public int ContainerId { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal Price { get; set; }
        public bool InFocus { get; set; }

        public virtual Product Product { get; set; }
        public virtual Container Container { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

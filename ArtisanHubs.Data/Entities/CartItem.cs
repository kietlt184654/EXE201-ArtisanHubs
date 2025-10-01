using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtisanHubs.Data.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; } // Khóa ngoại tới Cart
        public int ProductId { get; set; } // Khóa ngoại tới Product
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }
        // Navigation properties
        public virtual Cart Cart { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}

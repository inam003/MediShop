using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediShop.Model
{
    public class Cart
    {
        public int CartId { get; set; }
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public int MedicineId { get; set; }
        [ForeignKey("MedicineId")]
        public Medicine? Medicine { get; set; }

        public int Quantity { get; set; }

        [NotMapped]
        public double Total => (Medicine?.Price ?? 0) * Quantity;
    }

}

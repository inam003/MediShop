using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediShop.Model
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int MedicineId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }

        public int OrderId { get; set; }
        [ValidateNever]
        public Order? Order { get; set; }
        [ValidateNever]
        public Medicine? Medicine { get; set; }
    }
}

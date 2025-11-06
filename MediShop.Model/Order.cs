using OMSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediShop.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        //public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } // "Pending", "Completed", "Delivered"
        public double TotalAmount { get; set; }

        
        //public Customer Customer { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        //public Bill Bill { get; set; }
    }
}

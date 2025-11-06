using MediShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMSS.Domain.Entities
{
	public class Bill
	{
		public int BillId { get; set; }
		public int OrderId { get; set; }
		public decimal Amount { get; set; }
		public DateTime BillDate { get; set; }

		// Navigation
		public Order Order { get; set; }
	}
}

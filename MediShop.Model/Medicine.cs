using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediShop.Model;

public class Medicine
{
    public int MedicineId { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Category { get; set; }
    public string? Description { get; set; }
    [Required]
    public double Price { get; set; }
    public int StockQuantity { get; set; }
    [DisplayName("Image URL")]
    [ValidateNever]
    public string? ImageUrl { get; set; }

    [DisplayName("Supplier")]
    public int SupplierId { get; set; }
    [ForeignKey("SupplierId")]
    [ValidateNever]
    public Supplier? Supplier { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}
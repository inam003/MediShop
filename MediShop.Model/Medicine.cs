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
    [Required]
    public string? Description { get; set; }
    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Please enter a valid price")]
    public double? Price { get; set; }
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid stock quantity")]
    public int? StockQuantity { get; set; }
    [DisplayName("Image URL")]
    [ValidateNever]
    public string? ImageUrl { get; set; }

    [DisplayName("Supplier")]
    [Required(ErrorMessage = "Please select a supplier")]
    public int SupplierId { get; set; }
    [ForeignKey("SupplierId")]
    [ValidateNever]
    public Supplier? Supplier { get; set; }
    [ValidateNever]
    public ICollection<OrderDetail>? OrderDetails { get; set; }
}
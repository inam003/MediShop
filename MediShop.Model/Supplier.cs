using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MediShop.Model;

public class Supplier
{
    public int SupplierId { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? ContactNumber { get; set; }
    [Required]
    public string? Address { get; set; }
    [ValidateNever]
    public ICollection<Medicine>? Medicines { get; set; }
}
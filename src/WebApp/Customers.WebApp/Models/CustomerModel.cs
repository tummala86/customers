using System.ComponentModel.DataAnnotations;

namespace Customers.WebApp.Models;

public class CustomerModel
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "FirstName is required")]
    [StringLength(50, ErrorMessage = "FirstName value cannot be longer than 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "LastName is required")]
    [StringLength(50, ErrorMessage = "LastName cannot be longer than 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string Email { get; set; } = string.Empty;
}

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Xronopic.Api.Models
{
  public class User
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty; // Store the hashed password

    // Optional: Salt or other security-related fields
    public string? Salt { get; set; } // If using salting

    // Optional: Verification fields
    public bool IsEmailVerified { get; set; } = false;
    public string? VerificationToken { get; set; }

    // Optional: Password reset fields
    public string? PasswordResetToken { get; set; }
    public DateTime? PasswordResetTokenExpiry { get; set; }

    // Navigation property: One user can have many timelines
    public List<Timeline> Timelines { get; set; } = new();
  }
}
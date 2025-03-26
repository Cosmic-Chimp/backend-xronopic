using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Xronopic.Api.Models
{
  public class Timeline
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string TimelineTitle { get; set; } = string.Empty;

    public string TimelineDescription { get; set; } = string.Empty;

    // One timeline has many events
    public List<Event> Events { get; set; } = new();

    // Foreign key to the user who owns this timeline
    public string UserId { get; set; } = "";

    // Navigation property
    public User? User { get; set; }
  }
}
using System.ComponentModel.DataAnnotations;
using System;

namespace Xronopic.Api.Models
{
  public class Event
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string EventTitle { get; set; } = string.Empty;

    public string EventDescription { get; set; } = string.Empty;

    public DateTime? EventDate { get; set; }

    public string? ImgUrl { get; set; }

    // Foreign Key
    public int TimelineId { get; set; }
    public Timeline? Timeline { get; set; }
  }
}
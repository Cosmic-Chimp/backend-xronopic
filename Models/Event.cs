using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace Xronopic.Api.Models
{
  public class Event
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string EventTitle { get; set; } = string.Empty;

    public string EventDescription { get; set; } = string.Empty;

    public DateTimeOffset? EventDate { get; set; } = DateTimeOffset.UtcNow;
    public string? ImgUrl { get; set; }

    // Foreign Key
    public int TimelineId { get; set; }

    [JsonIgnore]
    public Timeline? Timeline { get; set; }
  }
}
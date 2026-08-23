using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ClassLib.Data.Entities;

public class Member
{
    public string Id { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string? ImageUrl { get; set; }
    public required string DisplayName { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedTs { get; set; } = DateTime.UtcNow;
    public DateTime LastActiveTs { get; set; } = DateTime.UtcNow;
    public required string Gender { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(Id))]
    public AppUser User { get; set; } = null!;

    [JsonIgnore]
    public List<Photo> Photos { get; set; } = [];

}

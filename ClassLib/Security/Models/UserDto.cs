using System;

namespace ClassLib.Security.Models;

public class UserDto
{
    public required string Id { get; set; }
    public required string  DisplayName { get; set; }
    public required string Email { get; set; }
    public string? ImageUrl { get; set; }   
    public required string Token { get; set; }

}

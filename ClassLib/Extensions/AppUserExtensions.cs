using System;
using ClassLib.Data.Entities;
using ClassLib.Security.Models;
using ClassLib.Security.Services;

namespace ClassLib.Extensions;

public static class AppUserExtensions
{
    public static UserDto ToDto(this AppUser user, ITokenService tokenService)
    {
        return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                ImageUrl = user.ImageUrl,
                Token = tokenService.CreateToken(user)
            };
    }
}

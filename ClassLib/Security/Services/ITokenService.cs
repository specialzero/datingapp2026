using System;
using ClassLib.Data.Entities;

namespace ClassLib.Security.Services;

public interface ITokenService
{
    string CreateToken(AppUser user);
}

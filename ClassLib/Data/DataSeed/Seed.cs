using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using ClassLib.Data.Contexts;
using ClassLib.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassLib.Data.DataSeed;

public class Seed
{
    public static async Task SeedUsers(DatingDbContext _dbcontext)
    {
        //if (await userManager.Users.AnyAsync()) return;

        var memberData = await File.ReadAllTextAsync("../ClassLib/Data/DataSeed/UserSeedData.json");
        var members = JsonSerializer.Deserialize<List<SeedUserDto>>(memberData);

        if (members == null)
        {
            Console.WriteLine("No members in seed data");
            return;
        }

        foreach (var member in members)
        {

            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                Id = member.Id,
                Email = member.Email,
                //UserName = member.Email,
                DisplayName = member.DisplayName,
                ImageUrl = member.ImageUrl,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")),
                PasswordSalt = hmac.Key,
                Member = new Member
                {
                    Id = member.Id,
                    DisplayName = member.DisplayName,
                    Description = member.Description,
                    DateOfBirth = member.DateOfBirth,
                    ImageUrl = member.ImageUrl,
                    Gender = member.Gender,
                    City = member.City,
                    Country = member.Country,
                    LastActiveTs = member.LastActive,
                    CreatedTs = member.Created
                }
            };

            user.Member.Photos.Add(new Photo
            {
                Url = member.ImageUrl!,
                MemberId = member.Id,
                //IsApproved = true
            });

            _dbcontext.Add(user);

            // var result = await userManager.CreateAsync(user, "Pa$$w0rd");
            // if (!result.Succeeded)
            // {
            //     Console.WriteLine(result.Errors.First().Description);
            // }
            // await userManager.AddToRoleAsync(user, "Member");
        }

        await _dbcontext.SaveChangesAsync();

        // var admin = new AppUser
        // {
        //     UserName = "admin@test.com",
        //     Email = "admin@test.com",
        //     DisplayName = "Admin"
        // };

        //await userManager.CreateAsync(admin, "Pa$$w0rd");
        //await userManager.AddToRolesAsync(admin, ["Admin", "Moderator"]);
    }

}

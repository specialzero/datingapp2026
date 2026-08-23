using System;
using ClassLib.Data.Contexts;
using ClassLib.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassLib.Data.Repos;

public class MemberRepo(DatingDbContext _dbcontext) : IMemberRepo
{
    public async Task<Member?> GetMemberByIdAsync(string id)
    {
        return await _dbcontext.Members.FindAsync(id);
    }

    public async Task<IReadOnlyList<Member>> GetMembersAsync()
    {
        return await _dbcontext.Members.ToListAsync();
    }

    public async Task<IReadOnlyList<Photo>> GetPhotosForMemberAsync(string memberId)
    {
        return await _dbcontext.Members
            .Where(x => x.Id == memberId)
            .SelectMany(x => x.Photos)
            .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _dbcontext.SaveChangesAsync() > 0;
    }

    public async void Update(Member member)
    {
        _dbcontext.Entry(member).State =  EntityState.Modified;
    }
}

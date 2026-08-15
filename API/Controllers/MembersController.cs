using ClassLib.Data.Contexts;
using ClassLib.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class MembersController(DatingDbContext context) : BaseApiController
    {
        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.AppUsers.ToListAsync();
            return members;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await context.AppUsers.FindAsync(id);
            
            if (member is null)
                return NotFound();
                
            return member;
        }
    }
}

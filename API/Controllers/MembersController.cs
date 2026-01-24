using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MembersController(AppDbContext context) : ControllerBase //localhost:5001/api/members
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync() ; 
            return members ; 

            
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<AppUser>> GetMembers(string id)
        {
            var member = await context.Users.FindAsync(id) ; 

            if (member is null)
            {
                return NotFound() ; 
            }

            return member ; 
        }

        
    }
}

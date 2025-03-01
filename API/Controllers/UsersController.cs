using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
    {
        var users = await userRepository.GetMembersAsync();

        return Ok(users);
    }

    // [HttpGet("{id:int}")] // api/users/id
    // public async Task<ActionResult<AppUser>> GetUser(int id)
    // {
    //     var user = await context.Users.FindAsync(id);

    //     if (user == null) return NotFound();

    //     return user;
    // }

    [HttpGet("{username}")] // api/users/username
    public async Task<ActionResult<MemberDTO>> GetUser(string username)
    {
        var user = await userRepository.GetMemberAsync(username);

        if (user == null) return NotFound();

        return user;
    }
}

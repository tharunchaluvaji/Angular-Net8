using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ErrorController(ApplicationDbContext dbContext) : BaseApiController
{
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetAuth()
    {
        return "Secret text";
    }

    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        var appUser = dbContext.Users.Find(-1);

        if (appUser == null) return NotFound();

        return appUser;
    }

    [HttpGet("server-error")]
    public ActionResult<AppUser> GetServerError()
    {
        var appUser = dbContext.Users.Find(-1) ?? throw new Exception("A bad thing has happened");

        return appUser;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This is not a good request");
    }
}

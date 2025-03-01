using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository(ApplicationDbContext dbContext, IMapper mapper) : IUserRepository
{
    public async Task<MemberDTO?> GetMemberAsync(string username)
    {
        return await dbContext.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
    {
        return await dbContext.Users
            .ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await dbContext.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUserNameAsync(string username)
    {
        return await dbContext.Users
            .Include(x => x.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await dbContext.Users
            .Include(x => x.Photos)
            .ToListAsync();
    }

    public async Task<bool> SaveAsync()
    {
        return await dbContext.SaveChangesAsync() > 0;
    }

    public void Update(AppUser user)
    {
        dbContext.Entry(user).State = EntityState.Modified;
    }
}

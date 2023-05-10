using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Repositories
{
    /// <summary>
    /// Returns the user by specified chat identifier (the unique identifier in telegram).
    /// </summary>
    public class UserRepository : IUserRepository
    {
        readonly DigichlistContext _context;

        public UserRepository(DigichlistContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<User>> GetAllAsync() => await _context.Users.AsNoTracking().ToListAsync();

        /// <inheritdoc />
        public async Task<User> GetByIdAsync(int id)
        {
            var result = await _context.Users
                .AsNoTracking()
                .FirstAsync(u => u.Id == id);

            return result;
        }

        /// <inheritdoc />
        public async Task<List<User>> GetUsersWithRolesAsync() => await _context.Users.Include(r => r.Role).ToListAsync();

        /// <inheritdoc />
        public async Task<User> GetUserWithRoleAsync(int id)
        {
            return await _context.Users.Include(r => r.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task<User> GetUserWithRolesAndDefectsByIdAsync(int id) =>
            await _context.Users
                .Include(r => r.Role)
                .Include(a => a.Defects)
                .FirstOrDefaultAsync(x => x.Id == id);

        /// <inheritdoc />
        public async Task<List<User>> GetRangeByIdsAsync(int[] idArr) => await _context.Users.Where(d => idArr.Contains(d.Id)).ToListAsync();

        /// <inheritdoc />
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteOneAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteRangeAsync(IEnumerable<User> users)
        {
            _context.RemoveRange(users);
            await _context.SaveChangesAsync();
        }
    }
}

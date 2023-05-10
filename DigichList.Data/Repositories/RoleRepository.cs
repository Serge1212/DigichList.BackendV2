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
    /// The dedicated repo for working with user roles.
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        readonly DigichlistContext _context;
        public RoleRepository(DigichlistContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<List<Role>> GetAllAsync() => await _context.Roles.ToListAsync();

        /// <inheritdoc />
        public async Task<Role> GetByIdAsync(int id) => await _context.Roles.FindAsync(id);

        /// <inheritdoc />
        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }
    }
}

using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Role> GetByIdAsync(int id) => await _context.Roles.FindAsync(id);

        /// <inheritdoc />
        public async Task<Role> GetRoleByNameAsync(string roleName) => await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName); //TODO: Id only maybe?

        /// <inheritdoc />
        public async Task<bool> AssignRoleAsync(User user, int roleId)
        {
            if(user == null)
                return false;

            var role = await GetByIdAsync(roleId);

            if(role == null)
                return false;
            
            // remove all assigned defects if a user is no longer a technician.
            if(user?.Role?.Name == "Technician") //TODO: why rely on a magic value?
            {
                _context
                    .Defects
                    .RemoveRange(_context.Defects.Where(x => x.AssignedWorker.Id == user.Id));
            }

            // assign new role.
            user.Role = role;
            user.IsRegistered = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
        /// <inheritdoc />
        public async Task<bool> RemoveRoleFromUserAsync(User user)
        {
            if (user == null)
                return false;

            await RemoveRoleAndAssignedDefectsAsync(user);
            return true;
        }

        private async Task RemoveRoleAndAssignedDefectsAsync(User user)
        {
            // remove all assigned defects for this user.
            _context.Defects.RemoveRange(user.Defects.Where(x => x.ClosedAt == null));
            user.Defects = null;
            user.Role = null;
            user.IsRegistered = false;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

    }
}

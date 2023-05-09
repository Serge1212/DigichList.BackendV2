using DigichList.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Backend.Interfaces
{
    /// <summary>
    /// The dedicated service for working with roles.
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Returns all roles.
        /// </summary>
        public Task<List<Role>> GetAllAsync();

        /// <summary>
        /// Returns the role by specified identifier.
        /// </summary>
        Task<Role> GetByIdAsync(int id);

        /// <summary>
        /// Updates the specified role.
        /// </summary>
        public Task UpdateAsync(Role model);

        /// <summary>
        /// Assignes the specified role to the specified user.
        /// </summary>
        Task<(bool, string)> AssignAsync(int userId, int roleId);

        /// <summary>
        /// Removes the specified role from the specified user.
        /// </summary>
        Task<(bool, string)> RemoveRoleFromUserAsync(int userId);
    }
}

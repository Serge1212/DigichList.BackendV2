using DigichList.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    /// <summary>
    /// The dedicated repo for working with user roles.
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Returns all roles.
        /// </summary>
        Task<List<Role>> GetAllAsync();

        /// <summary>
        /// Returns the role by specified identifier.
        /// </summary>
        Task<Role> GetByIdAsync(int id);

        /// <summary>
        /// Updates the specified role.
        /// </summary>
        public Task UpdateAsync(Role role);
    }
}

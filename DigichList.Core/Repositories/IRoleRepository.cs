using DigichList.Core.Entities;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    /// <summary>
    /// The dedicated repo for working with user roles.
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Returns the role by specified identifier.
        /// </summary>
        Task<Role> GetByIdAsync(int id);

        /// <summary>
        /// Returns the role by specified name.
        /// </summary>
        public Task<Role> GetRoleByNameAsync(string roleName); //TODO: Id maybe?

        /// <summary>
        /// Assignes the role for specified user.
        /// </summary>
        public Task<bool> AssignRoleAsync(User user, int roleId);

        /// <summary>
        /// Removes the role from specified user.
        /// </summary>
        public Task<bool> RemoveRoleFromUserAsync(User user);
    }
}

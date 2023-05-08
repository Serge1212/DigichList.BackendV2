using DigichList.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    /// <summary>
    /// The dedicated repo for working with users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Returns the user by specified chat identifier (the unique identifier in telegram).
        /// </summary>
        public Task<User> GetUserByChatIdAsync(int chatId);

        /// <summary>
        /// Returns the user by specified chat identifier (the unique identifier in telegram) grabbing the related role.
        /// </summary>
        public Task<User> GetUserByChatIdWithRoleAsync(int chatId);

        /// <summary>
        /// Gets all users with related roles.
        /// </summary>
        public IEnumerable<User> GetUsersWithRoles();

        public IEnumerable<User> GetTechnicians(); //TODO: consider delete

        public IEnumerable<User> GetUsersWithRolesAndAssignedDefects(); //TODO: consider delete

        public Task<User> GetUserWithRoleAsync(int id); //TODO: why 2 methods with chatId and identity? consider delete.

        public Task<User> GetUserWithRolesAndAssignedDefectsByIdAsync(int id); //TODO: consider delete

        /// <summary>
        /// Deletes the specified users.
        /// </summary>
        public Task DeleteRangeAsync(int[] idArr);
    }
}

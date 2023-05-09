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
        /// Returns the user by specified identifier.
        /// </summary>
        public Task<User> GetByIdAsync(int id);

        /// <summary>
        /// Returns the user by specified chat identifier (the unique identifier in telegram).
        /// </summary>
        public Task<User> GetUserByChatIdAsync(int chatId);

        /// <summary>
        /// Returns the user by specified chat identifier (the unique identifier in telegram) grabbing the related role.
        /// </summary>
        public Task<User> GetUserByChatIdWithRoleAsync(int chatId);

        /// <summary>
        /// Returns all users with related roles.
        /// </summary>
        public Task<List<User>> GetUsersWithRolesAsync();

        /// <summary>
        /// Returns users by specified identifiers.
        /// </summary>
        public Task<List<User>> GetRangeByIdsAsync(int[] idArr);

        public IEnumerable<User> GetUsersWithRolesAndAssignedDefects(); //TODO: consider delete

        public Task<User> GetUserWithRoleAsync(int id); //TODO: why 2 methods with chatId and identity? consider delete.

        public Task<User> GetUserWithRolesAndAssignedDefectsByIdAsync(int id); //TODO: consider delete

        /// <summary>
        /// Adds a brand new user.
        /// </summary>
        public Task AddAsync(User user);

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        public Task UpdateAsync(User user);

        /// <summary>
        /// Deletes one specified user.
        /// </summary>
        public Task DeleteOneAsync(User user);

        /// <summary>
        /// Deletes the specified users.
        /// </summary>
        public Task DeleteRangeAsync(IEnumerable<User> users);
    }
}

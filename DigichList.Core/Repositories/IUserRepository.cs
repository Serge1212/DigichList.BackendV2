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
        /// Returns all users.
        /// </summary>
        Task<IReadOnlyList<User>> GetAllAsync();

        /// <summary>
        /// Returns the user by specified identifier.
        /// </summary>
        public Task<User> GetByIdAsync(int id);

        /// <summary>
        /// Returns all users with related roles.
        /// </summary>
        public Task<List<User>> GetUsersWithRolesAsync();

        /// <summary>
        /// Returns users by specified identifiers.
        /// </summary>
        public Task<List<User>> GetRangeByIdsAsync(int[] idArr);

        /// <summary>
        /// Returns the user with role by specified user identifier.
        /// </summary>
        public Task<User> GetUserWithRoleAsync(int id);

        /// <summary>
        /// Returns the user with role and defects by specified user identifier.
        /// </summary>
        public Task<User> GetUserWithRolesAndDefectsByIdAsync(int id);

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

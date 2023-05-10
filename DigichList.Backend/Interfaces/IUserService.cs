using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Backend.Interfaces
{
    /// <summary>
    /// The dedicated service for working with users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Returns a user with role info by specified identifier.
        /// </summary>
        public Task<UserViewModel> GetUserWithRoleAsync(int id);

        /// <summary>
        /// Returns all users with roles.
        /// </summary>
        public Task<List<UserViewModel>> GetUsersWithRolesAsync();

        /// <summary>
        /// Returns all users with Technician role.
        /// </summary>
        public Task<List<UserViewModel>> GetTechniciansAsync();

        /// <summary>
        /// Returns all registered users.
        /// </summary>
        public Task<List<UserViewModel>> GetRegisteredUsersAsync();

        /// <summary>
        /// Returns all not registered users.
        /// </summary>
        Task<List<UserViewModel>> GetUnregisteredUsersAsync();

        /// <summary>
        /// Adds a brand new user.
        /// </summary>
        public Task AddAsync(User user);

        /// <summary>
        /// Adds a brand new user.
        /// </summary>
        public Task<bool> UpdateAsync(User user);

        /// <summary>
        /// Deletes one specified user.
        /// </summary>
        public Task<bool> DeleteOneAsync(int id);

        /// <summary>
        /// Deletes many specified users.
        /// </summary>
        public Task<bool> DeleteManyAsync(int[] ids);
    }
}

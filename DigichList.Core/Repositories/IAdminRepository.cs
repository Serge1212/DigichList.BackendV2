using DigichList.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    /// <summary>
    /// The dedicated repo for working with admins.
    /// </summary>
    public interface IAdminRepository
    {
        /// <summary>
        /// Gets all admins.
        /// </summary>
        Task<IReadOnlyList<Admin>> GetAllAsync();

        /// <summary>
        /// Returns the admin by specified identifier.
        /// </summary>
        public Task<Admin> GetByIdAsync(int id);

        /// <summary>
        /// Returns the admin by specified email.
        /// </summary>
        public Task<Admin> GetAdminByEmailAsync(string email);

        /// <summary>
        /// Deletes multiple admins by specified identifiers.
        /// </summary>
        public Task DeleteRangeAsync(int[] idArr);

        /// <summary>
        /// Adds a brand new admin.
        /// </summary>
        Task AddAsync(Admin admin);

        /// <summary>
        /// Updates the specified admin.
        /// </summary>
        Task UpdateAsync(Admin admin);

        /// <summary>
        /// Deletes the specified admin.
        /// </summary>
        Task DeleteAsync(Admin admin);
    }
}

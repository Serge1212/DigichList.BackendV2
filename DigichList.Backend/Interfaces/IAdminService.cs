using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Backend.Interfaces
{
    /// <summary>
    /// The dedicated service for working with admins.
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Gets all admins.
        /// </summary>
        Task<IReadOnlyList<AdminViewModel>> GetAllAsync();

        /// <summary>
        /// Returns the admin by specified identifier.
        /// </summary>
        public Task<AdminViewModel> GetByIdAsync(int id);

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
        /// Updates the specified admin info.
        /// </summary>
        Task UpdateAdminAsync(Admin admin);

        /// <summary>
        /// Updates the password for specified user.
        /// </summary>
        Task<bool> UpdatePasswordAsync(ChangeAdminPasswordViewModel model);

        /// <summary>
        /// Verifies the admin on logging in.
        /// </summary>
        Task<(bool, int)> VerifyAdminAsync(LoginViewModel request);

        /// <summary>
        /// Deletes the specified admin.
        /// </summary>
        Task DeleteAsync(int id);
    }
}

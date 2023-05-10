using DigichList.Backend.Interfaces;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Backend.Services
{
    /// <summary>
    /// The dedicated service for working with admins.
    /// </summary>
    public class AdminService : IAdminService
    {
        readonly IAdminRepository _repo;

        public AdminService(IAdminRepository repo)
        {
            _repo = repo;
        }

        /// <inheritdoc />
        public async Task DeleteRangeAsync(int[] idArr) => await _repo.DeleteRangeAsync(idArr);

        /// <inheritdoc />
        public async Task<Admin> GetAdminByEmailAsync(string email) => await _repo.GetAdminByEmailAsync(email);

        /// <inheritdoc />
        public async Task<IReadOnlyList<AdminViewModel>> GetAllAsync()
        {
            var result = await _repo.GetAllAsync();
            var mapped = new List<AdminViewModel>();

            foreach(var a in result)
            {
                mapped.Add(AdminViewModel.ToViewModel(a));
            }

            return mapped.AsReadOnly();
        }

        /// <inheritdoc />
        public async Task<AdminViewModel> GetByIdAsync(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            return AdminViewModel.ToViewModel(result);
        }

        /// <inheritdoc />
        public async Task AddAsync(Admin admin)
        {
            admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
            await _repo.AddAsync(admin);
        }

        /// <inheritdoc />
        public async Task UpdateAdminAsync(Admin admin)
        {
            var existingAdmin = await _repo.GetByIdAsync(admin.Id);

            // Map new info.
            existingAdmin.FirstName = admin.FirstName;
            existingAdmin.LastName = admin.LastName;
            existingAdmin.Email = admin.Email;
            existingAdmin.AccessLevel = admin.AccessLevel;

            // Update.
            await _repo.UpdateAsync(existingAdmin);
        }

        /// <inheritdoc />
        public async Task<bool> UpdatePasswordAsync(ChangeAdminPasswordViewModel model)
        {
            var admin = await _repo.GetByIdAsync(model.Id);

            if (admin is null)
            {
                return false;
            }

            admin.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            await _repo.UpdateAsync(admin);
            return true;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            var admin = await _repo.GetByIdAsync(id);
            await _repo.DeleteAsync(admin);
        }

        public async Task<(bool, int)> VerifyAdminAsync(LoginViewModel request)
        {
            var admin = await _repo.GetAdminByEmailAsync(request.Email);

            if (admin == null)
            {
                return (false, 0);
            }

            var passwordsMatch = BCrypt.Net.BCrypt.Verify(request.Password, admin.Password);
            if (!passwordsMatch)
            {
                return (false, 0);
            }

            return (true, admin.Id);
        }
    }
}

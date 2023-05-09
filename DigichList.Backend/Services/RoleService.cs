using DigichList.Backend.Interfaces;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.TelegramNotifications.BotNotifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Backend.Services
{
    /// <summary>
    /// The dedicated service for working with roles.
    /// </summary>
    public class RoleService : IRoleService
    {
        readonly IRoleRepository _roleRepository;
        readonly IUserRepository _userRepository;
        readonly IBotNotificationSender _botNotificationSender;

        public RoleService(
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            IBotNotificationSender botNotificationSender
            )
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _botNotificationSender = botNotificationSender;
        }

        /// <inheritdoc />
        public async Task<List<Role>> GetAllAsync() => await _roleRepository.GetAllAsync();

        /// <inheritdoc />
        public async Task<Role> GetByIdAsync(int id) => await _roleRepository.GetByIdAsync(id);

        /// <inheritdoc />
        public Task UpdateAsync(Role model) => _roleRepository.UpdateAsync(model);

        /// <inheritdoc />
        public async Task<(bool, string)> AssignAsync(int userId, int roleId)
        {
            var user = await _userRepository.GetUserWithRoleAsync(userId);
            var role = await _roleRepository.GetByIdAsync(roleId);

            if (user is null)
            {
                return (false, "User not found");
            }

            if (role is null)
            {
                return (false, "Role not found");
            }

            // Remove all assigned defects.
            if (user?.Role?.Name != "Technician")
            {
                user.Defects = null;
            }

            // Update user.
            user.Role = role;
            user.IsRegistered = true;
            await _userRepository.UpdateAsync(user);

            // Notify user.
            await _botNotificationSender.NotifyUserGotRole(user.ChatId, user.Role.Name);

            return (true, "");
        }

        /// <inheritdoc />
        public async Task<(bool, string)> RemoveRoleFromUserAsync(int userId)
        {
            var user = await _userRepository.GetUserWithRolesAndAssignedDefectsByIdAsync(userId);

            if (user == null)
            {
                return (false, "The user was not found");
            }

            if (user.Role == null)
            {
                return (false, "The user has no role specified");
            }

            var roleName = user.Role.Name;

            // Update user.
            user.Role = null;
            user.IsRegistered = false;
            user.Defects = null;
            await _userRepository.UpdateAsync(user);

            // Notify user.
            await _botNotificationSender.NotifyUserLostRole(user.ChatId, roleName);

            return (true, "");
        }
    }
}

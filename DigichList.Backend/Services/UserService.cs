using DigichList.Backend.Interfaces;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.TelegramNotifications.BotNotifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigichList.Backend.Services
{
    /// <summary>
    /// The dedicated service for working with users.
    /// </summary>
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;
        readonly IBotNotificationSender _botNotificationSender;

        public UserService(IUserRepository userRepository, IBotNotificationSender botNotificationSender)
        {
            _userRepository = userRepository;
            _botNotificationSender = botNotificationSender;
        }

        /// <inheritdoc />
        public async Task<UserViewModel> GetUserWithRoleAsync(int id)
        {
            var result = await _userRepository.GetUserWithRoleAsync(id);
            return UserViewModel.ToViewModel(result);
        }

        /// <inheritdoc />
        public async Task<List<UserViewModel>> GetUsersWithRolesAsync()
        {
            var users = await _userRepository.GetUsersWithRolesAsync();
            var mapped = users.Select(u => UserViewModel.ToViewModel(u)).ToList();
            return mapped;
        }

        /// <inheritdoc />
        public async Task<List<UserViewModel>> GetTechniciansAsync()
        {
            var result = (await _userRepository.GetUsersWithRolesAsync()).Where(u => u.Role?.Name == "Technician").ToList();
            var mapped = result.Select(u => UserViewModel.ToViewModel(u)).ToList();
            return mapped;
        }
        

        /// <inheritdoc />
        public async Task<List<UserViewModel>> GetRegisteredUsersAsync()
        {
            var result = (await _userRepository.GetUsersWithRolesAsync()).Where(u => u.IsRegistered).ToList();
            var mapped = result.Select(u => UserViewModel.ToViewModel(u)).ToList();
            return mapped;
        }

        /// <inheritdoc />
        public async Task<List<UserViewModel>> GetUnregisteredUsersAsync()
        {
            var users = (await _userRepository.GetUsersWithRolesAsync());
            var result = users.Where(u => !u.IsRegistered).ToList();
            var mapped = result.Select(u => UserViewModel.ToViewModel(u)).ToList();
            return mapped;
        }

        /// <inheritdoc />
        public async Task AddAsync(User user) => await _userRepository.AddAsync(user);

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(User user)
        {
            if (user == null)
            {
                return false;
            }

            if ((await _userRepository.GetByIdAsync(user.Id)) == null)
            {
                return false;
            }

            await _userRepository.UpdateAsync(user);
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteOneAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return false;
            }

            // Delete.
            await _userRepository.DeleteOneAsync(user);

            // Notify.
            await NotifyOnDeteleAsync(user.ChatId);

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteManyAsync(int[] ids)
        {
            var users = await _userRepository.GetRangeByIdsAsync(ids);

            if (users == null)
            {
                return false;
            }

            // Delete.
            await _userRepository.DeleteRangeAsync(users);

            // Notify.
            foreach (var u in users)
            {
                await NotifyOnDeteleAsync(u.ChatId);
            }

            return true;
        }

        async Task NotifyOnDeteleAsync(long chatId)
        {
            await _botNotificationSender.NotifyUserIsOrIsNotRegistered(chatId, false);
        }
    }
}

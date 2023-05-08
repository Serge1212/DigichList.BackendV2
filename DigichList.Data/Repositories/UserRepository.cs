using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Context;
using DigichList.TelegramNotifications.BotNotifications;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Repositories
{
    /// <summary>
    /// Returns the user by specified chat identifier (the unique identifier in telegram).
    /// </summary>
    public class UserRepository : IUserRepository
    {
        readonly IBotNotificationSender _botNotificationSender;
        readonly DigichlistContext _context;

        public UserRepository(DigichlistContext context, IBotNotificationSender botNotificationSender = null)
        {
            _context = context;
            _botNotificationSender = botNotificationSender;
        }

        /// <inheritdoc />
        public async Task<User> GetUserByChatIdAsync(int chatId) => await _context.Users.FirstOrDefaultAsync(x => x.ChatId == chatId);

        /// <inheritdoc />
        public async Task<User> GetUserByChatIdWithRoleAsync(int telegramId) =>
            await _context.Users
            .Include(r => r.Role)
            .FirstOrDefaultAsync(x => x.ChatId == telegramId);

        /// <inheritdoc />
        public IEnumerable<User> GetUsersWithRoles() => _context.Users.Include(r => r.Role);

        /// <inheritdoc />
        public IEnumerable<User> GetTechnicians() //TODO: GetUsersByRoleName(string roleName)
        {
            return _context.Users
                .Include(r => r.Role)
                .Where(x => x.Role.Name == "Technician");
        }

        /// <inheritdoc />
        public async Task<User> GetUserWithRoleAsync(int id)
        {
            return await _context.Users.Include(r => r.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public IEnumerable<User> GetUsersWithRolesAndAssignedDefects() =>
            _context.Users
                .Include(r => r.Role)
                .Include(a => a.Defects);

        /// <inheritdoc />
        public async Task<User> GetUserWithRolesAndAssignedDefectsByIdAsync(int id) =>
            await _context.Users
                .Include(r => r.Role)
                .Include(a => a.Defects)
                .FirstOrDefaultAsync(x => x.Id == id);

        /// <inheritdoc />
        public async Task DeleteRangeAsync(int[] idArr)
        {
            var usersToDelete = GetRangeByIds(idArr);
            _context.RemoveRange(usersToDelete);
            await NotifyUsersTheyWereRemovedFromDatabase(usersToDelete);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public IEnumerable<User> GetRangeByIds(int[] idArr)
        {
            return _context.Users.Where(d => idArr.Contains(d.Id));
        }

        private async Task NotifyUsersTheyWereRemovedFromDatabase(IEnumerable<User> usersToDelete)
        {
            foreach(var u in usersToDelete)
            {
                await _botNotificationSender.NotifyUserIsOrIsNotRegistered(u.ChatId, false);
            }
        }
    }
}

using DigichList.Core.Entities;
using System.Threading.Tasks;

namespace DigichList.TelegramNotifications.BotNotifications
{
    public interface IBotNotificationSender
    {
        public Task NotifyUserIsOrIsNotRegistered(long chatId, bool registrationStatus);
        public Task NotifyUserWasGivenWithDefect(long chatId, Defect defect);
        public Task NotifyUserGotRole(long chatId, string roleName);
        public Task NotifyUserLostRole(long chatId, string roleName);

    }
}

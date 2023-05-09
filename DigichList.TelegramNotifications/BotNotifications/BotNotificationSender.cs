using static DigichList.TelegramNotifications.Helpers.TelegramBotTextMessages;
using static DigichList.TelegramNotifications.Helpers.TelegramBotMessageSender;
using DigichList.Core.Entities;
using System.Threading.Tasks;

namespace DigichList.TelegramNotifications.BotNotifications
{
    public class BotNotificationSender : IBotNotificationSender
    {
        public async Task NotifyUserIsOrIsNotRegistered(long chatId, bool registrationStatus)
        {
            await (registrationStatus ?
                SendMessageAsync(chatId, UserGotRegistered) :
                SendMessageAsync(chatId, UserWasNotRegistered));
        }
        public async Task NotifyUserWasGivenWithDefect(long chatId, Defect defect)
        {
            var message = string.Format(UserGotDefect, defect.ToString());
            await SendMessageAsync(chatId, message);
        }

        public async Task NotifyUserHisDefectGotApproved(long chatId, string defectDescription)
        {
            var message = string.Format(UsersDefectGotApproved, defectDescription);
            await SendMessageAsync(chatId, message);
        }

        public async Task NotifyUserGotRole(long chatId, string roleName)
        {
            var message = GetMessageForSpecifiedRole(roleName, UserGotRole);
            await SendMessageAsync(chatId, message);
        }

        private string GetMessageForSpecifiedRole(string roleName, string message)
        {
            if (roleName == "Maid")
            {
                return string.Concat(message, MaidRoleDescription);
            }
            else if(roleName == "Technician")
            {
                return string.Concat(message, TechnicianRoleDescription);
            }
            return string.Empty;
        }

        public async Task NotifyUserLostRole(long chatId, string roleName)
        {
            var message = GetMessageForSpecifiedRole(roleName, "На жаль, ви втратили наступні повноваження:\n");
            await SendMessageAsync(chatId, message);
        }
    }
}

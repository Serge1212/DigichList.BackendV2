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
                SendMessageAsync(chatId, USER_REGISTERED) :
                SendMessageAsync(chatId, USER_NOT_REGISTERED));
        }
        public async Task NotifyUserWasGivenWithDefect(long chatId, Defect defect)
        {
            var message = string.Format(USER_GOT_DEFECT, defect.ToString());
            await SendMessageAsync(chatId, message);
        }

        public async Task NotifyUserGotRole(long chatId, string roleName)
        {
            var message = GetMessageForSpecifiedRole(roleName, USER_GOT_ROLE);
            await SendMessageAsync(chatId, message);
        }

        private string GetMessageForSpecifiedRole(string roleName, string message)
        {
            if (roleName == "Maid")
            {
                return string.Concat(message, MAID_ROLE_DESCRIPTION);
            }
            else if(roleName == "Technician")
            {
                return string.Concat(message, TECHNICIAN_ROLE_DESCRIPTION);
            }
            return string.Empty;
        }

        public async Task NotifyUserLostRole(long chatId, string roleName)
        {
            var message = GetMessageForSpecifiedRole(roleName, "Unfortunately you've lost the following capabilities:\n");
            await SendMessageAsync(chatId, message);
        }
    }
}

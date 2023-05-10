using System;
using Telegram.Bot;

namespace DigichList.TelegramNotifications.BotConfig
{
    internal static class BotClient
    {
        internal static readonly TelegramBotClient Bot = new TelegramBotClient(Environment.GetEnvironmentVariable("DigichlistV2", EnvironmentVariableTarget.User));
    }
}

 namespace DigichList.TelegramNotifications.Helpers
{
    internal static class TelegramBotTextMessages
    {
        internal const string UserGotRegistered = "Congratulations! Your registration request has been approved.";
        internal const string UserWasNotRegistered = "Unfortunately, your registration request has been declined.";
        internal const string UserGotDefect = "You've been assigned with a defect:\n{0}";
        internal const string UserGotRole = "Congratulations! You've been given with a role. Now you can:\n";
        internal const string MaidRoleDescription = "Publish defects.";
        internal const string TechnicianRoleDescription = "Publish defects.\nFix defects.";
    }
}

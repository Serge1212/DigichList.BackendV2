 namespace DigichList.TelegramNotifications.Helpers
{
    internal static class TelegramBotTextMessages
    {
        internal const string USER_REGISTERED = "Congratulations! Your registration request has been approved.";
        internal const string USER_NOT_REGISTERED = "Unfortunately, your registration request has been declined.";
        internal const string USER_GOT_DEFECT = "You've been assigned with a defect:\n{0}";
        internal const string USER_GOT_ROLE = "Congratulations! You've been given with a role. Now you can:\n";
        internal const string MAID_ROLE_DESCRIPTION = "Publish defects.";
        internal const string TECHNICIAN_ROLE_DESCRIPTION = "Publish defects.\nFix defects.";
    }
}

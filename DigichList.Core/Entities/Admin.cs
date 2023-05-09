using DigichList.Core.Enums;

namespace DigichList.Core.Entities
{
    /// <summary>
    /// Contains information about administrators.
    /// </summary>
    public class Admin
    {
        /// <summary>
        /// The admin's identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The admin's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The admin's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The admin's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The admin's password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The related access level.
        /// </summary>
        public AccessLevels AccessLevel { get; set; }
    }
}

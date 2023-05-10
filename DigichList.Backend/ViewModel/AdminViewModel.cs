using DigichList.Core.Entities;
using DigichList.Core.Enums;

namespace DigichList.Backend.ViewModel
{
    /// <summary>
    /// The view model that represents the admin information.
    /// </summary>
    public class AdminViewModel
    {
        /// <summary>
        /// The admin's related identifier.
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
        /// The admin's access level.
        /// </summary>
        public string AccessLevel { get; set; }

        public static AdminViewModel ToViewModel(Admin admin)
        {
            return new AdminViewModel
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                AccessLevel = (AccessLevels)admin.AccessLevel == AccessLevels.Admin ? "Admin" : "Superadmin",
            };
        }
    }
}

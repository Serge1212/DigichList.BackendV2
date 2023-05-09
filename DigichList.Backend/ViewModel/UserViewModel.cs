using DigichList.Core.Entities;

namespace DigichList.Backend.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool IsRegistered { get; set; }
        public string RoleName { get; set; }
        public int? RoleId { get; set; }

        public static UserViewModel ToViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                IsRegistered = user.IsRegistered,
                RoleName = user.Role?.Name,
                RoleId = user.Role?.Id,
            };
        }
    }
}

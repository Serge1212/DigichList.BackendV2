using DigichList.Core.Entities;

namespace DigichList.Backend.ViewModel
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public static RoleViewModel ToViewModel(Role role)
        {
            return new RoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
            };
        }
    }
}

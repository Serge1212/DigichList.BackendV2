﻿using DigichList.Core.Entities;
using DigichList.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    public interface IUserRepository : IRepository<User, int>
    {
        public Task<User> GetUserByTelegramIdAsync(int telegramId);

        public Task<User> GetUserByTelegramIdWithRoleAsync(int telegramId);

        public IEnumerable<User> GetUsersWithRoles();

        public IEnumerable<User> GetTechnicians();

        public IEnumerable<User> GetUsersWithRolesAndAssignedDefects();

        public Task<User> GetUserWithRoleAsync(int id);

        public Task<User> GetUserWithRolesAndAssignedDefectsByIdAsync(int id);

        public Task DeleteRangeAsync(int[] idArr);

    }
}

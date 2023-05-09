using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Repositories
{
    /// <summary>
    /// The dedicated repo for working with admins.
    /// </summary>
    public class AdminRepository : IAdminRepository
    {
        readonly DigichlistContext _context;
        public AdminRepository(DigichlistContext context)
        {
            _context = context;
        }

        public AdminRepository()
        {
        }

        /// <inheritdoc />
        public async Task<Admin> GetByIdAsync(int id) => await _context.Admins.FirstAsync(x => x.Id == id);

        /// <inheritdoc />
        public IEnumerable<Admin> GetRangeByIds(int[] idArr) => _context.Admins.Where(d => idArr.Contains(d.Id));

        public async Task<IReadOnlyList<Admin>> GetAllAsync() => await _context.Admins.ToListAsync();

        /// <inheritdoc />
        public async Task<Admin> GetAdminByEmailAsync(string email) => await _context.Admins.FirstOrDefaultAsync(x => x.Email == email);

        /// <inheritdoc />
        public async Task DeleteRangeAsync(int[] idArr)
        {
            var defectsToDelete = GetRangeByIds(idArr);
            _context.RemoveRange(defectsToDelete);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task AddAsync(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Admin admin)
        {
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Admin admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
        }
    }
}

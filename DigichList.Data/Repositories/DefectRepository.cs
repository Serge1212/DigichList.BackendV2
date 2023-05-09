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
    /// The dedicated repo for working with defects.
    /// </summary>
    public class DefectRepository : IDefectRepository
    {
        readonly DigichlistContext _context;
        public DefectRepository(DigichlistContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public List<Defect> GetAll() =>
            _context.Defects
            .Include(u => u.AssignedWorker)
            .ToList();

        /// <inheritdoc />
        public async Task<Defect> GetByIdAsync(int defectId) =>
            await _context.Defects
                 .Include(w => w.AssignedWorker)
                 .FirstOrDefaultAsync(x => x.Id == defectId);

        /// <inheritdoc />
        public IEnumerable<Defect> GetRangeByIds(int[] idArr)
        {
            return _context.Defects.Where(d => idArr.Contains(d.Id));
        }

        /// <inheritdoc />
        public IEnumerable<Defect> GetDefectsWithUsersAndAssignedDefects()
        {
            return _context.Defects
                .Include(u => u.AssignedWorker);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Defect defect)
        {
            _context.Defects.Update(defect);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteRangeAsync(int[] idArr)
        {
            var defectsToDelete = GetRangeByIds(idArr);
            _context.RemoveRange(defectsToDelete);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Defect defect)
        {
            _context.Defects.Remove(defect);
            await _context.SaveChangesAsync();
        }
    }
}

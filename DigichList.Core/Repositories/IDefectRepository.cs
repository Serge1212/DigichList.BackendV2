using DigichList.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    /// <summary>
    /// The dedicated repo for working with defects.
    /// </summary>
    public interface IDefectRepository
    {
        //TODO: comment.
        public IEnumerable<Defect> GetAllAsNoTracking();
        //TODO: comment.
        public Task<Defect> GetDefectWithAssignedDefectByIdAsync(int defectId);
        //TODO: comment.
        public Task DeleteRangeAsync(int[] idArr);
        //TODO: comment.
        public IEnumerable<Defect> GetRangeByIds(int[] idArr);
    }
}

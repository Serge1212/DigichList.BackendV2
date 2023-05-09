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
        /// <summary>
        /// Returns all defects.
        /// </summary>
        public List<Defect> GetAll();

        /// <summary>
        /// Updates the specified defect.
        /// </summary>
        public Task UpdateAsync(Defect defect);

        /// <summary>
        /// Deletes the specified defect.
        /// </summary>
        public Task DeleteAsync(Defect defect);

        //TODO: comment.
        public Task<Defect> GetByIdAsync(int defectId);
        //TODO: comment.
        public Task DeleteRangeAsync(int[] idArr);
        //TODO: comment.
        public IEnumerable<Defect> GetRangeByIds(int[] idArr);
    }
}

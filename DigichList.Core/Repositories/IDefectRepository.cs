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

        /// <summary>
        /// Returns the defect by specified identifier.
        /// </summary>
        public Task<Defect> GetByIdAsync(int defectId);

        /// <summary>
        /// Deletes the specified defects by specified identifiers.
        /// </summary>
        public Task DeleteRangeAsync(int[] idArr);

        /// <summary>
        /// Returns the range of defects by specified identifiers.
        /// </summary>
        public IEnumerable<Defect> GetRangeByIds(int[] idArr);
    }
}

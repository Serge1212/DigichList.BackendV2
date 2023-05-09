using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Backend.Interfaces
{
    /// <summary>
    /// The dedicated service for working with defects.
    /// </summary>
    public interface IDefectService
    {
        /// <summary>
        /// Returns all defects.
        /// </summary>
        public List<Defect> GetAll();

        /// <summary>
        /// Returns single defect by specified identifier.
        /// </summary>
        public Task<Defect> GetByIdAsync(int id);

        /// <summary>
        /// Updates the specified defect.
        /// </summary>
        public Task UpdateAsync(DefectViewModel model);

        /// <summary>
        /// Assigns the specified defect to the specified user.
        /// </summary>
        public Task<(bool, string)> AssignAsync(int userId, int defectId);

        /// <summary>
        /// Deletes the specified defect.
        /// </summary>
        public Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Deletes many specified defects.
        /// </summary>
        public Task DeleteRangeAsync(int[] ids);
    }
}

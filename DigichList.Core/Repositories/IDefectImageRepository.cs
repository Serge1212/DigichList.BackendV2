using DigichList.Core.Entities;
using System.Threading.Tasks;

namespace DigichList.Core.Repositories
{
    /// <summary>
    /// The dedicated repo for working with defect image.
    /// </summary>
    public interface IDefectImageRepository
    {
        /// <summary>
        /// Saves the defect image as byte string.
        /// </summary>
        public Task<DefectImage> SaveImage(string path);
    }
}

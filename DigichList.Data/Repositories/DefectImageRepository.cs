using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Repositories
{
    /// <summary>
    /// The dedicated repo for working with defect image.
    /// </summary>
    public class DefectImageRepository : IDefectImageRepository
    {

        //TODO: it's odd, consider delete or remake.
        /// <inheritdoc />
        public async Task<DefectImage> SaveImage(string path)
        {
            var imageArray = await File.ReadAllBytesAsync(path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            DefectImage defectImage = new DefectImage
            {
                Path = base64ImageRepresentation
            };
            return defectImage;
        }
    }
}

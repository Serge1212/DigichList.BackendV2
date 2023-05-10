using DigichList.Backend.Enums;
using DigichList.Core.Entities;
using System;

namespace DigichList.Backend.ViewModel
{
    /// <summary>
    /// The view model that reflects <see cref="Defect"/>s information.
    /// </summary>
    public class DefectViewModel
    {
        /// <summary>
        /// The defect's identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The defect's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The date and time this defect was created.
        /// </summary>
        public string CreatedAt { get; set; }

        /// <summary>
        /// The room number where the defect was found.
        /// </summary>
        public int RoomNumber { get; set; }

        /// <summary>
        /// The user name that published this defect.
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// The name of assigned user to fix this defect.
        /// </summary>
        public string Assignee { get; set; }

        /// <summary>
        /// The identifier of assigned user to fix this defect.
        /// </summary>
        public int? AssigneeId { get; set; }

        /// <summary>
        /// Current status for this defect.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The date and time the status was last changed.
        /// </summary>
        public string StatusChangedAt { get; set; }

        public static DefectViewModel ToViewModel(Defect defect)
        {
            return new DefectViewModel
            {
                Id = defect.Id,
                Description = defect.Description,
                CreatedAt = defect.CreatedAt.ToShortDateString(),
                RoomNumber = defect.RoomNumber,
                Assignee = defect.AssignedWorker.FirstName, //TODO: make more informative
                AssigneeId = defect.AssignedWorker?.Id,
                Status = ResolveStatus((DefectStatus)defect.Status),
                StatusChangedAt = defect.StatusChangedAt.HasValue?
                    defect.StatusChangedAt.Value.ToShortDateString() :
                    "N/A"
            };
        }

        static string ResolveStatus(DefectStatus status)
        {
            return status switch
            {
                DefectStatus.Opened => "Opened",
                DefectStatus.Fixing => "Fixing",
                DefectStatus.Eliminated => "Eliminated",
                _ => "Undefined"
            };
        }
    }
}

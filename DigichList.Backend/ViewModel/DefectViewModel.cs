using DigichList.Core.Entities;

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
        public int AssigneeId { get; set; }

        /// <summary>
        /// Current status for this defect.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The date and time the status was last changed.
        /// </summary>
        public string StatusChangedAt { get; set; }
    }
}

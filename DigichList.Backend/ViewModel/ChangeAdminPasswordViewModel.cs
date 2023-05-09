namespace DigichList.Backend.ViewModel
{
    /// <summary>
    /// The view model for reflecting password chang information.
    /// </summary>
    public class ChangeAdminPasswordViewModel
    {
        /// <summary>
        /// The specified admin identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The new password for this admin.
        /// </summary>
        public string Password { get; set; }
    }
}

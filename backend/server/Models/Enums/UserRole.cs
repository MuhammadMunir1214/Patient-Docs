namespace PatientDocs.Models.Enums
{
    /// <summary>
    /// Defines the different user roles in the PatientDocs system.
    /// This enum controls what actions each user can perform.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Administrator role: has full access to all features.
        /// Can manage users, patients, notes, and documents.
        /// </summary>
        Admin = 0,

        /// <summary>
        /// Doctor role: has limited access for patient care.
        /// Can view patients, create notes, and manage documents.
        /// Cannot access admin features or manage other users.
        /// </summary>
        Doctor = 1
    }
}

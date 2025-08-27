namespace PatientDocs.Models.Enums
{
    /// <summary>
    /// This class dfines the different types of documents that can be uploaded for patients.
    /// </summary>
    public enum DocumentType
    {
        MedicalHistory = 0,
        LabReport = 1,
        InsuranceCard = 2,
        GovernmentID = 3,
        CopayReceipt = 5,
        Other = 6
    }
}

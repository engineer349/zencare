namespace Zencareservice.Models
{
    public class Prescriptions
    {

        public int PrescriptionId { get; set; }

        public string ? DoctorName { get; set; }

        public string ? PatientFirstName { get; set; }

        public string? PatientLastName { get; set; }

        public string ? PatientAge { get; set; }

        public string ? PatientContactNo { get; set; }  

        public string ? Gender { get; set; }

        public string ? PatientDiagnosis { get; set; }

        public string ? PrescriptionDetail { get; set; }
    }
}

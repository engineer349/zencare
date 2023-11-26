namespace Zencareservice.Models
{
    public class Prescriptions
    {

        public int PrescriptionId { get; set; }

        public string ? DoctorName { get; set; }

        public string ? PatientName { get; set; }

        public string ? PatientAge { get; set; }

        public string ? PatientContactNo { get; set; }  

        public string ? Gender { get; set; }

        public string ? PatientDiagnosis { get; set; }

        public string ? PrescriptionDetail { get; set; }
    }
}

namespace Zencareservice.Models
{
    public class Appointments
    {   
        public int AptId {  get; set; }

        public string ? DoctorName { get; set; }

        public string ? PatientFirstName { get; set; }

        public string? PatientLastName { get; set; }

        public string? PatientAge { get; set; }

        public string ? PatientGender { get; set; }

        public string ? PatientContactNo { get; set; }

        public string ? PatientAddress1 { get; set; }

        public string? PatientAddress2 { get; set; }

        public string ? City { get; set; }

        public string ? State { get; set; }


        public string ? ReasonType { get; set; }

        public DateOnly ? AppointmentDate { get; set; }

        public TimeOnly ? AppointmentTime { get; set; }



    }
}

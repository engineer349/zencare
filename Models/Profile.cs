﻿namespace Zencareservice.Models
{
    public class Profile
    {

        public int PId { get; set; }

        public string Pcode { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public DateOnly Dob { get; set; }

        public string Phoneno { get; set; }

        public string AltPhoneno { get; set; }
        
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Zipcode { get; set; }

        public string Uniqueid { get; set; }

        public string Status { get; set; }

        public string Role { get; set; }
    }
}

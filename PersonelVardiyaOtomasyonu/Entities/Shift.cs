using System;

namespace PersonelVardiyaOtomasyonu.Entities
{
    public class Shift
    {
        public int Id { get; set; }
        public int RegistrantId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Hours { get; set; }
        public bool IsNew { get; set; }
    }
}

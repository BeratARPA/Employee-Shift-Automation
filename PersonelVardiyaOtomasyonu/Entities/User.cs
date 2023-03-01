namespace PersonelVardiyaOtomasyonu.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string IdentificationNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NameSurname { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
    }
}

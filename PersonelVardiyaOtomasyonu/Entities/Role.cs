namespace PersonelVardiyaOtomasyonu.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Print { get; set; }
        public bool UsersManagement { get; set; }
        public bool RolesManagement { get; set; }
        public bool SeeAllShifts { get; set; }
        public bool Status { get; set; }
    }
}

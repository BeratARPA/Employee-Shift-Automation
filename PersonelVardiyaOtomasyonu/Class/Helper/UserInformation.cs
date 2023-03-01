using PersonelVardiyaOtomasyonu.Entities;

namespace PersonelVardiyaOtomasyonu.Class.Helper
{
    //Burada global ve static değişkenlerimiz tanımlıyoruz. Nedeni ise programın her yerinden erişip gerekli bilgileri alabilmemizi sağlamak.
    public static class UserInformation
    {
        public static User User { get; set; }
        public static Role Role { get; set; }
    }
}

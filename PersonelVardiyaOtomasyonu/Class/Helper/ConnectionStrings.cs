using System.Data;
using System.Data.SqlClient;

namespace PersonelVardiyaOtomasyonu.Class.Helper
{
    //Bağlantı nesnemizi ayrı bir sınıfta tanımlıyoruz, nedeni ise tek bir adres ile her yerden erişim sağlamak.    
    public static class ConnectionStrings
    {
        public static SqlConnection _sqlConnection = new SqlConnection(@"Server=.\SQLEXPRESS; Initial Catalog=PersonelVardiyaOtomasyonu; Integrated Security=true; MultipleActiveResultSets=true");

        public static void ConnectionControl()
        {
            //Bağlantı açık değilse, bağlantıyı açar.
            if (_sqlConnection.State != ConnectionState.Open)
            {
                _sqlConnection.Open();
            }
        }
    }
}

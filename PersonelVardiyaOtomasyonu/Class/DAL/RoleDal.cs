using PersonelVardiyaOtomasyonu.Class.Helper;
using PersonelVardiyaOtomasyonu.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PersonelVardiyaOtomasyonu.Class.DAL
{
    //Oluşturduğumuz sınıf veri erişim sınıfıdır. Projede düzeni sağlamak için katman, katman çalışıyoruz ki karmaşıklık olmasın ve bir sonraki gelecek programcı zorlanmasın.
    public class RoleDal
    {
        //Buradaki method Roles tablosundaki bütün verileri oluşturduğumuz Role varlığına ekleyerek liste şeklinde geri döndürür.
        public List<Role> GetAll()
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Select * from Roles", ConnectionStrings._sqlConnection);

            //Belirttiğimiz sorguyu çalıştırarak DataReader nesnesinin içine atıyoruz. Nedeni ise verileri DataReader ile okumamız.
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //Birden çok veri gelebilir o yüzden Users listesi oluşturuyoruz.
            List<Role> roles = new List<Role>();

            //DataReader nesnesinin içinde veri olduğu sürece bu döngü çalışmaya devam edecek.
            while (sqlDataReader.Read())
            {
                //Role nesnesi oluşturup DataReaderdan gelen verileri oluşturduğumuz Role nesnesinin içine ekliyoruz.
                Role role = new Role
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    Name = sqlDataReader["Name"].ToString(),
                    Add = Convert.ToBoolean(sqlDataReader["Add"]),
                    Update = Convert.ToBoolean(sqlDataReader["Update"]),
                    Delete = Convert.ToBoolean(sqlDataReader["Delete"]),
                    Print = Convert.ToBoolean(sqlDataReader["Print"]),
                    UsersManagement = Convert.ToBoolean(sqlDataReader["UsersManagement"]),
                    RolesManagement = Convert.ToBoolean(sqlDataReader["RolesManagement"]),
                    SeeAllShifts = Convert.ToBoolean(sqlDataReader["SeeAllShifts"]),
                    Status = Convert.ToBoolean(sqlDataReader["Status"]),
                };

                //Yukarıda oluşturduğumuz Roles listesine Role nesnesini ekliyoruz.
                roles.Add(role);
            }

            //Gerekli nesneleri kapatıyoruz.
            sqlDataReader.Close();
            ConnectionStrings._sqlConnection.Close();

            //Shift listesini geri döndürüyoruz.
            return roles;
        }

        public Role GetById(int id)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Select * from Roles where Id=@id", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@id", id);

            //Belirttiğimiz sorguyu çalıştırarak DataReader nesnesinin içine atıyoruz. Nedeni ise verileri DataReader ile okumamız.
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //DataReader nesnesinin içinde veri olduğu sürece bu döngü çalışmaya devam edecek.
            while (sqlDataReader.Read())
            {
                //Role nesnesi oluşturup DataReaderdan gelen verileri oluşturduğumuz Role nesnesinin içine ekliyoruz.
                Role role = new Role
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    Name = sqlDataReader["Name"].ToString(),
                    Add = Convert.ToBoolean(sqlDataReader["Add"]),
                    Update = Convert.ToBoolean(sqlDataReader["Update"]),
                    Delete = Convert.ToBoolean(sqlDataReader["Delete"]),
                    Print = Convert.ToBoolean(sqlDataReader["Print"]),
                    UsersManagement = Convert.ToBoolean(sqlDataReader["UsersManagement"]),
                    RolesManagement = Convert.ToBoolean(sqlDataReader["RolesManagement"]),
                    SeeAllShifts = Convert.ToBoolean(sqlDataReader["SeeAllShifts"]),
                    Status = Convert.ToBoolean(sqlDataReader["Status"]),
                };

                //Gerekli nesneleri kapatıyoruz.
                sqlDataReader.Close();
                ConnectionStrings._sqlConnection.Close();

                //Role nesnesini geri döndürüyoruz.
                return role;
            }

            //Döngüye girmez ise geriye boş değer döndürüyoruz.
            return null;
        }

        public void Add(Role role)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Insert into Roles values(@name,@add,@update,@delete,@print,@usersManagement,@rolesManagement,@seeAllShifts,@status)", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@name", role.Name);
            sqlCommand.Parameters.AddWithValue("@add", role.Add);
            sqlCommand.Parameters.AddWithValue("@update", role.Update);
            sqlCommand.Parameters.AddWithValue("@delete", role.Delete);
            sqlCommand.Parameters.AddWithValue("@print", role.Print);
            sqlCommand.Parameters.AddWithValue("@usersManagement", role.UsersManagement);
            sqlCommand.Parameters.AddWithValue("@rolesManagement", role.RolesManagement);
            sqlCommand.Parameters.AddWithValue("@seeAllShifts", role.SeeAllShifts);
            sqlCommand.Parameters.AddWithValue("@status", role.Status);

            //Sorgumuzu çalıştırıyoruz.
            sqlCommand.ExecuteNonQuery();

            //Gerekli nesneleri kapatıyoruz.
            ConnectionStrings._sqlConnection.Close();
        }

        public void Update(Role role)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Update Roles set Name=@name,[Add]=@add,[Update]=@update,[Delete]=@delete,[Print]=@print,UsersManagement=@usersManagement,RolesManagement=@rolesManagement,SeeAllShifts=@seeAllShifts,Status=@status where Id=@id", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@id", role.Id);
            sqlCommand.Parameters.AddWithValue("@name", role.Name);
            sqlCommand.Parameters.AddWithValue("@add", role.Add);
            sqlCommand.Parameters.AddWithValue("@update", role.Update);
            sqlCommand.Parameters.AddWithValue("@delete", role.Delete);
            sqlCommand.Parameters.AddWithValue("@print", role.Print);
            sqlCommand.Parameters.AddWithValue("@usersManagement", role.UsersManagement);
            sqlCommand.Parameters.AddWithValue("@rolesManagement", role.RolesManagement);
            sqlCommand.Parameters.AddWithValue("@seeAllShifts", role.SeeAllShifts);
            sqlCommand.Parameters.AddWithValue("@status", role.Status);

            //Sorgumuzu çalıştırıyoruz.
            sqlCommand.ExecuteNonQuery();

            //Gerekli nesneleri kapatıyoruz.
            ConnectionStrings._sqlConnection.Close();
        }

        public void Delete(int id)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Delete from Roles where Id=@id", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@id", id);

            //Sorgumuzu çalıştırıyoruz.
            sqlCommand.ExecuteNonQuery();

            //Gerekli nesneleri kapatıyoruz.
            ConnectionStrings._sqlConnection.Close();
        }
    }
}

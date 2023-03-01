using PersonelVardiyaOtomasyonu.Class.Helper;
using PersonelVardiyaOtomasyonu.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PersonelVardiyaOtomasyonu.Class.DAL
{
    //Oluşturduğumuz sınıf veri erişim sınıfıdır. Projede düzeni sağlamak için katman, katman çalışıyoruz ki karmaşıklık olmasın ve bir sonraki gelecek programcı zorlanmasın.
    public class UserDal
    {
        //Buradaki method Users tablosundaki bütün verileri oluşturduğumuz User varlığına ekleyerek liste şeklinde geri döndürür.
        public List<User> GetAll()
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Select * from Users", ConnectionStrings._sqlConnection);

            //Belirttiğimiz sorguyu çalıştırarak DataReader nesnesinin içine atıyoruz. Nedeni ise verileri DataReader ile okumamız.
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //Birden çok veri gelebilir o yüzden Users listesi oluşturuyoruz.
            List<User> users = new List<User>();

            //DataReader nesnesinin içinde veri olduğu sürece bu döngü çalışmaya devam edecek.
            while (sqlDataReader.Read())
            {
                //User nesnesi oluşturup DataReaderdan gelen verileri oluşturduğumuz User nesnesinin içine ekliyoruz.
                User user = new User
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    RoleId = Convert.ToInt32(sqlDataReader["RoleId"]),
                    IdentificationNumber = sqlDataReader["IdentificationNumber"].ToString(),
                    Name = sqlDataReader["Name"].ToString(),
                    Surname = sqlDataReader["Surname"].ToString(),
                    NameSurname = sqlDataReader["Name"].ToString() + " " + sqlDataReader["Surname"].ToString(),
                    Password = sqlDataReader["Password"].ToString(),
                    Address = sqlDataReader["Address"].ToString(),
                    EMail = sqlDataReader["E-Mail"].ToString(),
                    PhoneNumber = sqlDataReader["PhoneNumber"].ToString(),
                    Status = Convert.ToBoolean(sqlDataReader["Status"]),
                };

                //Yukarıda oluşturduğumuz Users listesine User nesnesini ekliyoruz.
                users.Add(user);
            }

            //Gerekli nesneleri kapatıyoruz.
            sqlDataReader.Close();
            ConnectionStrings._sqlConnection.Close();

            //Users listesini geri döndürüyoruz.
            return users;
        }

        //Buradaki method Users tablosundaki bütün verileri oluşturduğumuz User varlığına ekleyerek liste şeklinde geri döndürür.
        public User GetByIdentificationNumber(string identificationNumber)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Select * from Users where IdentificationNumber=@identificationNumber", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@identificationNumber", identificationNumber);

            //Belirttiğimiz sorguyu çalıştırarak DataReader nesnesinin içine atıyoruz. Nedeni ise verileri DataReader ile okumamız.
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //DataReader nesnesinin içinde veri olduğu sürece bu döngü çalışmaya devam edecek.
            while (sqlDataReader.Read())
            {
                //User nesnesi oluşturup DataReaderdan gelen verileri oluşturduğumuz User nesnesinin içine ekliyoruz.
                User user = new User
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    RoleId = Convert.ToInt32(sqlDataReader["RoleId"]),
                    IdentificationNumber = sqlDataReader["IdentificationNumber"].ToString(),
                    Name = sqlDataReader["Name"].ToString(),
                    Surname = sqlDataReader["Surname"].ToString(),
                    NameSurname = sqlDataReader["Name"].ToString() + " " + sqlDataReader["Surname"].ToString(),
                    Password = sqlDataReader["Password"].ToString(),
                    Address = sqlDataReader["Address"].ToString(),
                    EMail = sqlDataReader["E-Mail"].ToString(),
                    PhoneNumber = sqlDataReader["PhoneNumber"].ToString(),
                    Status = Convert.ToBoolean(sqlDataReader["Status"]),
                };

                //Gerekli nesneleri kapatıyoruz.
                sqlDataReader.Close();
                ConnectionStrings._sqlConnection.Close();

                //User nesnesini geri döndürüyoruz.
                return user;
            }

            //Döngüye girmez ise geriye boş değer döndürüyoruz.
            return null;
        }

        public User GetById(int id)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Select * from Users where Id=@id", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@id", id);

            //Belirttiğimiz sorguyu çalıştırarak DataReader nesnesinin içine atıyoruz. Nedeni ise verileri DataReader ile okumamız.
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //DataReader nesnesinin içinde veri olduğu sürece bu döngü çalışmaya devam edecek.
            while (sqlDataReader.Read())
            {
                //User nesnesi oluşturup DataReaderdan gelen verileri oluşturduğumuz User nesnesinin içine ekliyoruz.
                User user = new User
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    RoleId = Convert.ToInt32(sqlDataReader["RoleId"]),
                    IdentificationNumber = sqlDataReader["IdentificationNumber"].ToString(),
                    Name = sqlDataReader["Name"].ToString(),
                    Surname = sqlDataReader["Surname"].ToString(),
                    NameSurname = sqlDataReader["Name"].ToString() + " " + sqlDataReader["Surname"].ToString(),
                    Password = sqlDataReader["Password"].ToString(),
                    Address = sqlDataReader["Address"].ToString(),
                    EMail = sqlDataReader["E-Mail"].ToString(),
                    PhoneNumber = sqlDataReader["PhoneNumber"].ToString(),
                    Status = Convert.ToBoolean(sqlDataReader["Status"]),
                };

                //Gerekli nesneleri kapatıyoruz.
                sqlDataReader.Close();
                ConnectionStrings._sqlConnection.Close();

                //User nesnesini geri döndürüyoruz.
                return user;
            }

            //Döngüye girmez ise geriye boş değer döndürüyoruz.
            return null;
        }

        public bool CheckByIdentificationNumber(string identificationNumber)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Select * from Users where IdentificationNumber=@identificationNumber", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@identificationNumber", identificationNumber);

            //Belirttiğimiz sorguyu çalıştırarak DataReader nesnesinin içine atıyoruz. Nedeni ise verileri DataReader ile okumamız.
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //DataReader nesnesinin içinde veri olduğu sürece bu döngü çalışmaya devam edecek.
            while (sqlDataReader.Read())
            {
                //Gerekli nesneleri kapatıyoruz.
                sqlDataReader.Close();
                ConnectionStrings._sqlConnection.Close();

                //Geriye true değerini döndürüyoruz.
                return true;
            }

            //Döngüye girmez ise geriye false değerini döndürüyoruz.
            return false;
        }

        public bool CheckByIdentificationNumberAndPassword(string identificationNumber, string password)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Select * from Users where IdentificationNumber=@identificationNumber and Password=@password", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@identificationNumber", identificationNumber);
            sqlCommand.Parameters.AddWithValue("@password", password);

            //Belirttiğimiz sorguyu çalıştırarak DataReader nesnesinin içine atıyoruz. Nedeni ise verileri DataReader ile okumamız.
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //DataReader nesnesinin içinde veri olduğu sürece bu döngü çalışmaya devam edecek.
            if (sqlDataReader.Read())
            {
                //Gerekli nesneleri kapatıyoruz.
                sqlDataReader.Close();
                ConnectionStrings._sqlConnection.Close();

                //Geriye true değerini döndürüyoruz.
                return true;
            }

            //Döngüye girmez ise geriye false değerini döndürüyoruz.
            return false;
        }

        public void Add(User user)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Insert into Users values(@roleId,@identificationNumber,@name,@surname,@password,@address,@email,@phoneNumber,@status)", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@roleId", user.RoleId);
            sqlCommand.Parameters.AddWithValue("@identificationNumber", user.IdentificationNumber);
            sqlCommand.Parameters.AddWithValue("@name", user.Name);
            sqlCommand.Parameters.AddWithValue("@surname", user.Surname);
            sqlCommand.Parameters.AddWithValue("@password", user.Password);
            sqlCommand.Parameters.AddWithValue("@address", user.Address);
            sqlCommand.Parameters.AddWithValue("@email", user.EMail);
            sqlCommand.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@status", user.Status);

            //Sorgumuzu çalıştırıyoruz.
            sqlCommand.ExecuteNonQuery();

            //Gerekli nesneleri kapatıyoruz.
            ConnectionStrings._sqlConnection.Close();
        }

        public void Update(User user)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Update Users set RoleId=@roleId,IdentificationNumber=@identificationNumber,Name=@name,Surname=@surname,Password=@password,Address=@address,[E-Mail]=@email,PhoneNumber=@phoneNumber,Status=@status where Id=@id", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@id", user.Id);
            sqlCommand.Parameters.AddWithValue("@roleId", user.RoleId);
            sqlCommand.Parameters.AddWithValue("@identificationNumber", user.IdentificationNumber);
            sqlCommand.Parameters.AddWithValue("@name", user.Name);
            sqlCommand.Parameters.AddWithValue("@surname", user.Surname);
            sqlCommand.Parameters.AddWithValue("@password", user.Password);
            sqlCommand.Parameters.AddWithValue("@address", user.Address);
            sqlCommand.Parameters.AddWithValue("@email", user.EMail);
            sqlCommand.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@status", user.Status);

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
            SqlCommand sqlCommand = new SqlCommand("Delete from Users where Id=@id", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@id", id);

            //Sorgumuzu çalıştırıyoruz.
            sqlCommand.ExecuteNonQuery();

            //Gerekli nesneleri kapatıyoruz.
            ConnectionStrings._sqlConnection.Close();
        }
    }
}

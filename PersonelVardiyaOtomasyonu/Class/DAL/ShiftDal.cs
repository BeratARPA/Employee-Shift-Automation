using PersonelVardiyaOtomasyonu.Class.Helper;
using PersonelVardiyaOtomasyonu.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PersonelVardiyaOtomasyonu.Class.DAL
{
    //Oluşturduğumuz sınıf veri erişim sınıfıdır. Projede düzeni sağlamak için katman, katman çalışıyoruz ki karmaşıklık olmasın ve bir sonraki gelecek programcı zorlanmasın.
    public class ShiftDal
    {
        //Buradaki method Shifts tablosundaki bütün verileri belirlediğimiz şartlara göre oluşturduğumuz Shift varlığına ekleyerek liste şeklinde geri döndürür.
        public List<Shift> GetAllWithDate(DateTime startDate, DateTime endDate)
        {
            DateTime startDateTime = startDate;
            DateTime endDateTime = endDate;
            endDateTime = endDateTime.AddDays(1);

            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Select * from Shifts where Date>=@startDate and Date<=@endDate", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@startDate", startDateTime.Date);
            sqlCommand.Parameters.AddWithValue("@endDate", endDateTime.Date);

            //Belirttiğimiz sorguyu çalıştırarak DataReader nesnesinin içine atıyoruz. Nedeni ise verileri DataReader ile okumamız.
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //Birden çok veri gelebilir o yüzden Users listesi oluşturuyoruz.
            List<Shift> shifts = new List<Shift>();

            //DataReader nesnesinin içinde veri olduğu sürece bu döngü çalışmaya devam edecek.
            while (sqlDataReader.Read())
            {
                //Shift nesnesi oluşturup DataReaderdan gelen verileri oluşturduğumuz Shift nesnesinin içine ekliyoruz.
                Shift shift = new Shift
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    RegistrantId = Convert.ToInt32(sqlDataReader["RegistrantId"]),
                    EmployeeId = Convert.ToInt32(sqlDataReader["EmployeeId"]),
                    DateOfRegistration = Convert.ToDateTime(sqlDataReader["DateOfRegistration"]),
                    Date = Convert.ToDateTime(sqlDataReader["Date"]),
                    Location = sqlDataReader["Location"].ToString(),
                    Hours = sqlDataReader["Hours"].ToString(),
                    IsNew = Convert.ToBoolean(sqlDataReader["IsNew"]),
                };

                //Yukarıda oluşturduğumuz Shifts listesine Shift nesnesini ekliyoruz.
                shifts.Add(shift);
            }

            //Gerekli nesneleri kapatıyoruz.
            sqlDataReader.Close();
            ConnectionStrings._sqlConnection.Close();

            //Shift listesini geri döndürüyoruz.
            return shifts;
        }

        //Buradaki method Shifts tablosundaki bütün verileri belirlediğimiz şartlara göre oluşturduğumuz Shift varlığına ekleyerek liste şeklinde geri döndürür.
        public List<Shift> GetAllWithDateAndEmployee(DateTime startDate, DateTime endDate, int employeeId)
        {
            DateTime startDateTime = startDate;
            DateTime endDateTime = endDate;
            endDateTime = endDateTime.AddDays(1);

            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Select * from Shifts where Date>=@startDate and Date<=@endDate and EmployeeId=@employeeId", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@startDate", startDateTime.Date);
            sqlCommand.Parameters.AddWithValue("@endDate", endDateTime.Date);
            sqlCommand.Parameters.AddWithValue("@employeeId", employeeId);

            //Belirttiğimiz sorguyu çalıştırarak DataReader nesnesinin içine atıyoruz. Nedeni ise verileri DataReader ile okumamız.
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //Birden çok veri gelebilir o yüzden Users listesi oluşturuyoruz.
            List<Shift> shifts = new List<Shift>();

            //DataReader nesnesinin içinde veri olduğu sürece bu döngü çalışmaya devam edecek.
            while (sqlDataReader.Read())
            {
                //Shift nesnesi oluşturup DataReaderdan gelen verileri oluşturduğumuz Shift nesnesinin içine ekliyoruz.
                Shift shift = new Shift
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    RegistrantId = Convert.ToInt32(sqlDataReader["RegistrantId"]),
                    EmployeeId = Convert.ToInt32(sqlDataReader["EmployeeId"]),
                    DateOfRegistration = Convert.ToDateTime(sqlDataReader["DateOfRegistration"]),
                    Date = Convert.ToDateTime(sqlDataReader["Date"]),
                    Location = sqlDataReader["Location"].ToString(),
                    Hours = sqlDataReader["Hours"].ToString(),
                    IsNew = Convert.ToBoolean(sqlDataReader["IsNew"]),
                };

                //Yukarıda oluşturduğumuz Shifts listesine Shift nesnesini ekliyoruz.
                shifts.Add(shift);
            }

            //Gerekli nesneleri kapatıyoruz.
            sqlDataReader.Close();
            ConnectionStrings._sqlConnection.Close();

            //Shift listesini geri döndürüyoruz.
            return shifts;
        }

        public bool CheckByNewShiftWithEmployeeId(int employeeId)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Select * from Shifts where EmployeeId=@employeeId and IsNew=1", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@employeeId", employeeId);

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

        public Shift GetById(int id)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Select * from Shifts where Id=@id", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@id", id);

            //Belirttiğimiz sorguyu çalıştırarak DataReader nesnesinin içine atıyoruz. Nedeni ise verileri DataReader ile okumamız.
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //DataReader nesnesinin içinde veri olduğu sürece bu döngü çalışmaya devam edecek.
            while (sqlDataReader.Read())
            {
                //Shift nesnesi oluşturup DataReaderdan gelen verileri oluşturduğumuz Shift nesnesinin içine ekliyoruz.
                Shift shift = new Shift
                {
                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                    RegistrantId = Convert.ToInt32(sqlDataReader["RegistrantId"]),
                    EmployeeId = Convert.ToInt32(sqlDataReader["EmployeeId"]),
                    DateOfRegistration = Convert.ToDateTime(sqlDataReader["DateOfRegistration"]),
                    Date = Convert.ToDateTime(sqlDataReader["Date"]),
                    Location = sqlDataReader["Location"].ToString(),
                    Hours = sqlDataReader["Hours"].ToString(),
                    IsNew = Convert.ToBoolean(sqlDataReader["IsNew"]),
                };

                //Gerekli nesneleri kapatıyoruz.
                sqlDataReader.Close();
                ConnectionStrings._sqlConnection.Close();

                //Shift nesnesini geri döndürüyoruz.
                return shift;
            }

            //Döngüye girmez ise geriye boş değer döndürüyoruz.
            return null;
        }

        public void DeactivateAllNewShiftsTheEmployee(int employeeId)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Update Shifts set IsNew=0 where employeeId=@employeeId", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@employeeId", employeeId);

            //Sorgumuzu çalıştırıyoruz.
            sqlCommand.ExecuteNonQuery();

            //Gerekli nesneleri kapatıyoruz.
            ConnectionStrings._sqlConnection.Close();
        }

        public void Add(Shift shift)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Insert into Shifts values(@registrantId,@employeeId,@dateOfRegistration,@date,@location,@hours,@isNew)", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@registrantId", shift.RegistrantId);
            sqlCommand.Parameters.AddWithValue("@employeeId", shift.EmployeeId);
            sqlCommand.Parameters.AddWithValue("@dateOfRegistration", shift.DateOfRegistration);
            sqlCommand.Parameters.AddWithValue("@date", shift.Date);
            sqlCommand.Parameters.AddWithValue("@location", shift.Location);
            sqlCommand.Parameters.AddWithValue("@hours", shift.Hours);
            sqlCommand.Parameters.AddWithValue("@isNew", shift.IsNew);

            //Sorgumuzu çalıştırıyoruz.
            sqlCommand.ExecuteNonQuery();

            //Gerekli nesneleri kapatıyoruz.
            ConnectionStrings._sqlConnection.Close();
        }

        public void Update(Shift shift)
        {
            //Bağlantı kontrolunu sağlıyoruz. Açık mı? Kapalı mı? Açık ise bağlantıyı açacak.
            ConnectionStrings.ConnectionControl();

            //Sorgumuzu belirtiyoruz
            SqlCommand sqlCommand = new SqlCommand("Update Shifts set RegistrantId=@registrantId,EmployeeId=@employeeId,DateOfRegistration=@dateOfRegistration,Date=@date,Location=@location,Hours=@hours,IsNew=@isNew where Id=@id", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@id", shift.Id);
            sqlCommand.Parameters.AddWithValue("@registrantId", shift.RegistrantId);
            sqlCommand.Parameters.AddWithValue("@employeeId", shift.EmployeeId);
            sqlCommand.Parameters.AddWithValue("@dateOfRegistration", shift.DateOfRegistration);
            sqlCommand.Parameters.AddWithValue("@date", shift.Date);
            sqlCommand.Parameters.AddWithValue("@location", shift.Location);
            sqlCommand.Parameters.AddWithValue("@hours", shift.Hours);
            sqlCommand.Parameters.AddWithValue("@isNew", shift.IsNew);

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
            SqlCommand sqlCommand = new SqlCommand("Delete from Shifts where Id=@id", ConnectionStrings._sqlConnection);

            //Sorgumuza parametre ekliyoruz.
            sqlCommand.Parameters.AddWithValue("@id", id);

            //Sorgumuzu çalıştırıyoruz.
            sqlCommand.ExecuteNonQuery();

            //Gerekli nesneleri kapatıyoruz.
            ConnectionStrings._sqlConnection.Close();
        }
    }
}

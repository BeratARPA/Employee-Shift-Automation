using PersonelVardiyaOtomasyonu.Class.DAL;
using PersonelVardiyaOtomasyonu.Entities;
using System;
using System.Windows.Forms;

namespace PersonelVardiyaOtomasyonu
{
    public partial class ShiftUpdate : Form
    {
        public ShiftUpdate(int id)
        {
            InitializeComponent();

            _id = id;
        }

        ShiftDal _shiftDal = new ShiftDal();
        UserDal _userDal = new UserDal();
        int _id;

        private void ShiftUpdate_Load(object sender, EventArgs e)
        {
            GetUserList();
            GetList();
        }

        //Bütün Kullanıcılar listeliyor.
        public void GetUserList()
        {
            cb_users.DisplayMember = "NameSurname";
            cb_users.ValueMember = "Id";
            cb_users.DataSource = _userDal.GetAll();
        }

        //Gelen Id'ye göre gelen vardiyayı controllere ekliyor.
        public void GetList()
        {
            //Id'ye göre vardiyayı getiriyor.
            var shift = _shiftDal.GetById(_id);
            if (shift != null)
            {
                dtp_date.Value = shift.Date;
                cb_locations.Text = shift.Location;
                cb_hours.Text = shift.Hours;
                cb_users.SelectedValue = shift.EmployeeId;
            }
        }

        private void cb_locations_TextChanged(object sender, EventArgs e)
        {
            //ComboBoxdaki verileri temizliyor.
            cb_hours.Items.Clear();

            //ComboBoxdaki seçilen veriyi kontrol ediyor
            switch (cb_locations.Text)
            {
                //Seçilen veri bu ise buraya giriyor.
                case "Kampüs Girisi":
                    //Gerekli verileri ComboBoxa ekliyor.
                    cb_hours.Items.Add("00:00 - 08:00");
                    cb_hours.Items.Add("08:00 - 16:00");
                    cb_hours.Items.Add("16:00 - 24:00");
                    break;

                //Seçilen veri bu ise buraya giriyor.
                case "Kampüs Içi":
                    //Gerekli verileri ComboBoxa ekliyor.
                    cb_hours.Items.Add("08:00 - 16:00");
                    cb_hours.Items.Add("09:00 - 17:00");
                    break;

                //Hiçbiri değilse buraya giriyor.
                default:
                    //Gerekli verileri ComboBoxa ekliyor.
                    cb_hours.Items.Add("Saat bulunmamaktadır!");
                    break;
            }
        }

        private void btn_continue_Click(object sender, EventArgs e)
        {
            //Textbox boş değilse giriyor.
            if (cb_locations.Text != "" && cb_hours.Text != "" && cb_users.Text != "")
            {
                //Id'ye göre vardiya getiriliyor.
                var value = _shiftDal.GetById(_id);

                //User nesnesi oluşturuyoruz.
                Shift shift = new Shift
                {
                    Id = _id,
                    RegistrantId = value.RegistrantId,
                    EmployeeId = Convert.ToInt32(cb_users.SelectedValue),
                    DateOfRegistration = value.DateOfRegistration,
                    Date = Convert.ToDateTime(dtp_date.Value.ToShortDateString()),
                    Location = cb_locations.Text,
                    Hours = cb_hours.Text,
                    IsNew = value.IsNew,
                };

                //Mevcut vardiya güncelleniyor
                _shiftDal.Update(shift);

                //Main formu var mı? Kontrol ediyor.
                Main main = (Main)Application.OpenForms["Main"];

                //Main nesnesi boş değilse, Main formundaki GetList metotunu çalıştırıyor.
                if (main != null)
                {
                    main.GetList();
                }

                //Mevcut formu kapatıyoruz.
                this.Close();
            }
            else
            {
                MessageBox.Show("Gerekli alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

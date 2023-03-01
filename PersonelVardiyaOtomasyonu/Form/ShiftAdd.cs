using PersonelVardiyaOtomasyonu.Class.DAL;
using PersonelVardiyaOtomasyonu.Class.Helper;
using PersonelVardiyaOtomasyonu.Entities;
using System;
using System.Windows.Forms;

namespace PersonelVardiyaOtomasyonu
{
    public partial class ShiftAdd : Form
    {
        public ShiftAdd()
        {
            InitializeComponent();
        }

        ShiftDal _shiftDal = new ShiftDal();
        UserDal _userDal = new UserDal();

        private void ShiftAdd_Load(object sender, EventArgs e)
        {
            GetUserList();
        }

        //Bütün Kullanıcılar listeliyor.
        public void GetUserList()
        {
            cb_users.DisplayMember = "NameSurname";
            cb_users.ValueMember = "Id";
            cb_users.DataSource = _userDal.GetAll();
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
                //User nesnesi oluşturuyoruz.
                Shift shift = new Shift
                {
                    RegistrantId = UserInformation.User.Id,
                    EmployeeId = Convert.ToInt32(cb_users.SelectedValue),
                    DateOfRegistration = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                    Date = Convert.ToDateTime(dtp_date.Value.ToShortDateString()),
                    Location = cb_locations.Text,
                    Hours = cb_hours.Text,
                    IsNew = true,
                };

                //Vardiya girilmek istenen kullanıcının aktif vardiyası var ise buraya giriyor.
                if (_shiftDal.CheckByNewShiftWithEmployeeId(Convert.ToInt32(cb_users.SelectedValue)))
                {
                    if (MessageBox.Show("Seçili personelin aktif bir vardiyası bulunmaktadır.\n\nAktif vardiyasını pasif hale getirip!\n\nYeni vardiya eklensin mi?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        //Tüm aktif vardiyaları pasi hale getiriyor.
                        _shiftDal.DeactivateAllNewShiftsTheEmployee(Convert.ToInt32(cb_users.SelectedValue));

                        //Yeni vardiya ekleniyor.
                        _shiftDal.Add(shift);

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
                }
                else
                {
                    //Yeni vardiya ekleniyor.
                    _shiftDal.Add(shift);

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
            }
            else
            {
                MessageBox.Show("Gerekli alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}

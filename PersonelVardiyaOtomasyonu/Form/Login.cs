using PersonelVardiyaOtomasyonu.Class.DAL;
using PersonelVardiyaOtomasyonu.Class.Helper;
using System;
using System.Windows.Forms;

namespace PersonelVardiyaOtomasyonu
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        UserDal _userDal = new UserDal();
        RoleDal _roleDal = new RoleDal();

        private void txt_IdentificationNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Harf girişini engelliyor.
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            //Kimlik ve parola boş değilse giriyor buraya.
            if (txt_IdentificationNumber.Text != "" && txt_password.Text != "")
            {
                //Girilen kimlik ve parolaya ait kullanıcı var mı kontrol ediyor.
                if (_userDal.CheckByIdentificationNumberAndPassword(txt_IdentificationNumber.Text, txt_password.Text))
                {
                    //Programda giriş yapan kullanıcının bilgierini oluşturduğumuz global değişkenlerimize atıyoruz.
                    UserInformation.User = _userDal.GetByIdentificationNumber(txt_IdentificationNumber.Text);
                    UserInformation.Role = _roleDal.GetById(UserInformation.User.RoleId);

                    //Ana formu açıyor.
                    Main main = new Main();
                    main.Show();

                    //Mevcut formu gizliyor.
                    this.Hide();

                    //Textboxları temizliyor.
                    txt_IdentificationNumber.Clear();
                    txt_password.Clear();
                }
                else
                {
                    MessageBox.Show("Kimlik Numarasını veya Parolayı doğru girdiğinizden emin olunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Gerekli alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

using PersonelVardiyaOtomasyonu.Class.DAL;
using PersonelVardiyaOtomasyonu.Class.Helper;
using PersonelVardiyaOtomasyonu.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PersonelVardiyaOtomasyonu
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        ShiftDal _shiftDal = new ShiftDal();
        UserDal _userDal = new UserDal();

        private void Main_Load(object sender, EventArgs e)
        {
            CheckUserRole();
            GetList();
        }

        //Bütün Vardiyaları listeliyor.
        public void GetList()
        {
            //DataGridViewdeki satırları temizliyor.
            dgv_shifts.Rows.Clear();

            //Shifts listesi oluşturuyoruz ve yetkisi var mı kontrol edip ona göre verileri çekiyoruz.
            List<Shift> shifts;
            if (UserInformation.Role.SeeAllShifts)
            {
                //Role yetkisi var ise burası çalışıyor.
                shifts = _shiftDal.GetAllWithDate(dtp_startDate.Value, dtp_endDate.Value);
            }
            else
            {
                //Role yetkisi yok ise burası çalışıyor.
                shifts = _shiftDal.GetAllWithDateAndEmployee(dtp_startDate.Value, dtp_endDate.Value, UserInformation.User.Id);
            }

            //Gelen verileri döngüye sokuyoruz ki hepsine tek, tek erişebilelim.
            foreach (Shift shift in shifts)
            {
                //Gelen vardiyaya göre gerekli verileri çekiyorum.
                var registrantUser = _userDal.GetById(shift.RegistrantId);
                var user = _userDal.GetById(shift.EmployeeId);

                //IsNew true'ya eşitse Aktif Vardiya değilse Pasif Vardiya olarak durum değişkenine atıyoruz.
                string durum = shift.IsNew ? "Aktif Vardiya" : "Pasif Vardiya";

                //Gerekli sütünlara gerekli verileri ekleniyor.
                dgv_shifts.Rows.Add(shift.Id, shift.RegistrantId, registrantUser.NameSurname, shift.EmployeeId, user.NameSurname, shift.DateOfRegistration.ToShortDateString(), shift.Date.ToShortDateString(), shift.Location, shift.Hours, shift.IsNew, durum);
            }

            //Seçili satırı seçimden kaldırıyor.
            dgv_shifts.ClearSelection();
        }

        private void dtp_endDate_ValueChanged(object sender, EventArgs e)
        {
            //Tarih seçimi yapıldığında burası çalışacak.
            GetList();
        }

        //Rolleri kontrol ediyor.
        public void CheckUserRole()
        {
            //FlowLayoutPaneldeki control sayısını alıp collapse değişkenine atıyoruz.
            int collapse = flp_buttons.Controls.Count;

            //Global oluşturduğumuz değişkenlerdeki bilgileri atıyoruz.
            lbl_nameSurname.Text = UserInformation.User.Name + " " + UserInformation.User.Surname.ToUpper();
            lbl_roleName.Text = "(" + UserInformation.Role.Name + ")";

            //Rol pasifse buraya giriyor.
            if (!UserInformation.Role.Add)
            {
                //Buttonu kapatıyor ve collapse değerini bir adet düşürüyor.
                btn_add.Visible = false;
                collapse--;
            }

            //Rol pasifse buraya giriyor.
            if (!UserInformation.Role.Update)
            {
                //Buttonu kapatıyor ve collapse değerini bir adet düşürüyor.
                btn_update.Visible = false;
                collapse--;
            }

            //Rol pasifse buraya giriyor.
            if (!UserInformation.Role.Delete)
            {
                //Buttonu kapatıyor ve collapse değerini bir adet düşürüyor.
                btn_delete.Visible = false;
                collapse--;
            }

            //Rol pasifse buraya giriyor.
            if (!UserInformation.Role.Print)
            {
                //Buttonu kapatıyor ve collapse değerini bir adet düşürüyor.
                btn_print.Visible = false;
                collapse--;
            }

            //Rol pasifse buraya giriyor.
            if (!UserInformation.Role.UsersManagement)
            {
                //Buttonu kapatıyor ve collapse değerini bir adet düşürüyor.
                btn_users.Visible = false;
                collapse--;
            }

            //Rol pasifse buraya giriyor.
            if (!UserInformation.Role.RolesManagement)
            {
                //Buttonu kapatıyor ve collapse değerini bir adet düşürüyor.
                btn_roles.Visible = false;
                collapse--;
            }

            //Eğer Collapse değeri sıfıra eşitse paneli gizliyor.
            if (collapse == 0)
            {
                splitContainer1.Panel1Collapsed = true;
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            //Login formu var mı? Kontrol ediyor.
            Login loginCheck = (Login)Application.OpenForms["Login"];

            //LoginCheck nesnesi boş değilse giriyor, login formu arkaplanda açık demektir. Boş ise yeniden Login nesnesi oluşturup formu açıyor.
            if (loginCheck != null)
            {
                //Login formunu açıyor.
                loginCheck.Show();
            }
            else
            {   
                //Login formunu açıyor.
                Login login = new Login();
                login.Show();
            }

            //Mevcut formu kapatıyoruz.
            this.Close();
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {            
            splitContainer1.SplitterDistance = 160;
        }

        #region LeftMenu
        private void btn_roles_Click(object sender, EventArgs e)
        {
            //Roles formunu açıyor.
            Roles roles = new Roles();
            roles.ShowDialog();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            //ShiftAdd formunu açıyor.
            ShiftAdd shiftAdd = new ShiftAdd();
            shiftAdd.ShowDialog();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            //DataGridViewde seçili satır varsa giriyor.
            if (dgv_shifts.SelectedRows.Count != 0)
            {
                //ShiftUpdate formuna seçilen satırın Id bilgini göndererek açıyor.
                ShiftUpdate shiftUpdate = new ShiftUpdate(Convert.ToInt32(dgv_shifts.CurrentRow.Cells[0].Value));
                shiftUpdate.ShowDialog();
            }
            else
            {
                MessageBox.Show("Güncellenecek vardiyayı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_users_Click(object sender, EventArgs e)
        {
            //Users formunu açıyor.
            Users users = new Users();
            users.ShowDialog();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            //DataGridViewde seçili satır varsa giriyor.
            if (dgv_shifts.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Vardiyayı silmek istediğinizden emin misiniz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //Seçili satırın ID bilgisini göndererek veriyi siliyor.
                    _shiftDal.Delete(Convert.ToInt32(dgv_shifts.CurrentRow.Cells[0].Value));

                    //Tekrar listeliyor.
                    GetList();
                }
            }
            else
            {
                MessageBox.Show("Silinecek vardiyayı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void btn_print_Click(object sender, EventArgs e)
        {
            //DataTable nesnesi oluşturuyoruz.
            DataTable dataTable = new DataTable();

            //Sütün ekliyoruz.
            dataTable.Columns.Add("Kayıt Eden Personel");
            dataTable.Columns.Add("Personel");
            dataTable.Columns.Add("Kayıt Tarihi");
            dataTable.Columns.Add("Vardiya Tarihi");
            dataTable.Columns.Add("Lokasyon");
            dataTable.Columns.Add("Saatler");
            dataTable.Columns.Add("Durum");

            //DataGridViewdeki verileri döngüye sokuyoruz.
            for (int i = 0; i < dgv_shifts.Rows.Count; i++)
            {
                //DataRow nesnesi oluşturuyor ve yeni bir row ekliyoruz.
                DataRow dataRow = dataTable.NewRow();

                //İlgili sütüna veri ekliyoruz.
                dataRow["Kayıt Eden Personel"] = dgv_shifts.Rows[i].Cells[2].Value;
                dataRow["Personel"] = dgv_shifts.Rows[i].Cells[4].Value;
                dataRow["Kayıt Tarihi"] = dgv_shifts.Rows[i].Cells[5].Value;
                dataRow["Vardiya Tarihi"] = dgv_shifts.Rows[i].Cells[6].Value;
                dataRow["Lokasyon"] = dgv_shifts.Rows[i].Cells[7].Value;
                dataRow["Saatler"] = dgv_shifts.Rows[i].Cells[8].Value;
                dataRow["Durum"] = dgv_shifts.Rows[i].Cells[10].Value;

                //Oluşturduğumuz satırı DataTable nesnesine ekliyoruz.
                dataTable.Rows.Add(dataRow);
            }

            //DataTable ve tarih bilgilerini metota gönderip yazdırma işlemlerini yaptırıyoruzç
            PrintPDFBuilder.Print(dataTable, dtp_startDate.Value, dtp_endDate.Value);
        }
    }
}

using Restaurant.BLL;
using Restaurant.DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurant.Helper;
namespace Restaurant
{
    public partial class FrmAdmin : Form
    {
        public FrmAdmin(UserModel um)
        {
            InitializeComponent();
            this.um = um;
        }
        UserModel um;
        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            SayWelcome(um);
            tmrSaat.Enabled = true;
            ListUsers(UserBLL.GetUsers());
            ListEmployees(EmployeeBLL.GetEmployees());
            AddEmployeesToUser(EmployeeBLL.GetEmployees());
            AddCategoriesToFood();
            ListFoods(FoodBLL.GetFoods());
            PaintTables(TableBLL.GetTables());
            MasalariDoldur(TableBLL.GetTables());
            PozisyonlarıDoldur();
            SiparisListele(OrderBLL.GetOrders());
        }
        public void SiparisListele(List<OrderModel>oml)
        {
            dgvSatislar.Rows.Clear();
            int adet=0;
            decimal karZarar=0;
            foreach (var item in oml)
            {
                dgvSatislar.Rows.Add(item.SiparisID, item.MasaID, item.calisan.CalisanAdi, item.AlınmaZamani, $"{item.Tutari} ₺");
                var yemekleri = FoodNavigateBLL.GetFoodNavigates(item.SiparisID);
                adet += yemekleri.Count();
               
                foreach (var yemek in yemekleri)
                {

                    karZarar += yemek.yemek.YemekFiyati - yemek.yemek.YemekMaliyeti;
                }
            }
            lblSatilanAdet.Text = adet.ToString();
            lblKarZarar.Text = $"{karZarar.ToString()} ₺";


        }
        public void PozisyonlarıDoldur()
        {
            cmbPozisyon.DataSource = Enum.GetValues(typeof(Common.calisanPozisyonu));
            
        }
        private void tpSiparis_Click(object sender, EventArgs e)
        {
           
        }
        //TABLE
        public void PaintTables(List<TableModel>tml)
        {
            foreach (var item in tml)
            {
                switch (item.MasaID)
                {
                    case 1:
                        if (item.MasaDurumu==(int)Common.masaDurumu.Boş)
                        {
                            btn1.BackColor = Color.Green;
                        }
                        else
                        {
                            btn1.BackColor = Color.Red;
                        }
                        break;
                    case 2:
                        if (item.MasaDurumu == (int)Common.masaDurumu.Boş)
                        {
                            btn2.BackColor = Color.Green;
                        }
                        else
                        {
                            btn2.BackColor = Color.Red;
                        }
                        break;
                    case 3:
                        if (item.MasaDurumu == (int)Common.masaDurumu.Boş)
                        {
                            btn3.BackColor = Color.Green;
                        }
                        else
                        {
                            btn3.BackColor = Color.Red;
                        }
                        break;
                    case 4:
                        if (item.MasaDurumu == (int)Common.masaDurumu.Boş)
                        {
                            btn4.BackColor = Color.Green;
                        }
                        else
                        {
                            btn4.BackColor = Color.Red;
                        }
                        break;
                    case 5:
                        if (item.MasaDurumu == (int)Common.masaDurumu.Boş)
                        {
                            btn5.BackColor = Color.Green;
                        }
                        else
                        {
                            btn5.BackColor = Color.Red;
                        }
                        break;
                    case 6:
                        if (item.MasaDurumu == (int)Common.masaDurumu.Boş)
                        {
                            btn6.BackColor = Color.Green;
                        }
                        else
                        {
                            btn6.BackColor = Color.Red;
                        }
                        break;
                    case 7:
                        if (item.MasaDurumu == (int)Common.masaDurumu.Boş)
                        {
                            btn7.BackColor = Color.Green;
                        }
                        else
                        {
                            btn7.BackColor = Color.Red;
                        }
                        break;
                    case 8:
                        if (item.MasaDurumu == (int)Common.masaDurumu.Boş)
                        {
                            btn8.BackColor = Color.Green;
                        }
                        else
                        {
                            btn8.BackColor = Color.Red;
                        }
                        break;
                    case 9:
                        if (item.MasaDurumu == (int)Common.masaDurumu.Boş)
                        {
                            btn9.BackColor = Color.Green;
                        }
                        else
                        {
                            btn9.BackColor = Color.Red;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        //USER
        public void SayWelcome(UserModel um)
        {
            lblKarsila.Text = $"Hoşgeldiniz,{um.Calisan.CalisanAdi}";
        }

        private void tmrSaat_Tick(object sender, EventArgs e)
        {
            lblSaat.Text = DateTime.Now.ToShortTimeString();
        }

        private void FrmAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            UserBLL.SetUserOffline(um);
            FrmGiris fg = new FrmGiris();
            fg.Show();

        }
        //USER
        public void ListUsers(List<UserModel> uml)
        {
            dgvKullanici.Rows.Clear();
            foreach (var item in uml)
            {
                dgvKullanici.Rows.Add(item.KullaniciID, item.KullaniciAdi, item.Sifre, item.Calisan.CalisanAdi, item.OnlineMİ);
            }
        }
        //EMPLOYEE
        public void ListEmployees(List<EmployeeModel> eml)
        {
            dgvEmployee.Rows.Clear();
            foreach (var item in eml)
            {//Enum ile pozisyon ismini çek!!!
                dgvEmployee.Rows.Add(item.CalisanID, item.CalisanAdi, item.CalisanSoyadi, item.Maasi, item.BaslangicTarihi,PozisyonAl( item.Pozisyonu));
            }
        }
        public string PozisyonAl(int ID)
        {
            if (ID==(int)Common.calisanPozisyonu.admin)
            {
                return Common.calisanPozisyonu.admin.ToString();
            }
            else
            {
                return Common.calisanPozisyonu.garson.ToString();
            }
        }
        public void AddEmployeesToUser(List<EmployeeModel> eml)
        {
         
            cmbCalisan.DataSource = eml;
            cmbCalisan.DisplayMember = "CalisanAdi";
            cmbCalisan.ValueMember = "CalisanID";
        }
        //FOOD
        public void AddCategoriesToFood()
        {
         
            cmbKategori.DataSource = Enum.GetValues(typeof(Common.foodCategory));
            cmbKategori.DisplayMember = "Description";

        }
        public void ListFoods(List<FoodModel> fml)
        {
            dgvYemek.Rows.Clear();
            foreach (var item in fml)
            {
                dgvYemek.Rows.Add(item.YemekID, item.YemekAdi, item.YemekMaliyeti, item.YemekFiyati,WriteCategoryFromEnum(item.YemekKategorisi) , item.YemekMevcutAdet);
            }
        }
        public string WriteCategoryFromEnum(int ID)
        {
            string kategori="";
            switch (ID)
            {
                case 1:
                    kategori= Common.foodCategory.Çorba.ToString();
                    break;
                case 2:
                    kategori=Common.foodCategory.Salata.ToString();
                    break;
                case 3:
                    kategori= Common.foodCategory.AraSıcak.ToString();
                    break;
                case 4:
                    kategori= Common.foodCategory.AnaYemek.ToString();
                    break;
                case 5:
                    kategori= Common.foodCategory.Mezeler.ToString();
                    break;
                case 6:
                    kategori= Common.foodCategory.Tatlılar.ToString();
                    break;
                case 7:
                    kategori= Common.foodCategory.İçecekler.ToString();
                    break;
                default:
                    break;
            }
            return kategori;
        }
        //Employee ADD
        private void button3_Click(object sender, EventArgs e)
        {

            string mesaj = EmployeeBLL.AddEmployee(txtEmployeeName.Text, txtEmployeeSirname.Text, txtEmployeeSalary.Text, (int)cmbPozisyon.SelectedItem);
            MessageBox.Show(mesaj);
            ListEmployees(EmployeeBLL.GetEmployees());
            EmployeeTemizle();
        }
        public void EmployeeTemizle()
        {
            txtEmployeeName.Text = "";
            txtEmployeeSirname.Text = "";
            txtEmployeeSalary.Text = "";
        }
        //Employee Update
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int duzenlenecek = (int)dgvEmployee.CurrentRow.Cells["calisanID"].Value;
                var mesaj = EmployeeBLL.UpdateEmployee(duzenlenecek, txtEmployeeName.Text, txtEmployeeSirname.Text, txtEmployeeSalary.Text, (int)cmbPozisyon.SelectedItem);
                MessageBox.Show(mesaj);
                ListEmployees(EmployeeBLL.GetEmployees());
            }
            catch (Exception)
            {
                MessageBox.Show("İçeride düzenlenecek çalışan yoktur.");
                throw;
            }
         
            EmployeeTemizle();
        }
        //Employee DELETE
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int silinecek = (int)dgvEmployee.CurrentRow.Cells["calisanID"].Value;
                var sonuc = EmployeeBLL.DeleteEmployee(silinecek);
                if (sonuc)
                {
                    MessageBox.Show("Çalışan silindi.");
                    //Bağlantılı kullanıcıyı sildik!!
                    EmployeeBLL.DeleteEmployeesUser(silinecek);
                }
                else
                {
                    MessageBox.Show("Çalışan silinirken hata oluştu.");
                }
                ListEmployees(EmployeeBLL.GetEmployees());
            }
            catch (Exception)
            {

                MessageBox.Show("İçeride silinecek çalışan yoktur.");
            }
           
        }
        //User ADD
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            var sonuc = UserBLL.AddUser(txtUserName.Text, txtPassword.Text, (int)cmbCalisan.SelectedValue);
            if (sonuc.Item1)
            {
                MessageBox.Show(sonuc.Item2);
            }
            else
            {
                MessageBox.Show(sonuc.Item2);
            }
            ListUsers(UserBLL.GetUsers());
        }
        //User UPDATE
        private void btnModifyUser_Click(object sender, EventArgs e)
        {
            try
            {
                int duzenlenecek = (int)dgvKullanici.CurrentRow.Cells["userID"].Value;
                var f = UserBLL.GetEmployeesUser((int)cmbCalisan.SelectedValue);
                if (f.Count()<=0)
                {
                    var sonuc = UserBLL.UpdateUser(duzenlenecek, txtUserName.Text, txtPassword.Text, (int)cmbCalisan.SelectedValue);
                    if (sonuc.Item1)
                    {
                        MessageBox.Show(sonuc.Item2);
                    }
                    else
                    {
                        MessageBox.Show(sonuc.Item2);
                    }
                    ListUsers(UserBLL.GetUsers());
                }
                else
                {
                    MessageBox.Show("İçeride bu çalışan bir hesabı olduğundan başka bir hesaba bu kullanıcı atanamaz.");
                }
                
            }
            catch (Exception)
            {

                MessageBox.Show("İçeride düzenlenecek kullanıcı yoktur.");
            }
            
        }
        //User DELETE
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                int silinecek = (int)dgvKullanici.CurrentRow.Cells["userID"].Value;
                var sonuc = UserBLL.DeleteUser(silinecek);
                if (sonuc)
                {
                    MessageBox.Show("Kullanıcı silindi.");
                }
                else
                {
                    MessageBox.Show("Kullanıcı silinirken hata oluştu.");
                }

                ListUsers(UserBLL.GetUsers());
            }
            catch (Exception)
            {

                MessageBox.Show("İçeride silinecek kullanıcı yoktur.");
            }
          
        }
        //FOOD ADD
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            var mesaj = FoodBLL.AddFood(txtAdi.Text, txtMaliyeti.Text, txtFiyati.Text, (int)cmbKategori.SelectedValue, txtAdet.Text,txtResim.Text);
            MessageBox.Show(mesaj);
            ListFoods(FoodBLL.GetFoods());
            CleanTextsFood();
        }
        //FOOD UPDATE
        private void btnModifyFood_Click(object sender, EventArgs e)
        {
            try
            {
                int duzenlenecek = (int)dgvYemek.CurrentRow.Cells["yemekID"].Value;
                var mesaj = FoodBLL.UpdateFood(duzenlenecek, txtAdi.Text, txtMaliyeti.Text, txtFiyati.Text, (int)cmbKategori.SelectedValue, txtAdet.Text, txtResim.Text);
                MessageBox.Show(mesaj);
                ListFoods(FoodBLL.GetFoods());
            }
            catch (Exception)
            {

                MessageBox.Show("İçeride düzenlenecek yemek yoktur.");
            }
          
            CleanTextsFood();
        }
        //FOOD TEMİZLE
        public void CleanTextsFood()
        {
            txtAdi.Text = "";
            txtMaliyeti.Text = "";
            txtFiyati.Text = "";
            txtAdet.Text = "";
            txtResim.Text = "";
        }
        //FOOD DELETE
        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            try
            {
                int silinecek = (int)dgvYemek.CurrentRow.Cells["yemekID"].Value;
                if (FoodBLL.DeleteFood(silinecek))
                {
                    MessageBox.Show("Yemek silindi.");
                }
                else
                {
                    MessageBox.Show("Yemek silinirken hata oluştu.");
                }
                ListFoods(FoodBLL.GetFoods());
            }
            catch (Exception)
            {

                MessageBox.Show("İçeride silinecek yemek yoktur.");
            }
          
            CleanTextsFood();
        }
        Bitmap image;
        private void btnResimEkle_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                image = new Bitmap(openFileDialog1.FileName);
                byte[] imageArray = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
                string base64Text = Convert.ToBase64String(imageArray);
                txtResim.Text = base64Text;
            }

        }

        private void tpYemek_Click(object sender, EventArgs e)
        {

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSiparisOlustur fso = new FrmSiparisOlustur(1,um.CalisanID);
            fso.Show();

        }

        private void btn2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSiparisOlustur fso = new FrmSiparisOlustur(2, um.CalisanID);
            fso.Show();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSiparisOlustur fso = new FrmSiparisOlustur(3, um.CalisanID);
            fso.Show();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSiparisOlustur fso = new FrmSiparisOlustur(4, um.CalisanID);
            fso.Show();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSiparisOlustur fso = new FrmSiparisOlustur(5, um.CalisanID);
            fso.Show();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSiparisOlustur fso = new FrmSiparisOlustur(6, um.CalisanID);
            fso.Show();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSiparisOlustur fso = new FrmSiparisOlustur(7, um.CalisanID);
            fso.Show();
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            this.Hide();
            
            FrmSiparisOlustur fso = new FrmSiparisOlustur(8, um.CalisanID);
            fso.Show();
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSiparisOlustur fso = new FrmSiparisOlustur(9, um.CalisanID);
            fso.Show();
        }
        public void ButtonPaintAndIcons(Button btn)
        {
            btn.Text = "";
            if (btn.BackColor == Color.Green)
            {
                btn.BackgroundImage = Properties.Resources.icons8_empty_box_96px;
                btn.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                btn.BackColor = Color.Red;
                btn.BackgroundImage = Properties.Resources.icons8_full_trash_200px;
                btn.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }
        private void btn1_MouseHover(object sender, EventArgs e)
        {
            ButtonPaintAndIcons(btn1);
        }

        private void btn2_MouseHover(object sender, EventArgs e)
        {
            ButtonPaintAndIcons(btn2);
        }
      
        private void btn3_MouseHover(object sender, EventArgs e)
        {

            ButtonPaintAndIcons(btn3);
        }

        private void btn4_MouseHover(object sender, EventArgs e)
        {

            ButtonPaintAndIcons(btn4);
        }

        private void btn5_MouseHover(object sender, EventArgs e)
        {

            ButtonPaintAndIcons(btn5);
        }

        private void btn6_MouseHover(object sender, EventArgs e)
        {

            ButtonPaintAndIcons(btn6);
        }

        private void btn7_MouseHover(object sender, EventArgs e)
        {
            ButtonPaintAndIcons(btn7);
        }

        private void btn8_MouseHover(object sender, EventArgs e)
        {

            ButtonPaintAndIcons(btn8);
        }

        private void btn9_MouseHover(object sender, EventArgs e)
        {
            ButtonPaintAndIcons(btn9);
        }

        private void btnAdminKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn1_MouseLeave(object sender, EventArgs e)
        {
            btn1.BackgroundImage = Properties.Resources.icons8_1_200px;
        }

        private void btn2_MouseLeave(object sender, EventArgs e)
        {
            btn2.BackgroundImage = Properties.Resources.icons8_2_200px;
        }

        private void btn3_MouseLeave(object sender, EventArgs e)
        {
            btn3.BackgroundImage = Properties.Resources.icons8_3_200px;
        }

        private void btn4_MouseLeave(object sender, EventArgs e)
        {
            btn4.BackgroundImage = Properties.Resources.icons8_4_200px;
        }

        private void btn5_MouseLeave(object sender, EventArgs e)
        {
            btn5.BackgroundImage = Properties.Resources.icons8_5_200px_1;
        }

        private void btn6_MouseLeave(object sender, EventArgs e)
        {
            btn6.BackgroundImage = Properties.Resources.icons8_6_200px_1;
        }

        private void btn7_MouseLeave(object sender, EventArgs e)
        {
            btn7.BackgroundImage = Properties.Resources.icons8_7_200px;
        }

        private void btn8_MouseLeave(object sender, EventArgs e)
        {
            btn8.BackgroundImage = Properties.Resources.icons8_8_200px;
        }

        private void btn9_MouseLeave(object sender, EventArgs e)
        {
            btn9.BackgroundImage = Properties.Resources.icons8_9_200px;
        }

        private void tpKullanici_Click(object sender, EventArgs e)
        {
            AddEmployeesToUser(EmployeeBLL.GetEmployees());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbMasa_CheckedChanged(object sender, EventArgs e)
        {
            MasalariDoldur(TableBLL.GetTables());
        }
        public void MasalariDoldur(List<TableModel>f)
        {
          
           
            cmbSecim.DataSource = f;
            cmbSecim.DisplayMember = "MasaID";
            cmbSecim.ValueMember = "MasaID";
        }

        private void rbUrun_CheckedChanged(object sender, EventArgs e)
        {
            YemekleriDoldur();
        }
        public void YemekleriDoldur()
        {
           
            var f = FoodBLL.GetFoods();
            cmbSecim.DataSource = f;
            cmbSecim.DisplayMember = "YemekAdi";
            cmbSecim.ValueMember = "YemekID";
        }

        private void rbGarson_CheckedChanged(object sender, EventArgs e)
        {
            GarsonlariDoldur();
        }
        public void GarsonlariDoldur()
        {

           
            var f = EmployeeBLL.GetEmployees();
            cmbSecim.DataSource = f;
            cmbSecim.DisplayMember = "CalisanAdi";
            cmbSecim.ValueMember = "CalisanID";
        }

        private void rbKategori_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void tcAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListUsers(UserBLL.GetUsers());
            ListEmployees(EmployeeBLL.GetEmployees());
            AddEmployeesToUser(EmployeeBLL.GetEmployees());
            AddCategoriesToFood();
            ListFoods(FoodBLL.GetFoods());
            SiparisListele(OrderBLL.GetOrders());
        }

        private void cmbSecim_SelectedIndexChanged(object sender, EventArgs e)
        {
          

           
         

        }

        private void btnGetir_Click(object sender, EventArgs e)
        {
            DateTime basTarih = dtpBaslangic.Value;
            DateTime sonTarih = dtpBitis.Value;
            List<TableModel> tml = new List<TableModel>();
           
            if (rbMasa.Checked)
            {
                int adet = 0;

                decimal karZarar=0;
                try
                {
                    var masa=int.Parse(cmbSecim.SelectedValue.ToString());
                    var x = OrderBLL.GetTablesOrders(masa, basTarih, sonTarih);
                    if (x.Count()>0)
                    {
                        dgvSatislar.Rows.Clear();
                        foreach (var item in x)
                        {
                            dgvSatislar.Rows.Add(item.SiparisID, item.MasaID, item.calisan.CalisanAdi, item.AlınmaZamani, $"{item.Tutari} ₺");
                            var yemekleri = FoodNavigateBLL.GetFoodNavigates(item.SiparisID);
                            adet += yemekleri.Count();
                            foreach (var yemek in yemekleri)
                            {

                                karZarar += yemek.yemek.YemekFiyati - yemek.yemek.YemekMaliyeti;
                            }

                        }
                        lblSatilanAdet.Text = x.Count().ToString();
                        lblKarZarar.Text = $"{karZarar.ToString()} ₺";
                    }
                    else
                    {
                        MessageBox.Show("Bu masanın içeride siparişi yoktur.");
                    }
                  
                }
                catch (Exception)
                {

                    MessageBox.Show("1 ile 9 arası masa değeri seçin!");
                }
          
                
            }
            else if (rbGarson.Checked)
            {
                int adet = 0;
                decimal karZarar = 0;
                try
                {
                    string garson = cmbSecim.SelectedValue.ToString();
                    var gelen = EmployeeBLL.GetEmployeeByID(Convert.ToInt32(garson));
                    var x = OrderBLL.GetWaitersOrders(Convert.ToInt32(garson), basTarih, sonTarih);
                    if (x .Count()>0)
                    {
                    
                        dgvSatislar.Rows.Clear();
                        foreach (var item in x)
                        {
                            dgvSatislar.Rows.Add(item.SiparisID, item.MasaID, item.calisan.CalisanAdi, item.AlınmaZamani, $"{item.Tutari} ₺");
                            var yemekleri = FoodNavigateBLL.GetFoodNavigates(item.SiparisID);
                            adet += yemekleri.Count();
                            foreach (var yemek in yemekleri)
                            {

                                karZarar += yemek.yemek.YemekFiyati - yemek.yemek.YemekMaliyeti;
                            }

                        }
                        lblSatilanAdet.Text = x.Count().ToString();
                        lblKarZarar.Text = $"{karZarar.ToString()} ₺";
                    }
                    else
                    {
                        MessageBox.Show("Bu garsonun içeride siparişi yoktur.");
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Listeden garson seçtiğinize emin olun.");
                }
               
                
            }
            else
            {
            

            }
        }

        private void btnHepsiniGetir_Click(object sender, EventArgs e)
        {
            DateTime bas = dtpBaslangic.Value;
            DateTime son = dtpBitis.Value;
            SiparisListele(OrderBLL.GetOrdersByDate(bas, son));
        }

        private void tpRapor_Click(object sender, EventArgs e)
        {

        }
    }
}

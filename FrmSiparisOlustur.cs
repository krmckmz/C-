using Restaurant.BLL;
using Restaurant.DAL.Model;
using Restaurant.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant
{
    public partial class FrmSiparisOlustur : Form
    {
        public FrmSiparisOlustur(int masaID, int calisanID)
        {
            InitializeComponent();
            this.masaID = masaID;
            this.calisanID = calisanID;
        }
        Button btn;
        Label lbl;

    
        int masaID;
        int calisanID;
        int orderID;
        private void FrmSiparisOlustur_Load(object sender, EventArgs e)
        {
            NesneleriOlustur(FoodBLL.GetFoods());
            ListOrderFoods(masaID);

            //YemekEkleyebilsin(OrderBLL.GetLastAddedOrder(masaID).Item1);

            //İçeride ödenmeyen sipariş yoksa bunu true yap.
            //pbSiparisOlustur.Visible = true;


        }
        //public void YemekEkleyebilsin(OrderModel om)
        //{
        //    if (om != null)
        //    {
        //        btn.Enabled = true;
        //        txt.Enabled = true;
        //    }
        //} 
        int sayac = 99;
        public void NesneleriOlustur(List<FoodModel> fml)
        {
            List<Button> btnList = new List<Button>();
            foreach (var item in fml)
            {

                //Label
                lbl = new Label();
                lbl.Text = item.YemekAdi;
                lbl.Font = new Font("Arial", 11, FontStyle.Bold);
                flpYemek.Controls.Add(lbl);
                lbl.ForeColor = Color.White;
                //lbl2 = new Label();
                //lbl2.Text = item.YemekID.ToString();
                //lbl2.Visible = true;
                //flpYemek.Controls.Add(lbl2);


                //Button
                btn = new Button();

                byte[] imageBytes = Convert.FromBase64String(item.YemekResmi);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                //if (item.YemekMevcutAdet<5)
                //{
                //    btn.Text = $"Adet:{item.YemekMevcutAdet.ToString()}"; 
                //}
                try
                {
                    Image image = Image.FromStream(ms, true);
                    btn.BackgroundImage = image;
                 
                }
                catch (Exception)
                {
                    btn.BackgroundImage = Properties.Resources.icons8_image_200px;
                   
                }
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                btn.FlatStyle = FlatStyle.Popup;
                btn.Name = "btn" + sayac;
                btn.Click += new EventHandler(this.Btn_Click);
                btn.MouseHover += new EventHandler(this.Btn_MouseHover);
                btn.MouseLeave += new EventHandler(this.Btn_MouseLeave);
                btn.Margin = new Padding(0);
                btn.Size = new Size(125, 125);
                btn.Tag = item.YemekID;
                btnList.Add(btn);
                flpYemek.Controls.Add(btn);
                sayac++;


            }
        }
        //Sipariş Yemekleri Listeleme
        public void ListOrderFoods(int masaID)
        {
            decimal tutar=0;
            var f = OrderBLL.GetLastAddedOrder(masaID);
            orderID = f.Item1.SiparisID;
            if (f.Item2)
            {
                var liste = FoodNavigateBLL.GetLastOrderFoods(orderID);
                dgvSiparisYemekleri.Rows.Clear();
                foreach (var item in liste)
                {
                    dgvSiparisYemekleri.Rows.Add(item.KayitID, item.SiparisID, item.yemek.YemekAdi,$"{ item.yemek.YemekFiyati+ " ₺"}");
                    tutar += item.yemek.YemekFiyati;
                }
                lblTutar.Text = $"{tutar} ₺";
            }

        }
        private void Btn_MouseLeave(object sender,EventArgs e)
        {
            //Button b = (Button)sender;
            //var y = FoodBLL.GetFoodByID(Convert.ToInt32(b.Tag));
            //(b.Name).Text = "";
            //byte[] imageBytes = Convert.FromBase64String(y.YemekResmi);
            //MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            //ms.Write(imageBytes, 0, imageBytes.Length);
            //Image image = Image.FromStream(ms, true);
            //btn.BackgroundImage = image;
            //btn.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void Btn_MouseHover(object sender,EventArgs e)
        {
            //Button b = (Button)sender;
            //var y=FoodBLL.GetFoodByID(Convert.ToInt32(b.Tag));

            //btn.Text = $"Adet:{y.YemekMevcutAdet.ToString()}";
            //btn.BackgroundImage = null;
            //if (y.YemekMevcutAdet<1)
            //{
            //    btn.Enabled = false;

            //}
        }
        //Dinamik Yemek Basma
        private void Btn_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            orderID = OrderBLL.GetLastAddedOrder(masaID).Item1.SiparisID;
            if (orderID > 0)
            {
                var donen = FoodNavigateBLL.AddFoodNavigateTable(orderID, Convert.ToInt32(b.Tag.ToString()),txtAdet.Text);
                MessageBox.Show(donen.Item2);
                ListOrderFoods(masaID);
                //Tutarı güncelle.
                OrderBLL.UpdateOrderBill(masaID);
            }
            else
            {
                MessageBox.Show("Lütfen önce sipariş kaydı açın.");
            }



            //O sipariş ID'si ile birlikte button tag'ındaki ID'li yemeği textbox adedi ile ara tabloya ekle.
        }

        private void btnSiparisOlustur_Click(object sender, EventArgs e)
        {
            
            //Masa durumunu dolu set et++.
            var sonSiparis = OrderBLL.GetLastAddedOrder(masaID);
            if (sonSiparis.Item2!=true )
            {
                var donen = OrderBLL.AddOrder(masaID, calisanID);
                if (donen)
                {

                    MessageBox.Show("Sipariş eklendi.");
                    TableBLL.SetTableBusy(masaID);
                }
                else
                {
                    MessageBox.Show("Sipariş eklenirken hata oluştu.");
                }
            }
            else
            {
                MessageBox.Show("İçeride kapanmamış bir sipariş var.");
            }

            //int siparisID = donen.Item2.SiparisID;
            //orderID = siparisID;
        }

        private void btnOdendi_Click(object sender, EventArgs e)
        {
           var faturaSiparis = OrderBLL.GetLastAddedOrder(masaID).Item1;
          
            var yemekVarmi = FoodNavigateBLL.GetLastOrderFoods(OrderBLL.GetLastAddedOrder(masaID).Item1.SiparisID);
            if (yemekVarmi.Count()>0)
            {
                var f = OrderBLL.PayOrder(masaID);
                if (f.Item1)
                {
                    MessageBox.Show(f.Item2);
                    //Masayı boş set et.
                    TableBLL.SetTableFree(masaID);
                    //Adisyon bas.
                    FrmAdisyon fad = new FrmAdisyon(faturaSiparis);
                    this.Hide();
                    fad.Show();
                }
                else
                {
                    MessageBox.Show(f.Item2);
                }
            }
            else
            {
                MessageBox.Show("Masanın yemeği olmadığından , hesap almak yerine sipariş iptal edilmelidir.");
            }

            

        }

        private void btnYemekSil_Click(object sender, EventArgs e)
        {
            var f=FoodNavigateBLL.GetLastOrderFoods(OrderBLL.GetLastAddedOrder(masaID).Item1.SiparisID);

            if (f.Count()>0)
            {
                
                int silinecek = (int)dgvSiparisYemekleri.CurrentRow.Cells["kayitID"].Value;
                string yemek = dgvSiparisYemekleri.CurrentRow.Cells["yemekID"].Value.ToString();
                var y=FoodBLL.GetFoodByName(yemek).First();
                if (FoodNavigateBLL.DeleteFromFoodNavigate(silinecek))
                {

                    OrderBLL.UpdateOrderBill(masaID);
                    MessageBox.Show("Yemek silindi.");
                
                    //Yemeğin stoğunu güncelle.
                    FoodBLL.UpdateFood(y.YemekID, y.YemekAdi,y.YemekMaliyeti.ToString(), y.YemekFiyati.ToString(), y.YemekKategorisi, (y.YemekMevcutAdet + 1).ToString(), y.YemekResmi);
                }
                else
                {
                    MessageBox.Show("Yemek silinirken hata oluştu.");
                }
                ListOrderFoods(masaID);
            }
            else
            {
                MessageBox.Show("Bu siparişin silinecek yemeği yoktur!");
            }

        }

        private void FrmSiparisOlustur_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();

            UserBLL.GetOnlineUser().OpenFormsForUsers();
             
            
        
        }
       
        private void btnİptal_Click(object sender, EventArgs e)
        {
            var donen = OrderBLL.CancelOrder(masaID);
         
            
                if (donen.Item1)
                {
                    MessageBox.Show(donen.Item2);
                //Masayı boş set et.
                    TableBLL.SetTableFree(masaID);
                }
                else
                {
                    MessageBox.Show(donen.Item2);
                }
            
          
          
        }

        private void btnKapatSiparis_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

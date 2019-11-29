using Restaurant.BLL;
using Restaurant.DAL.Entity;
using Restaurant.DAL.Model;
using Restaurant.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant
{
    public partial class FrmAdisyon : Form
    {
        public FrmAdisyon(Siparis s)
        {
            InitializeComponent();
            this.s = s;
        }
        Siparis s;
        decimal tutar;
        private void FrmAdisyon_Load(object sender, EventArgs e)
        {
            lblSaat.Text = DateTime.Now.ToString();
            lblSiparisID.Text = $"Sipariş No: {s.SiparisID.ToString()}";
            lblMasaID.Text= $"Masa No: {s.MasaID.ToString()}";
            lblTutar.Text = $"{TutarHesapla(s.SiparisID).ToString()} ₺";
            ListOrderFoods(FoodNavigateBLL.GetLastOrderFoods(s.SiparisID));
        }
        public decimal TutarHesapla(int siparisID)
        {
            var yemekler = FoodNavigateBLL.GetLastOrderFoods(siparisID);
            foreach (var item in yemekler)
            {
                tutar += item.yemek.YemekFiyati;
            }
            return tutar;
        }
        public void ListOrderFoods(List<FoodNavigateTableModel>fntl)
        {
            dgvAdisyon.Rows.Clear();
            foreach (var item in fntl)
            {
                dgvAdisyon.Rows.Add(item.yemek.YemekAdi,$"{item.yemek.YemekFiyati} ₺");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAdisyon_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            UserBLL.GetOnlineUser().OpenFormsForUsers();
          

        }
    }
}

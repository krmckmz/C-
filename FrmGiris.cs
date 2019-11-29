using Restaurant.BLL;
using Restaurant.DAL.Entity;
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

namespace Restaurant
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        private void FrmGiris_Load(object sender, EventArgs e)
        {
            
          

        }
      
        private void btnCikis_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
           
            if (KullaniciDoluMu(txtKullaniciAdi.Text,txtParola.Text))
            {
                try
                {
                    var sonuc = UserBLL.VerifyUser(txtKullaniciAdi.Text, Convert.ToInt32(txtParola.Text));
                    if (sonuc.Item2 == true)
                    {


                        try
                        {
                            Hide();
                            UserBLL.SetUserOnline(sonuc.Item1);
                            UserBLL.OpenFormsForUsers(sonuc.Item1);

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Kullanıcı formu açılırken hata oluştu.");

                        }

                    }
                    else
                    {
                        MessageBox.Show(sonuc.Item3);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hata ile karşılaşıldı.");
            
                }
              
                
            }
            else
            {
                MessageBox.Show("Bilgilerinizi eksiksiz giriniz.");
            }
           
        }
        public bool KullaniciDoluMu(string userName,string passWord)
        {
            if (userName!=""&&passWord!="")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

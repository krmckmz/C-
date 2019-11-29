using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
namespace Restaurant.DAL.Model
{
    public class EmployeeModel
    {
        public int CalisanID { get; set; }
        private string calisanAdi;

        public string CalisanAdi
        {
            get { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(calisanAdi); }
            set { calisanAdi = value.ToLower(); }
        }
        private string calisanSoyadi;

        public string CalisanSoyadi
        {
            get { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(calisanSoyadi); }
            set { calisanSoyadi = value.ToLower(); }
        }

        //public string CalisanAdi { get; set; }
        //public string CalisanSoyadi { get; set; }
        public decimal Maasi { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public Nullable<System.DateTime> CikisTarihi { get; set; }
   
        public int Pozisyonu { get; set; }
        public bool isDeleted { get; set; }
    }
}

using Restaurant.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
namespace Restaurant.DAL.Model
{
    public class FoodModel:Yemek
    {
        public override string YemekAdi { get => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(base.YemekAdi); set => base.YemekAdi = value.ToLower(); }

        

        //public int YemekID { get; set; }
        //public string YemekAdi { get; set; }
        //public decimal YemekFiyati { get; set; }
        //public int YemekKategorisi { get; set; }
        //public byte[] YemekResmi { get; set; }
        //public decimal YemekMaliyeti { get; set; }
        //public int YemekMevcutAdet { get; set; }
        //public bool isDeleted { get; set; }
    }
}

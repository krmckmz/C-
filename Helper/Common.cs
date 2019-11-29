using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Helper
{
    public static class Common
    {
        public  enum foodCategory {Çorba=1,Salata,AraSıcak,AnaYemek,Mezeler,Tatlılar,İçecekler}
        public enum siparisDurumu { Alındı,TeslimEdildi,Ödendi,İptal}
        public enum masaDurumu { Boş,Dolu}
        public enum calisanPozisyonu { admin=1,garson=2}

    }
}

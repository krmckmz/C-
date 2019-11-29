using Restaurant.DAL.Entity;
using Restaurant.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Helper
{
    public static class Extension
    {
        public static UserModel ConvertToUserModel(this Kullanici k)
        {

            return new UserModel { KullaniciID = k.KullaniciID, KullaniciAdi = k.KullaniciAdi, CalisanID = k.CalisanID, Calisan = k.Calisan.ConvertToEmployeeModel(), Sifre = k.Sifre, OnlineMİ = k.OnlineMİ, isDeleted = k.isDeleted };
        }
        public static Kullanici ConvertToKullanici(this UserModel um)
        {
            return new Kullanici {KullaniciID=um.KullaniciID,KullaniciAdi=um.KullaniciAdi,Sifre=um.Sifre,CalisanID=um.CalisanID,OnlineMİ=um.OnlineMİ,isDeleted=um.isDeleted };
        }
        public static EmployeeModel ConvertToEmployeeModel(this Calisan c)
        {
            return new EmployeeModel {CalisanID=c.CalisanID,CalisanAdi=c.CalisanAdi,CalisanSoyadi=c.CalisanSoyadi,Maasi=c.Maasi,Pozisyonu=c.Pozisyonu,BaslangicTarihi=c.BaslangicTarihi,CikisTarihi=c.CikisTarihi,isDeleted=c.isDeleted };
        }
        public static Calisan ConvertToCalisan(this EmployeeModel em)
        {
            return new Calisan {CalisanID=em.CalisanID,CalisanAdi=em.CalisanAdi,CalisanSoyadi=em.CalisanSoyadi,BaslangicTarihi=em.BaslangicTarihi,CikisTarihi=em.CikisTarihi,isDeleted=em.isDeleted,Maasi=em.Maasi,Pozisyonu=em.Pozisyonu };
        }
        public static FoodModel ConvertToFoodModel(this Yemek y)
        {
            return new FoodModel { YemekID = y.YemekID, YemekAdi = y.YemekAdi, YemekKategorisi = y.YemekKategorisi, YemekFiyati = y.YemekFiyati, YemekMaliyeti = y.YemekMaliyeti, YemekMevcutAdet = y.YemekMevcutAdet, YemekResmi = y.YemekResmi, isDeleted = y.isDeleted };
        } 
        public static Yemek ConvertToYemek(this FoodModel fm)
        {
            return new Yemek { YemekID = fm.YemekID, YemekAdi = fm.YemekAdi, YemekKategorisi = fm.YemekKategorisi, YemekFiyati = fm.YemekFiyati, YemekMaliyeti = fm.YemekMaliyeti, YemekMevcutAdet = fm.YemekMevcutAdet, YemekResmi = fm.YemekResmi, isDeleted = fm.isDeleted };
        }
        public static TableModel ConvertToTableModel(this Masa m)
        {
            return new TableModel { MasaID = m.MasaID, Kapasitesi = m.Kapasitesi, MasaDurumu = m.MasaDurumu };
        }
        public static Masa ConvertToMasa(this TableModel tm)
        {
            return new Masa { MasaID = tm.MasaID, MasaDurumu = tm.MasaDurumu, Kapasitesi = tm.Kapasitesi };
        }
        public static OrderModel ConvertToOrderModel(this Siparis s)
        {
            return new OrderModel { calisan = s.Calisan.ConvertToEmployeeModel(),
                masa = s.Masa.ConvertToTableModel(),
                SiparisID = s.SiparisID, MasaID = s.MasaID,
                isDeleted = s.isDeleted,
                Tutari = s.Tutari ,
                CalisanID =s.CalisanID,
                AlınmaZamani =s.AlınmaZamani};
        }
        public static Siparis ConvertToSiparis(this OrderModel om)
        {
            return new Siparis {/*Calisan=om.calisan.ConvertToCalisan(),Masa=om.masa.ConvertToMasa(),*/SiparisID=om.SiparisID,isDeleted=om.isDeleted,Tutari=om.Tutari,AlınmaZamani=om.AlınmaZamani,CalisanID=om.CalisanID,Durumu=om.Durumu,MasaID=om.MasaID };
        }
        public static FoodNavigateTableModel ConvertToFoodNavigateTable(this YemekAraTablo yat)
        {
            return new FoodNavigateTableModel { KayitID = yat.KayitID, siparis = yat.Siparis.ConvertToOrderModel(), yemek = yat.Yemek.ConvertToFoodModel(), SiparisID = yat.SiparisID, YemekID = yat.YemekID };
        }
        public static YemekAraTablo ConvertToYemekAraTablo(this FoodNavigateTableModel fnt)
        {
            return new YemekAraTablo { KayitID = fnt.KayitID, YemekID = fnt.YemekID, /*Siparis = fnt.siparis.ConvertToSiparis()*/SiparisID = fnt.SiparisID,/* Yemek = fnt.yemek.ConvertToYemek()*/ };
        }
        public static bool IsItNumber(this int newPassword)
        {
            foreach (var hane in newPassword.ToString())
            {
                if (!char.IsNumber(hane))
                {
                    return false;
                }
            }
            return true;

        }
        public static bool IsItDecimal(this string maas)
        {
            try
            {
                decimal.Parse(maas);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public static bool IsItInteger(this string integer)
        {
            try
            {
                Int32.Parse(integer);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public static bool IsItString(this string userName)
        {
            foreach (var hane in userName)
            {
                if (char.IsNumber(hane))
                {
                    return false;
                }
            }
            return true;
        }
        public static  void OpenFormsForUsers(this UserModel u)
        {
            if (u.Calisan.Pozisyonu == (int)Common.calisanPozisyonu.admin)
            {
                FrmAdmin fa = new FrmAdmin(u);
                fa.Show();
            }
            else
            {
                FrmGarson fg = new FrmGarson(u);
                fg.Show();
            }
        }

    }
}

using Restaurant.DAL;
using Restaurant.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Helper;
using System.Data.Entity;
namespace Restaurant.BLL
{
    public static class FoodBLL
    {
        public static List<FoodModel> GetFoods()
        {
            return FoodDAL.GetFoods();
        }
        public static string AddFood(string adi, string maliyeti, string fiyati, int kategori, string adet,string resmi)
        {
            if (GetFoodByName(adi).Count()<=0)
            {
                var donen = GetFoodToAdd(adi, maliyeti, fiyati, kategori, adet, resmi);
                if (donen.Item2)
                {
                    try
                    {
                        FoodDAL.CRUD(donen.Item1.ConvertToYemek(), EntityState.Added);
                        return "Yemek eklendi.";
                    }
                    catch (Exception)
                    {

                        return "Yemek eklenirken hata oluştu.";
                    }

                }
                else
                {
                    return "Ad ve resim alanlarının metin, maliyet,fiyat ve adet alanlarının da sayılardan oluştuğuna emin olun.";
                }
            }
            else
            {
                return "İçeride mevcut olan yemek eklenemez.";
            }
          
        }
        public static List<FoodModel>GetFoodByName(string adi)
        {
            return FoodDAL.GetFoodByName(adi);
        }
        public static string UpdateFood(int ID,string adi, string maliyeti, string fiyati, int kategori, string adet,string resim)
        {
            if (GetFoodByName(adi).First().YemekID==ID)
            {
                var donen = GetFoodToUpdate(ID, adi, maliyeti, fiyati, kategori, adet, resim);
                if (donen.Item2)
                {
                    try
                    {
                        FoodDAL.CRUD(donen.Item1.ConvertToYemek(), EntityState.Modified);
                        return "Yemek güncellendi.";
                    }
                    catch (Exception)
                    {

                        return "Yemek güncellenirken hata oluştu.";
                    }

                }
                else
                {
                    return "Ad ve resim alanlarının metin, maliyet,fiyat ve adet alanlarının da sayılardan oluştuğuna emin olun.";
                }
            }
            else
            {
                return "Sistemde aynı ada kayıtlı tek bir yemek olabilir.";
            }
            
        }
        public static bool DeleteFood(int silinecek)
        {
            return FoodDAL.CRUD(FoodDAL.GetFoodToDelete(silinecek).ConvertToYemek(),EntityState.Modified);
        }
        public static Tuple<FoodModel, bool> GetFoodToAdd(string adi, string maliyeti, string fiyati, int kategori, string adet,string resmi)
        {
            FoodModel fm = new FoodModel();
            if (adi.IsItString() && maliyeti.IsItDecimal() && fiyati.IsItDecimal() && adet.IsItInteger()&&resmi.IsItInteger()==false)
            {
                
                fm.YemekResmi = resmi;
                fm.YemekAdi = adi;
                fm.YemekMaliyeti = Convert.ToDecimal(maliyeti);
                fm.YemekFiyati = Convert.ToDecimal(fiyati);
                fm.YemekKategorisi = Convert.ToInt32(kategori);
                fm.YemekMevcutAdet = Convert.ToInt32(adet);
                fm.isDeleted = false;
                return new Tuple<FoodModel, bool>(fm, true);
            }
            else
            {
                return new Tuple<FoodModel, bool>(fm, false);
            }
        }
        public static Tuple<FoodModel,bool>GetFoodToUpdate(int ID, string adi, string maliyeti, string fiyati, int kategori, string adet,string resim)
        {
            FoodModel fm = new FoodModel();
            if (adi.IsItString() && maliyeti.IsItDecimal() && fiyati.IsItDecimal() && adet.IsItInteger()&&resim.IsItInteger()==false)
            {
                fm.YemekResmi = resim;
                fm.YemekID = ID;
                fm.YemekAdi = adi;
                fm.YemekMaliyeti = Convert.ToDecimal(maliyeti);
                fm.YemekFiyati = Convert.ToDecimal(fiyati);
                fm.YemekKategorisi = Convert.ToInt32(kategori);
                fm.YemekMevcutAdet = Convert.ToInt32(adet);
                fm.isDeleted = false;
                return new Tuple<FoodModel, bool>(fm, true);
            }
            else
            {
                return new Tuple<FoodModel, bool>(fm, false);
            }
        }
        public static FoodModel GetFoodByID(int foodID)
        {
            return FoodDAL.GetFoodByID(foodID);
        }
    }
}

using Restaurant.DAL;
using Restaurant.DAL.Model;
using Restaurant.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL
{
    public static class FoodNavigateBLL
    {
        public static List<FoodNavigateTableModel>GetAllFoodNavigates()
        {
            return FoodNavigateDAL.GetAllFoodNavigates();
        }
        public static List<FoodNavigateTableModel>GetFoodNavigates(int siparisID)
        {
            return FoodNavigateDAL.GetFoodNavigates(siparisID);
        }
        public static List<FoodNavigateTableModel> GetLastOrderFoods(int siparisID)
        {
            List<FoodNavigateTableModel> fntl = new List<FoodNavigateTableModel>();

            var f = FoodNavigateDAL.GetLastOrderFoods(siparisID);
            foreach (var item in f)
            {

                fntl.Add(item);

            }
            return fntl;
        }
        public static Tuple<bool, string> AddFoodNavigateTable(int siparisID, int yemekID, string adet)
        {
            string mesaj="";
            bool sonuc=true;
            var y = FoodBLL.GetFoodByID(yemekID);
            if (y.YemekMevcutAdet>0)
            {
                var donen = GetFoodNavigateTableToAdd(siparisID, yemekID, adet);
                if (donen.Item2)
                {
                    for (int i = 0; i < int.Parse(adet); i++)
                    {
                        sonuc = FoodNavigateDAL.CRUD(donen.Item1.ConvertToYemekAraTablo(), System.Data.Entity.EntityState.Added);
                       
                        if (sonuc)
                        {

                            
                           
                            mesaj = "Yemek eklendi.";
                        }
                        else
                        {
                            mesaj = "Yemek eklenirken hata oluştu.";
                        }
                    }
                    //Yemek stoğunu güncelle.
                    FoodBLL.UpdateFood(yemekID, y.YemekAdi, y.YemekMaliyeti.ToString(), y.YemekFiyati.ToString(), y.YemekKategorisi, (y.YemekMevcutAdet - int.Parse(adet)).ToString(), y.YemekResmi);

                }
                else
                {
                    sonuc = false;
                    mesaj = "Lütfen adet değerine sayı girin.";
                }
            }
            else
            {
                sonuc = false;
                mesaj="Bu yemekten kalmamıştır.";
            }
            

           
            return new Tuple<bool, string>(sonuc, mesaj);
        }
        public static Tuple<FoodNavigateTableModel,bool> GetFoodNavigateTableToAdd(int siparisID,int yemekID,string adet)
        {
            bool sonuc;
            FoodNavigateTableModel fnt = new FoodNavigateTableModel();
            if (adet.IsItInteger())
            {
                fnt.siparis = OrderBLL.GetOrderByID(siparisID);
                fnt.yemek = FoodBLL.GetFoodByID(yemekID);
                fnt.SiparisID = siparisID;
                fnt.YemekID = yemekID;
                sonuc = true;
            }
            else
            {
                sonuc = false;
            }
            return new Tuple<FoodNavigateTableModel, bool>(fnt, sonuc);

        }
        public static bool DeleteFromFoodNavigate(int kayitID)
        {
            var silinecek=FoodNavigateDAL.GetFoodNavigateByID(kayitID);
            return FoodNavigateDAL.CRUD(silinecek.ConvertToYemekAraTablo(), System.Data.Entity.EntityState.Deleted);
            

        }
        public static FoodNavigateTableModel GetFoodNavigateByID(int kayitID)
        {
            return FoodNavigateDAL.GetFoodNavigateByID(kayitID);
        }

    }
}

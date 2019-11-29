using Restaurant.DAL.Entity;
using Restaurant.DAL.Model;
using Restaurant.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL
{
    public static class FoodNavigateDAL
    {
        public static bool CRUD(YemekAraTablo yat,EntityState state)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                db.Entry(yat).State = state;
                if (db.SaveChanges()>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static List<FoodNavigateTableModel> GetLastOrderFoods(int siparisID)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                List<FoodNavigateTableModel> fntl = new List<FoodNavigateTableModel>();
                var x = db.YemekAraTablo.Where(y=>y.SiparisID==siparisID).ToList();
                foreach (var item in x)
                {
                    fntl.Add(item.ConvertToFoodNavigateTable());
                }
                return fntl;
            }
        }
        public static FoodNavigateTableModel GetFoodNavigateByID(int kayitID)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                var x=db.YemekAraTablo.Find(kayitID).ConvertToFoodNavigateTable();
                return x;
            }
        }
        public static List<FoodNavigateTableModel>GetFoodNavigates(int siparisID)
        {
            List<FoodNavigateTableModel> fntl = new List<FoodNavigateTableModel>();
            using (RestaurantEntities db=new RestaurantEntities())
            {
               var f=  db.YemekAraTablo.Where(x => x.SiparisID == siparisID).ToList();
                foreach (var item in f)
                {
                    fntl.Add(item.ConvertToFoodNavigateTable());
                }
                return fntl;
            }
        }
        public static List<FoodNavigateTableModel>GetAllFoodNavigates()
        {
            List<FoodNavigateTableModel> fntl = new List<FoodNavigateTableModel>();
            using (RestaurantEntities db=new RestaurantEntities())
            {
                var f = db.YemekAraTablo.ToList();
                foreach (var item in f)
                {
                    fntl.Add(item.ConvertToFoodNavigateTable());
                }
                return fntl;
            }
        }
    }
}

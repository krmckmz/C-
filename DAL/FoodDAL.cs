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
    public static class FoodDAL
    {
        public static List<FoodModel>GetFoods()
        {
            List<FoodModel> fml = new List<FoodModel>();
            using (RestaurantEntities db=new RestaurantEntities())
            {
                var liste=db.Yemek.Where(y => y.isDeleted == false).ToList();
              
                foreach (var item in liste)
                {
                    fml.Add(item.ConvertToFoodModel());
                }

            }
            return fml;
        }

        public static bool CRUD(Yemek y,EntityState state)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                db.Entry(y).State = state;
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
        public static FoodModel GetFoodToDelete(int silinecek)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                var y=db.Yemek.Find(silinecek).ConvertToFoodModel();
                y.isDeleted = true;
                return y;
            }
        }
        public static FoodModel GetFoodByID(int foodID)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                return db.Yemek.Find(foodID).ConvertToFoodModel();
            }
        }
        public static List<FoodModel>GetFoodByName(string adi)
        {
            List<FoodModel> fml = new List<FoodModel>();
            using (RestaurantEntities db=new RestaurantEntities())
            {
                var f = db.Yemek.Where(y => y.YemekAdi.ToLower() == adi.ToLower()&&y.isDeleted==false);
                foreach (var item in f)
                {
                    fml.Add(item.ConvertToFoodModel());
                }
                return fml;
            }
        }
    }
}

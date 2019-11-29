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
   public static class EmployeeDAL
    {
        public static List<EmployeeModel>GetEmployeeByName(string ad,string soyad)
        {
            List<EmployeeModel> eml = new List<EmployeeModel>();
            using (RestaurantEntities db=new RestaurantEntities())
            {
                var f = db.Calisan.Where(c => c.CalisanAdi.ToLower() == ad.ToLower() && c.CalisanSoyadi.ToLower() == soyad.ToLower()&&c.isDeleted==false).ToList();
                foreach (var item in f)
                {
                    eml.Add(item.ConvertToEmployeeModel());
                }
                return eml;
            }
        }
        public static List<EmployeeModel> GetEmployeeByName(string ad)
        {
            List<EmployeeModel> eml = new List<EmployeeModel>();
            using (RestaurantEntities db = new RestaurantEntities())
            {
                var f = db.Calisan.Where(c => c.CalisanAdi.ToLower() == ad.ToLower()&& c.isDeleted == false).ToList();
                foreach (var item in f)
                {
                    eml.Add(item.ConvertToEmployeeModel());
                }
                return eml;
            }
        }
        public static List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> eml = new List<EmployeeModel>();
            using (RestaurantEntities db=new RestaurantEntities())
            {
                var liste= db.Calisan.Where(c => c.isDeleted == false).ToList();
                foreach (Calisan item in liste)
                {
                    eml.Add(item.ConvertToEmployeeModel());
                }

            }
            return eml;
        }
        public static bool EmployeeCRUD(Calisan c,EntityState state)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                db.Entry(c).State = state;
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
        
        public static EmployeeModel GetEmployeeToDelete(int ID)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                var c=db.Calisan.Find(ID);
                var em=c.ConvertToEmployeeModel();
                em.isDeleted = true;
                return em; 
            }
        }
        public static Calisan GetEmployeeByID(int ID)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
              return db.Calisan.Find(ID);
            }
        }
    }
}

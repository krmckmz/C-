using Restaurant.DAL.Entity;
using Restaurant.DAL.Model;
using Restaurant.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace Restaurant.DAL
{
    public static class UserDAL
    {
        public static bool UserCRUD(Kullanici k, EntityState state)
        {
            using (RestaurantEntities db = new RestaurantEntities())
            {
                db.Entry(k).State = state;
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static UserModel GetUserToDelete(int ID)
        {
            UserModel um = new UserModel();
            using (RestaurantEntities db = new RestaurantEntities())
            {
                try
                {
                    var f = db.Kullanici.Where(x => x.CalisanID == ID).First();
                    var ff = f.ConvertToUserModel();
                    ff.isDeleted = true;
                    return ff;
                }
                catch (Exception)
                {
                    return um;
                    throw;
                }
              
            }
        }
        public static List<UserModel> GetUsers()
        {
            List<UserModel> uml = new List<UserModel>();
            using (RestaurantEntities db = new RestaurantEntities())
            {
                var users = db.Kullanici.Where(k => k.isDeleted == false).ToList();
                foreach (var item in users)
                {
                    UserModel um = new UserModel();
                    um = item.ConvertToUserModel();
                    uml.Add(um);
                }
            }
            return uml;
        }
        public static Tuple<UserModel, bool, string> VerifyUser(string userName, int passWord)
        {
            bool sonuc;
            string mesaj = "";
            Kullanici user=null;
            using (RestaurantEntities db = new RestaurantEntities())
            {
                try
                {
                   user = db.Kullanici.Where(k => k.KullaniciAdi == userName && k.isDeleted == false).First();
                    if (user.Sifre == passWord)
                    {
                        sonuc = true;
                        //user.OnlineMİ = true;
                        //db.SaveChanges();
                    }
                    else
                    {
                        mesaj = "Şifre yanlış girildi."; ;
                        sonuc = false;

                    }
                }
                catch (Exception)
                {

                    mesaj = "Böyle bir kullanıcı bulunmamaktadır.";
                    sonuc = false;
                }  
               
              
                
                return new Tuple<UserModel, bool, string>(user.ConvertToUserModel(), sonuc, mesaj);
            }

        }
        public static bool SetUserOnline(UserModel um)
        {
            using (RestaurantEntities db = new RestaurantEntities())
            {

                var k = db.Kullanici.Find(um.ConvertToKullanici().KullaniciID);
                k.OnlineMİ = true;
                db.Entry(k).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool SetUserOffline(UserModel um)
        {
            using (RestaurantEntities db = new RestaurantEntities())
            {
                var k = db.Kullanici.Find(um.ConvertToKullanici().KullaniciID);
                k.OnlineMİ = false;
                db.Entry(k).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool SetNewPassword(UserModel um)
        {
            using (RestaurantEntities db = new RestaurantEntities())
            {
                db.Entry(um.ConvertToKullanici()).State = System.Data.Entity.EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }
        public static Tuple<bool, string> AddUser(UserModel um)
        {
            Kullanici k = um.ConvertToKullanici();
            bool sonuc;
            string mesaj;
            using (RestaurantEntities db = new RestaurantEntities())
            {
                db.Entry(k).State = EntityState.Added;
                if (db.SaveChanges() > 0)
                {
                    sonuc = true;
                    mesaj = "Kullanıcı eklendi.";
                }
                else
                {
                    sonuc = false;
                    mesaj = "Kullanıcı eklenirken hata oluştu.";
                }

            }
            return new Tuple<bool, string>(sonuc, mesaj);
        }
        public static Tuple<bool, string> UpdateUser(UserModel um)
        {
            Kullanici k = um.ConvertToKullanici();
            bool sonuc;
            string mesaj;
            using (RestaurantEntities db = new RestaurantEntities())
            {
                db.Entry(k).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    sonuc = true;
                    mesaj = "Kullanıcı güncellendi.";
                }
                else
                {
                    sonuc = false;
                    mesaj = "Kullanıcı güncellenirken hata oluştu.";
                }

            }

            return new Tuple<bool, string>(sonuc, mesaj);
        }
       
        public static UserModel GetOnlineUser()
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                var f = db.Kullanici.Where(x => x.OnlineMİ == true).First();
                return  f.ConvertToUserModel();
            }
        }
        public static List<UserModel> GetEmployeesUser(int calisanID)
        {
            List<UserModel> uml = new List<UserModel>();
            using (RestaurantEntities db=new RestaurantEntities())
            {
              var f=  db.Kullanici.Where(k => k.CalisanID == calisanID&&k.isDeleted==false).ToList();
                foreach (var item in f)
                {
                    uml.Add(item.ConvertToUserModel());
                }
                return uml;
            }
        }
    }
}

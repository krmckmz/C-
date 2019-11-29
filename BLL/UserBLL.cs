using Restaurant.DAL;
using Restaurant.DAL.Entity;
using Restaurant.DAL.Model;
using Restaurant.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL
{
    public static class UserBLL
    {
        public static UserModel GetUserToDelete(int ID)
        {
            return UserDAL.GetUserToDelete(ID);
        }
        public static bool UserCRUD(Kullanici k,EntityState state)
        {
            return UserDAL.UserCRUD(k, state);
        }
        public static List<UserModel> GetUsers()
        {
            return UserDAL.GetUsers();
        }
        public static Tuple<UserModel, bool, string> VerifyUser(string userName, int passWord)
        {
            return UserDAL.VerifyUser(userName, passWord);
        }
        public static void OpenFormsForUsers(UserModel um)
        {

            if (um.Calisan.Pozisyonu == 1)
            {

                FrmAdmin fa = new FrmAdmin(um);
                fa.Show();

            }
            else
            {
                FrmGarson fg = new FrmGarson(um);
                fg.Show();
            }
        }
        public static bool SetUserOnline(UserModel um)
        {
            return UserDAL.SetUserOnline(um);
        }
        public static bool SetUserOffline(UserModel um)
        {
            return UserDAL.SetUserOffline(um);
        }
        public static Tuple<bool, string> SetNewPassword(UserModel um, string newPassword, string newPasswordRepeat)
        {
            bool sonuc;
            string sonucMesaj = "";

            if (newPassword.Length > 0 && newPasswordRepeat.Length > 0)
            {
                try
                {
                    IsItNumber(Convert.ToInt32(newPassword));
                    IsItNumberRepeat(Convert.ToInt32(newPasswordRepeat));
                }
                catch (Exception)
                {
                    sonuc = false;
                    sonucMesaj = "Yeni şifre ve tekrarının sadece sayılardan oluştuğuna emin olun.";
                    return new Tuple<bool, string>(sonuc, sonucMesaj);
                }
                if (ArePasswordsEqual(Convert.ToInt32(newPassword), Convert.ToInt32(newPasswordRepeat)))
                {
                    um.Sifre = Convert.ToInt32(newPassword);
                    if (UserDAL.SetNewPassword(um))
                    {
                        sonucMesaj = "Şifre başarıyla güncellendi.";
                        sonuc = true;
                    }
                    else
                    {
                        sonucMesaj = "Şifre güncellenirken hata oluştu.";
                        sonuc = false;
                    }
                }
                else
                {
                    sonuc = false;
                    sonucMesaj = "Yeni şifre ve tekrarının aynı olduğuna emin olun.";
                }

            }
            else
            {
                sonuc = false;
                sonucMesaj = "Yeni parolanızı ve tekrarını eksiksiz girin.";
            }
            return new Tuple<bool, string>(sonuc, sonucMesaj);
        }
        public static UserModel GetOnlineUser()
        {
            return UserDAL.GetOnlineUser();
        }
        public static bool IsItNumber(int newPassword)
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
        public static bool IsItNumberRepeat(int newPasswordRepeat)
        {
            foreach (var hane in newPasswordRepeat.ToString())
            {
                if (!char.IsNumber(hane))
                {
                    return false;
                }

            }
            return true;
        }
        public static bool IsItString(string userName)
        {
            foreach (var hane in userName)
            {
                if (!char.IsLetter(hane))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ArePasswordsEqual(int newPassword, int newPasswordRepeat)
        {
            if (newPassword == newPasswordRepeat)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Tuple<bool, string> AddUser(string userName, string passWord, int calisanID)
        {

            var donen = GetUser(userName, passWord, calisanID);
            var f = UserBLL.GetEmployeesUser(calisanID);
            if (f.Count()<=0)
            {
                if (donen.Item2)
                {
                    return UserDAL.AddUser(donen.Item1);
                }
                else
                {
                    return new Tuple<bool, string>(false, "Kullanıcı adının yalnızca metin,şifrenin yalnızca sayılardan oluştuğuna emin olun.");
                }
            }
            else
            {
                return new Tuple<bool, string>(false, "İçeride bu çalışana ait bir kullanıcı olduğundan ekleme yapılamadı.");
            }
         




        }
        public static Tuple<bool, string> UpdateUser(int ID, string userName, string passWord, int calisanID)
        {

            var donen = GetUserToUpdate(ID, userName, passWord, calisanID);
            if (donen.Item2)
            {
                return UserDAL.UpdateUser(donen.Item1);
            }
            else
            {
                return new Tuple<bool, string>(false, "Kullanıcı adının yalnızca metin,şifrenin yalnızca sayılardan oluştuğuna emin olun.");
            }




        }
        public static bool DeleteUser(int silinecek)
        {
           var f= UserDAL.UpdateUser(UserDAL.GetUserToDelete(silinecek));
            return f.Item1;

        }
        public static Tuple<UserModel, bool> GetUser(string userName, string passWord, int calisanID)
        {

            UserModel um = new UserModel();
            try
            {
                IsItNumber(Convert.ToInt32(passWord));
                IsItString(userName);
                um.KullaniciAdi = userName;
                um.Sifre = Convert.ToInt32(passWord);
                um.isDeleted = false;
                um.OnlineMİ = false;
                um.CalisanID = calisanID;
                return new Tuple<UserModel, bool>(um, true);
            }
            catch (Exception)
            {
                return new Tuple<UserModel, bool>(um, false);

            }
        }
        public static List<UserModel>GetEmployeesUser(int calisanID)
        {
            return UserDAL.GetEmployeesUser(calisanID);
        }
        public static Tuple<UserModel, bool> GetUserToUpdate(int duzenlenecek, string userName, string passWord, int calisanID)
        {
            UserModel um = new UserModel();
            try
            {
                IsItNumber(Convert.ToInt32(passWord));
                IsItString(userName);
                um.KullaniciID = duzenlenecek;
                um.KullaniciAdi = userName;
                um.Sifre = Convert.ToInt32(passWord);
                um.isDeleted = false;
                um.OnlineMİ = false;
                um.CalisanID = calisanID;
                return new Tuple<UserModel, bool>(um, true);
            }
            catch (Exception)
            {
                return new Tuple<UserModel, bool>(um, false);

            }
        }
      
    }
}

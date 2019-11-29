using Restaurant.DAL;
using Restaurant.DAL.Model;
using Restaurant.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Restaurant.BLL
{
    public static class EmployeeBLL
    {
        public static List<EmployeeModel> GetEmployees()
        {
            return EmployeeDAL.GetEmployees();
        }
        public static string AddEmployee(string adi, string soyadi, string maasi, int pozisyonu)
        {

            if (GetEmployeeByName(adi,soyadi).Count()<=0)
            {
                if (GetEmployeeToAdd(adi, soyadi, maasi, pozisyonu).Item2)
                {
                    try
                    {
                        EmployeeDAL.EmployeeCRUD(GetEmployeeToAdd(adi, soyadi, maasi, pozisyonu).Item1.ConvertToCalisan(), EntityState.Added);
                        return "Çalışan eklendi.";
                    }
                    catch (Exception)
                    {
                        return "Çalışan eklenirken hata oluştu.";

                    }
                }
                else
                {
                    return "Ad ve soyad metin ,maas ve pozisyon sayısal ifadelerden oluşmalıdır.";
                }
            }
            else
            {
                return "Sistemde bu çalışan mevcut olduğundan eklenmedi.";
            }
          






        }
        public static string UpdateEmployee(int ID, string adi, string soyadi, string maasi, int pozisyonu)
        {
            var f = EmployeeBLL.GetEmployeeByName(adi,soyadi).First();
            if (f.CalisanID==ID)
            {
                if (GetEmployeeToUpdate(ID, adi, soyadi, maasi, pozisyonu).Item2)
                {
                    try
                    {
                        EmployeeDAL.EmployeeCRUD(GetEmployeeToUpdate(ID, adi, soyadi, maasi, pozisyonu).Item1.ConvertToCalisan(), EntityState.Modified);
                        return "Çalışan güncellendi.";
                    }
                    catch (Exception)
                    {

                        return "Çalışan güncellenirken hata oluştu.";
                    }

                }
                else
                {
                    return "Ad ve soyad metin ,maas ve pozisyon sayısal ifadelerden oluşmalıdır.";
                }
            }
            else
            {
                return "Mevcutta var olan bir çalışan başka bir kayıtta da var olamaz.";
            }
         
        }
        public static bool DeleteEmployee(int ID)
        {
            bool sonuc = EmployeeDAL.EmployeeCRUD(EmployeeDAL.GetEmployeeToDelete(ID).ConvertToCalisan(), EntityState.Modified);

            
            if (sonuc)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool DeleteEmployeesUser(int ID)
        {
            bool sonuc;
            var  f = UserDAL.GetUserToDelete(ID);
            if (f.KullaniciID>0)
            {
                 sonuc = UserBLL.UserCRUD(f.ConvertToKullanici(), EntityState.Modified);
            }
            else
            {
                sonuc = false;
            }
            return sonuc;
        }
        public static List<EmployeeModel>GetEmployeeByName(string ad,string soyad)
        {
            return EmployeeDAL.GetEmployeeByName(ad, soyad);
        }
        public static List<EmployeeModel> GetEmployeeByName(string ad)
        {
            return EmployeeDAL.GetEmployeeByName(ad);
        }
        public static Tuple<EmployeeModel, bool> GetEmployeeToAdd(string adi, string soyadi, string maasi, int pozisyonu)
        {

            EmployeeModel em = new EmployeeModel();
            if (adi.IsItString() && soyadi.IsItString() && maasi.IsItDecimal() && pozisyonu.ToString().IsItInteger())
            {

                em.CalisanAdi = adi;
                em.CalisanSoyadi = soyadi;
                em.Maasi = Convert.ToDecimal(maasi);
                em.Pozisyonu = Convert.ToInt32(pozisyonu);
                em.isDeleted = false;
                em.BaslangicTarihi = DateTime.Now;
                return new Tuple<EmployeeModel, bool>(em, true);

            }
            else
            {
                return new Tuple<EmployeeModel, bool>(em, false);
            }


        }
        public static Tuple<EmployeeModel, bool> GetEmployeeToUpdate(int ID, string adi, string soyadi, string maasi, int pozisyonu)
        {
            EmployeeModel em = new EmployeeModel();
            if (ID.IsItNumber() && adi.IsItString() && soyadi.IsItString() && maasi.IsItDecimal()&& pozisyonu.ToString().IsItInteger())
            {
                var f = GetEmployeeByID(ID);
                em.CalisanID = ID;
                em.CalisanAdi = adi;
                em.CalisanSoyadi = soyadi;
                em.Maasi = Convert.ToDecimal(maasi);
                em.Pozisyonu = Convert.ToInt32(pozisyonu);
                em.isDeleted = false;
                em.BaslangicTarihi = f.BaslangicTarihi;
                return new Tuple<EmployeeModel, bool>(em, true);

            }
            else
            {
                return new Tuple<EmployeeModel, bool>(em, false);
            }


        }
        public static EmployeeModel GetEmployeeByID(int ID)
        {
            return EmployeeDAL.GetEmployeeByID(ID).ConvertToEmployeeModel();
        }

    }
}

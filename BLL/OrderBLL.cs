using Restaurant.DAL;
using Restaurant.DAL.Model;
using Restaurant.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Restaurant.DAL.Entity;

namespace Restaurant.BLL
{
    public static class OrderBLL
    {
        public static bool AddOrder(int masaID,int calisanID)
        {
            
            return OrderDAL.AddOrder(GetOrderToAdd(masaID, calisanID).ConvertToSiparis());
        }
        public static OrderModel GetOrderToAdd(int masaID,int calisanID)
        {

            return new OrderModel {MasaID = TableBLL.GetTableByID(masaID).MasaID, CalisanID = EmployeeBLL.GetEmployeeByID(calisanID).CalisanID, Durumu = (int)Common.siparisDurumu.Alındı, isDeleted = false, AlınmaZamani = DateTime.Now,calisan=EmployeeBLL.GetEmployeeByID(calisanID),masa=TableBLL.GetTableByID(masaID) };
            
        }
        
        public static Tuple<Siparis,bool> GetLastAddedOrder(int masaID)
        {
            Siparis om=new Siparis();
            var donen = OrderDAL.GetLastAddedOrder(masaID);
            int silinenID = donen.Item1.SiparisID;
            if (donen.Item2)
            {
                return new Tuple<Siparis, bool>(donen.Item1.ConvertToSiparis(), true);
            }
            else
            {
                return new Tuple<Siparis, bool>(om, false);
            }

        }
        public static List<OrderModel>GetOrdersByDate(DateTime bas,DateTime son)
        {
            return OrderDAL.GetOrdersByDate(bas, son);
        }
        public static List<OrderModel>GetTablesOrders(int masaID,DateTime bas,DateTime son)
        {
            return OrderDAL.GetTablesOrders(masaID, bas, son);
        }
        public static List<OrderModel>GetWaitersOrders(int garsonID,DateTime bas,DateTime son)
        {
            return OrderDAL.GetWaitersOrders(garsonID, bas, son);
        }
        public static OrderModel GetOrderByID(int siparisID)
        {
            return OrderDAL.GetOrderByID(siparisID);
        }
        public static List<OrderModel>GetOrders()
        {
            return OrderDAL.GetOrders();
        }
        public static Tuple <bool,string> CancelOrder(int masaID)
        {   
            bool sonuc;
            string mesaj;
            var z=GetLastAddedOrder(masaID);
            if (z.Item2)
            {
                var f = OrderDAL.GetOrderToCancel(z.Item1);
                f.Durumu = (int)Common.siparisDurumu.İptal;
                var donen= OrderDAL.CRUD(f.ConvertToSiparis(), EntityState.Modified);
                if (donen.Item1)
                {
                    sonuc = true;
                    mesaj = "Sipariş iptal edildi.";
                    if (DeleteCancelledOrdersFood(f.SiparisID))
                    {
                        sonuc = true;
                        
                    }
                    else
                    {
                        sonuc = false;
                        mesaj = "İlgili kayıtlar silinirken hata oluştu.";
                    }
                }
                else
                {
                    sonuc = false;
                    mesaj = "Sipariş iptal edilirken hata oluştu.";
                }
            }
            else
            {
                sonuc = false;
                mesaj = "İçeride iptal edilecek sipariş yok.";
            }
            return new Tuple<bool, string>(sonuc,mesaj);

        }
        public static Tuple<bool,string>PayOrder(int masaID)
        {
            bool sonuc=true;
            string mesaj;
            var z = GetLastAddedOrder(masaID);
            if (z.Item2)
            {
                var f = OrderDAL.GetOrderToCancel(z.Item1);
                f.Durumu = (int)Common.siparisDurumu.Ödendi;
                var donen = OrderDAL.CRUD(f.ConvertToSiparis(), EntityState.Modified);
                if (donen.Item1)
                {
                    sonuc = true;
                    mesaj = "Sipariş hesabı alındı.";
                }
                else
                {
                    sonuc = false;
                    mesaj = "Sipariş iptal edilirken hata oluştu.";
                }
            }
            else
            {
                mesaj = "İçeride ödenecek sipariş yok.";
            }
            return new Tuple<bool, string>(sonuc, mesaj);
        }
        public static bool DeleteCancelledOrdersFood(int ID)
        {
            return OrderDAL.DeleteCancelledOrdersFood(ID);
        }
        public static bool UpdateOrderBill(int masaID)
        {
            return OrderDAL.UpdateOrderBill(masaID);
           
        }
    }
}

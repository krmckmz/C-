using Restaurant.BLL;
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
    public static class OrderDAL
    {
        public static List<OrderModel>GetOrdersByDate(DateTime bas,DateTime son)
        {
            List<OrderModel> oml = new List<OrderModel>();
            using (RestaurantEntities db=new RestaurantEntities())
            {
                var f = db.Siparis.Where(s => s.AlınmaZamani > bas && s.AlınmaZamani < son);
                foreach (var item in f)
                {
                    oml.Add(item.ConvertToOrderModel());

                }
                return oml;
            }

        }
        public static List<OrderModel>GetOrders()
        {
            List<OrderModel> oml = new List<OrderModel>();
            using (RestaurantEntities db=new RestaurantEntities())
            {
                var f = db.Siparis.Where(x=>x.Durumu==(int)Common.siparisDurumu.Ödendi||x.Durumu==(int)Common.siparisDurumu.İptal).ToList();
                foreach (var item in f)
                {
                    oml.Add(item.ConvertToOrderModel());
                }
                return oml;
            }
        }
        public static List<OrderModel>GetWaitersOrders(int garsonID,DateTime bas,DateTime son)
        {
            List<OrderModel> oml = new List<OrderModel>();
            using (RestaurantEntities db = new RestaurantEntities())
            {
                try
                {

                    var f = db.Siparis.Where(x => (x.AlınmaZamani > bas && x.AlınmaZamani < son) &&( x.CalisanID == garsonID) && (x.Tutari!=0)).ToList();
                    foreach (var item in f)
                    {
                        oml.Add(item.ConvertToOrderModel());
                    }
                    return oml;
                }
                catch (Exception)
                {
                    return oml;
                    throw;
                }

            }
        }
        public static List<OrderModel>GetTablesOrders(int masaID,DateTime bas,DateTime son)
        {
            List<OrderModel> oml = new List<OrderModel>();
            using (RestaurantEntities db=new RestaurantEntities())
            {
                try
                {
                    
                    var f = db.Siparis.Where(x => (x.AlınmaZamani > bas && x.AlınmaZamani < son) && x.MasaID == masaID&&(x.Tutari!=0)).ToList();
                    foreach (var item in f)
                    {
                        oml.Add(item.ConvertToOrderModel());
                    }
                    return oml;
                }
                catch (Exception)
                {
                    return oml;
                    throw;
                }
               
            }
        }
        public static Tuple<bool,Siparis> CRUD(Siparis s,EntityState state)
        {
            bool sonuc;
          
            using (RestaurantEntities db=new RestaurantEntities())
            {
                db.Entry(s).State = state;
                if (db.SaveChanges()>0)
                {
                    sonuc = true;
                   
                }
                else
                {
                    sonuc= false;
                }
                return new Tuple<bool, Siparis>(sonuc, s);
            }

        }
        public static bool AddOrder(Siparis s)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                db.Siparis.Add(s);
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
        public static  Tuple<OrderModel,bool> GetLastAddedOrder(int masaID)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                OrderModel si = new OrderModel();
                try
                {
                    //var s = db.Siparis.Where(o => (o.Durumu == (int)Common.siparisDurumu.TeslimEdildi || o.Durumu == (int)Common.siparisDurumu.Alındı) && o.MasaID == masaID && o.isDeleted == false).FirstOrDefault();
                    var     s = db.Siparis.Where(x => (x.Durumu == (int)Common.siparisDurumu.Alındı||x.Durumu==(int)Common.siparisDurumu.TeslimEdildi) && (x.MasaID == masaID)&&(x.isDeleted==false)).FirstOrDefault();
                    si = s.ConvertToOrderModel();
                    return new Tuple<OrderModel, bool>(si, true);
                }
                catch (Exception)
                {

                    return new Tuple<OrderModel, bool>(si, false);
                }
               
             
            }
        }
        public static OrderModel GetOrderByID(int siparisID)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                return db.Siparis.Find(siparisID).ConvertToOrderModel();
            }
        }
        public static OrderModel GetOrderToCancel(Siparis s)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
               return db.Siparis.Find(s.SiparisID).ConvertToOrderModel();
            }
        }
        public static bool DeleteCancelledOrdersFood(int ID)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                try
                {
                    var silinecekler = db.YemekAraTablo.Where(x => x.SiparisID == ID);
                    foreach (var item in silinecekler)
                    {
                        db.YemekAraTablo.Remove(item);
                    }
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
               

            }
        }
        public static bool UpdateOrderBill(int masaID)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                var f = GetLastAddedOrder(masaID).Item1;
                
                var liste = FoodNavigateBLL.GetLastOrderFoods(f.SiparisID);
                decimal bill = 0;

                foreach (var item in liste)
                {
                    bill += item.yemek.YemekFiyati;
                }
                f.Tutari = bill;

                if (CRUD(f.ConvertToSiparis(), EntityState.Modified).Item1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
           
        }
       
    }
}

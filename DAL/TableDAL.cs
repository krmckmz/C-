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
   public static class TableDAL
    {
        public static Masa GetTableByID(int ID)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
              return  db.Masa.Find(ID);
            }
        }
        public static List<TableModel>GetTables()
        {
            List<TableModel> tml = new List<TableModel>();
            using (RestaurantEntities db=new RestaurantEntities())
            {
                foreach (var item in db.Masa.ToList())
                {
                    tml.Add(item.ConvertToTableModel());
                }
                return tml;
            }
        }
        public static bool SetTableBusy(int masaID)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                var f=db.Masa.Find(masaID);
                db.Entry(f).State = EntityState.Detached;
                var x=f.ConvertToTableModel();
                x.MasaDurumu = (int)Common.masaDurumu.Dolu;
                db.Entry(x.ConvertToMasa()).State=EntityState.Modified;
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
        public static bool SetTableFree(int masaID)
        {
            using (RestaurantEntities db=new RestaurantEntities())
            {
                var f = db.Masa.Find(masaID);
                db.Entry(f).State = EntityState.Detached;
                var x = f.ConvertToTableModel();
                x.MasaDurumu = (int)Common.masaDurumu.Boş;
               
                db.Entry(x.ConvertToMasa()).State = EntityState.Modified;

                //db.Masa.Attach(x.ConvertToMasa());
                //db.Entry(x.ConvertToMasa()).State = EntityState.Modified;
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
    }
}

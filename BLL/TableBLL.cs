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
    public static class TableBLL
    {
        public static TableModel GetTableByID(int ID)
        {
            return TableDAL.GetTableByID(ID).ConvertToTableModel();
        }
        public static List<TableModel>GetTables()
        {
            return TableDAL.GetTables();
        }
        public static bool SetTableBusy(int masaID)
        {
            return TableDAL.SetTableBusy(masaID);
        }
        public static bool SetTableFree(int masaID)
        {
            return TableDAL.SetTableFree(masaID);
        }

    }
}

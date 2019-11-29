using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Model
{
    public class OrderModel
    {
        public int SiparisID { get; set; }
        public int MasaID { get; set; }
        public int CalisanID { get; set; }
        public System.DateTime AlınmaZamani { get; set; }
        public int Durumu { get; set; }
        public Nullable<decimal> Tutari { get; set; }
        public bool isDeleted { get; set; }
        public EmployeeModel calisan = new EmployeeModel();
        public TableModel masa = new TableModel();
    }
}

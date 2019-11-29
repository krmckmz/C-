using Restaurant.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Model
{
    public class UserModel
    {
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public int Sifre { get; set; }
        public int CalisanID { get; set; }
        public bool OnlineMİ { get; set; }
        public bool isDeleted { get; set; }

        public EmployeeModel Calisan = new EmployeeModel();
    }
}

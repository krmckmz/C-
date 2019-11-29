using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Model
{
    public class FoodNavigateTableModel
    {
        public int KayitID { get; set; }
        public int SiparisID { get; set; }
        public int YemekID { get; set; }
        public OrderModel siparis = new OrderModel();
        public FoodModel yemek = new FoodModel();
    }
}

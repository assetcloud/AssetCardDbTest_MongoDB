using DB_Server;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_management.Models
{
    //Fixed_Assets_Model
    public class Fixed_Assets_Model: AssetBase_Model
    {
        //public string Specifications { set; get; } //guige
        //public string Storage_location { set; get; }//cunfangdidian 
        public SX_Model Specifications { set; get; }//规格
        public SX_Model Storage_location { set; get; }//存放地点
        
        public Fixed_Assets_Model()
        {
            Specifications = new SX_Model("规格");
            Storage_location = new SX_Model("存放地点");

        }
    }
}

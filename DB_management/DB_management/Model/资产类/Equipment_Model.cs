using DB_Server;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_management.Models
{
    public class Equipment_Model:Fixed_Assets_Model//设备
    {
        public List<SX_Model> KZ_dict { set; get; }//kuozhan
        public Equipment_Model()
        {
            SX_Model Device_ID = new SX_Model("设备号");//设备号
            SX_Model Power = new SX_Model("功率");//功率
            
            KZ_dict = new List<SX_Model>();
            KZ_dict.Add(Power);
            KZ_dict.Add(Device_ID);
        }
    }
}

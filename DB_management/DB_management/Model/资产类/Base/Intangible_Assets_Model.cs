using DB_Server;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_management.Models
{
    //无形资产模型
    public class Intangible_Assets_Model : AssetBase_Model
    {
        //public string Valuation_value { set; get; } //pinggujiazhi
        //public string Bearing_form { set; get; }//chengzaixingshi

        public SX_Model Valuation_value { set; get; }//评估价值
        public SX_Model Bearing_form { set; get; }//承载形式
        
        public Intangible_Assets_Model()
        {
            Valuation_value = new SX_Model("评估价值");
            Bearing_form = new SX_Model("承载形式");
        }
    }
}

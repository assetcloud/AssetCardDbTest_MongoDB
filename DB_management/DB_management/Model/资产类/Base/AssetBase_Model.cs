using DB_Server;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_management.Models
{
    public class AssetBase_Model
    {
        //此处有修改
        public SX_Model Asset_Coding { set; get; }
        public SX_Model Asset_Name { set; get; }
        public SX_Model Unit_of_measurement { set; get; }
        public SX_Model Book_Value { set; get; }
        public SX_Model User { set; get; }
        
        public AssetBase_Model()
        {
            Asset_Coding = new SX_Model("资产代码");
            Asset_Name = new SX_Model("资产名称");
            Unit_of_measurement = new SX_Model("计量单位");
            Book_Value = new SX_Model("账面价值");
            User = new SX_Model("使用人");
        }
    }
}

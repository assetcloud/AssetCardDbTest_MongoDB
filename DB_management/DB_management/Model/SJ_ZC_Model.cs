using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_management.Model
{
    [BsonIgnoreExtraElements]
    public class SJ_ZC_Model
    {
        public SJ_SX_Model Asset_Coding { set; get; }
        public SJ_SX_Model Asset_Name { set; get; }
        public SJ_SX_Model Unit_of_measurement { set; get; }
        public SJ_SX_Model Book_Value { set; get; }
        public SJ_SX_Model User { set; get; }
        public SJ_SX_Model Specifications { set; get; }
        public SJ_SX_Model Storage_location { set; get; }
        public List<SJ_SX_Model> KJ_SX { set; get; }

        public SJ_ZC_Model()
        {
            Asset_Coding = new SJ_SX_Model();
            Asset_Name = new SJ_SX_Model();
            Unit_of_measurement = new SJ_SX_Model();
            Book_Value = new SJ_SX_Model();
            User = new SJ_SX_Model();
            Specifications = new SJ_SX_Model();
            Storage_location = new SJ_SX_Model();
            KJ_SX = new List<SJ_SX_Model>();
        }
    }
}

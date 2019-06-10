using DB_Server;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_management.Models
{
    [BsonIgnoreExtraElements]
    public class SX_Model : YSX_Model
    {
        public string V { set; get; }
        public SX_Model(string Names)
        {
            M_Db_Operate m_Db_Operate = new M_Db_Operate();
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<YSX_Model>("Meta_attribute");

            List<string> key0 = new List<string>();
            key0.Add("Name");
            List<string> value0 = new List<string>();
            value0.Add(Names);
            var ysx0 = m_Db_Operate.Inquire_Data(collection, key0, value0);
            ID = ysx0[0].ID;
            Name = ysx0[0].Name;
            Length = ysx0[0].Length;
            Remarks = ysx0[0].Remarks;
            Unit = ysx0[0].Unit;
            Dimension_dic = ysx0[0].Dimension_dic;
            Edition = ysx0[0].Edition;
           // _id = ysx0[0]._id;
        }
        public SX_Model()
        {
        }
    }
}

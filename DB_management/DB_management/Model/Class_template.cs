using DB_Server;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_management.Model
{
    [BsonIgnoreExtraElements]
    public class Class_template
    {
        public List<string> KZ_SX { set; get; }
        public string TPYE { set; get; }
        public Class_template(String type)
        {
            KZ_SX = new List<string>();
            TPYE = type;
            M_Db_Operate m_Db_Operate = new M_Db_Operate();
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<Class_template>("Class_templates");

            List<string> key = new List<string>();
            List<string> value = new List<string>();
            key.Add("TPYE");
            value.Add(type);

            KZ_SX = m_Db_Operate.Inquire_Data(collection, key, value)[0].KZ_SX;
        }
        public Class_template()
        {
            KZ_SX = new List<string>();
        }
    }
}

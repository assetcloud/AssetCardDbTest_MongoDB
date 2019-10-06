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
        public List<string> ZC_SX { set; get; }   //扩展属性名称
        public string BM { set; get; }
        public string TPYE { set; get; }
        public string F_id { set; get; }
        public string bz { set; get; }
        public Class_template(String type)
        {
            ZC_SX = new List<string>();
            TPYE = type;
            M_Db_Operate m_Db_Operate = new M_Db_Operate();
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<Class_template>("Class_templates");

            List<string> key = new List<string>();
            List<string> value = new List<string>();
            key.Add("TPYE");
            value.Add(type);
            ZC_SX = m_Db_Operate.Inquire_Data(collection, key, value)[0].ZC_SX;
        }
        public void insert_c()
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<Class_template>("Class_templates");
            Class_template type = new Class_template();
            type.BM = "2";
            type.TPYE = "设备装备";
            collection.InsertOne(type);

        }
        public Class_template()
        {
            ZC_SX = new List<string>();
        }

        public static implicit operator Class_template(int v)
        {
            throw new NotImplementedException();
        }
    }
}

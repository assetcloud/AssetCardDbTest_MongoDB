using DB_Server;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_management.Model
{
    public class Class_templates
    {
        public string TPYE { set; get; }
        List<string> KZ_SX{ set; get; }
        public Class_templates(string TYPE)
        {
            TPYE = TYPE;
            KZ_SX = new List<string>();
            //从数据库中读取资产类别的元属性名称
            M_Db_Operate m_Db_Operate = new M_Db_Operate();
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<Class_templates>("Class_templates");
            List<string> key = new List<string>();
            key.Add("TPYE");
            List<string> value = new List<string>();
            value.Add(TPYE);
            //读取
            KZ_SX = m_Db_Operate.Inquire_Data(collection, key, value)[0].KZ_SX;
        }
        public Class_templates()
        {
            KZ_SX = new List<string>();
        }
    }
}

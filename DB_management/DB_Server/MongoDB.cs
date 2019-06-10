using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB_Server
{
    public class M_Db_Operate
    {
        public MongoClient client { get; set; }
        public IMongoDatabase database { get; set; }
        public M_Db_Operate()
        {
            client = new MongoClient("mongodb://127.0.0.1:27017");
            database = client.GetDatabase("test");
        }
        //插入信息
        public int Insert_Data<T>(IMongoCollection<T> collection, T document)
            {
                try
                {
                    collection.InsertOne(document);
                    return 0;
                }
                catch
                {
                    return -1;
                }
            }
        //查询信息
        public List<T> Inquire_Data<T>(IMongoCollection<T> collection,List<string> key = null, List<string> value = null, int type = 1)
        {
            try
            {
                List<T> result = new List<T>();
                switch (type)
                {
                    //1. 2.
                    case 1:
                        {
                            FilterDefinitionBuilder<T> builderFilter = Builders<T>.Filter;
                            //约束条件
                            List<FilterDefinition<T>> builderFilterss = new List<FilterDefinition<T>>();
                            for (int i = 0; i < key.Count; i++)
                                builderFilterss.Add(builderFilter.Eq(key[i], value[i]));
                            FilterDefinition<T> filter = builderFilter.And(builderFilterss.ToArray());
                            //获取数据
                            result = collection.Find(filter).ToList();
                            break;
                        }
                    case 2:
                        {
                            FilterDefinitionBuilder<T> builderFilter = Builders<T>.Filter;
                            result = collection.FindSync<T>(builderFilter.Empty).ToList();
                            break;
                        }
                }
                return result;
            }
            catch
            {
                return new List<T>();
            }
        }
    }
}

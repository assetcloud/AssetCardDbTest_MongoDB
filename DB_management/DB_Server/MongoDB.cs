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
        //删除信息
        public bool Del_Data<T>(IMongoCollection<T> collection,string key,string value)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq(key, value);   //匹配Name  为 “g” 的数据
                var result = collection.DeleteOne(filter);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //更新信息
        public bool Up_Data<T,D>(IMongoCollection<T> collection,string key , string value,string ZD, D now)
        {
            try
            {
                collection.UpdateMany(Builders<T>.Filter.Eq(key, value), Builders<T>.Update.Set(ZD, now));
            }
            catch
            {
                return false;
            }
            return true;
        }
        //关联查询查询数据
        public List<BsonDocument> Get_Aggregate(string coll, List<string> key, List<string> value, List<string> coll_name, List<string> localField, List<string> foreignField, List<string> ASS)
        {
            try
            {
                var oll = database.GetCollection<BsonDocument>(coll);
                IList<IPipelineStageDefinition> stages = new List<IPipelineStageDefinition>();
                string match = "{$match:{";
                for (int i = 0; i < key.Count; i++)
                {
                    match += "'" + key[i] + "':'" + value[i] + "',";
                }
                match += "}}";
                PipelineStageDefinition<BsonDocument, BsonDocument> stage1 =
                    new JsonPipelineStageDefinition<BsonDocument, BsonDocument>(match);
                stages.Add(stage1);
                for (int i = 0; i < localField.Count; i++)
                {
                    string pipelineJson = "{$lookup:{from: '"+ coll_name[i] + "',localField: '" +
                        localField[i] + "',foreignField: '" + foreignField[i] + "',as: '" + 
                        ASS[i] + "'}}";

                    PipelineStageDefinition<BsonDocument, BsonDocument> stage =
                        new JsonPipelineStageDefinition<BsonDocument, BsonDocument>(pipelineJson);
                    stages.Add(stage);
                }
                PipelineDefinition<BsonDocument, BsonDocument> pipeline = new PipelineStagePipelineDefinition<BsonDocument, BsonDocument>(stages);
                return oll.Aggregate(pipeline).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}

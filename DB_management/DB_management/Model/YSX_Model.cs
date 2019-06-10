using DB_Server;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_management.Models
{
    [BsonIgnoreExtraElements]
    public class YSX_Model
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Data_Type { get; set; }
        public string Unit { get; set; }
        public string Length { get; set; }
        public string Edition { get; set; }
        public string Remarks { get; set; }
        public Dictionary<string, string> Dimension_dic { get; set; }
        public YSX_Model(string flag)
        {
            Dimension_dic = new Dictionary<string, string>();
            //实际使用时从数据库读出
            Dimension_dic.Add("资产类别维度", "");
            Dimension_dic.Add("管理者维度", "");
            Dimension_dic.Add("应用特性维度", "");
            Dimension_dic.Add("自定义维度", "");
        }
        public YSX_Model()
        {
            Dimension_dic = new Dictionary<string, string>();
        }
    }
}

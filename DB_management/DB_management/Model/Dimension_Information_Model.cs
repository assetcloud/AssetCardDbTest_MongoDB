using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_management.Models
{
    [BsonIgnoreExtraElements]
    public class Dimension_Information_Model
    {
        public string ID { set; get; }
        public string NAME { set; get; }
        public string TYPE { set; get; }
        public string F_ID { set; get; }
        public string Remarks { set; get; }

    }
}

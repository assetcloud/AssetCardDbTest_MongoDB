using DB_Server;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_management.Models
{
    public class Cultural_Relic_Model : Fixed_Assets_Model
    {
        //public string Cultural_Relics_Grade { set; get; }
        //public string Collection_Age { set; get; }


        public List<SX_Model> KZ_dict { set; get; }//yuding
        public Cultural_Relic_Model()
        {

            SX_Model Grade = new SX_Model("文物等级");//文物等级
            SX_Model Collection_age = new SX_Model("藏品年代");//藏品年代

            KZ_dict = new List<SX_Model>();
            KZ_dict.Add(Collection_age);
            KZ_dict.Add(Grade);
        }
    }
}

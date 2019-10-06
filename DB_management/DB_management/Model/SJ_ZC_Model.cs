using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB_Server;
using DB_management.Models;
using MongoDB.Bson;

namespace DB_management.Model
{
    [BsonIgnoreExtraElements]
    public class SJ_ZC_Model
    {
        public string ZCBM { set; get; }                //资产编码
        public List<SJ_SX_Model> ZC_SX { set; get; }   //属性列表
        public string Remarks { set; get; }             //备注
        public SJ_ZC_Model()
        {
            ZC_SX = new List<SJ_SX_Model>();
        }
        public SJ_ZC_Model(BsonDocument res ,string type,int number)
        {
            Random ra = new Random();
            ZC_SX = new List<SJ_SX_Model>();
            M_Db_Operate YSX_db = new M_Db_Operate();
            var ysx_coll = YSX_db.database.GetCollection<YSX_Model>("Meta_attribute");
            int j = 0;
            ZCBM = number.ToString();
            while (true)
            {
                try
                {
                    SJ_SX_Model sx = new SJ_SX_Model();
                    sx.mp = res["ZC_SX"][j]["ID"].ToString();
                    if (sx.mp == "0002")
                    {
                        sx.V = type;
                        ZC_SX.Add(sx);
                        j++;
                        continue;
                    }
                    if (sx.mp == "0003")
                    {
                        sx.V = number.ToString();
                        ZC_SX.Add(sx);
                        j++;
                        continue;
                    }
                    if (sx.mp == "0004")
                    {
                        sx.V = type+ ra.Next(15).ToString();
                        ZC_SX.Add(sx);
                        j++;
                        continue;
                    }
                    if (res["ZC_SX"][j]["Name"].ToString() == "文物标识")
                    {
                        sx.V = ra.Next(2).ToString();
                        if (sx.V == "1")
                        {
                            var ww_ysx = YSX_db.Inquire_Data(ysx_coll, new List<string>() { "Dimension_dic.应用特性维度" }, new List<string> { "文物信息" });
                            foreach (var ww in ww_ysx)
                            {
                                SJ_SX_Model ww_s = new SJ_SX_Model();
                                if (ww.Data_Type == "整型")
                                    ww_s.V = ra.Next(1, 100).ToString();
                                else if (ww.Data_Type == "时间型")
                                    ww_s.V = Convert.ToDateTime("2007-1-1").AddDays(ra.Next(1000)).ToString();
                                else
                                    ww_s.V = ww.Name + ra.Next(1, 100).ToString();
                                ww_s.mp = ww.ID;
                                ZC_SX.Add(ww_s);
                            }
                        }
                    }
                    else if (res["ZC_SX"][j]["Name"].ToString() == "是否包含物联网传感")
                    {
                        sx.V = ra.Next(2).ToString();
                        if (sx.V == "1")
                        {
                            var wlw_ysx = YSX_db.Inquire_Data(ysx_coll, new List<string>() { "Dimension_dic.应用特性维度" }, new List<string> { "物联网信息" });
                            foreach (var wlw in wlw_ysx)
                            {
                                SJ_SX_Model wlw_s = new SJ_SX_Model();
                                if (wlw.Data_Type == "整型")
                                    wlw_s.V = ra.Next(1, 100).ToString();
                                else if (wlw.Data_Type == "时间型")
                                    wlw_s.V = Convert.ToDateTime("2007-1-1").AddDays(ra.Next(1000)).ToString();
                                else
                                    wlw_s.V = wlw.Name + ra.Next(1, 100).ToString();
                                wlw_s.mp = wlw.ID;
                                ZC_SX.Add(wlw_s);
                            }
                        }
                    }
                    else if (res["ZC_SX"][j]["Name"].ToString() == "陈列品标识")
                    {
                        sx.V = ra.Next(2).ToString();
                        if (sx.V == "1")
                        {
                            var bb_ysx = YSX_db.Inquire_Data(ysx_coll, new List<string>() { "Dimension_dic.应用特性维度" }, new List<string> { "标本信息" });
                            foreach (var bb in bb_ysx)
                            {
                                SJ_SX_Model bb_s = new SJ_SX_Model();
                                if (bb.Data_Type == "整型")
                                    bb_s.V = ra.Next(1, 100).ToString();
                                else if (bb.Data_Type == "时间型")
                                    bb_s.V = Convert.ToDateTime("2007-1-1").AddDays(ra.Next(1000)).ToString();
                                else
                                    bb_s.V = bb.Name + ra.Next(1, 100).ToString();
                                bb_s.mp = bb.ID;
                                ZC_SX.Add(bb_s);
                            }
                        }
                    }
                    else
                    {
                        if (res["ZC_SX"][j]["Data_Type"].ToString() == "整型")
                            sx.V = ra.Next(1, 100).ToString();
                        else if (res["ZC_SX"][j]["Data_Type"].ToString() == "时间型")
                            sx.V = Convert.ToDateTime("2007-1-1").AddDays(ra.Next(1000)).ToString();
                        else
                            sx.V = res["ZC_SX"][j]["Name"].ToString() + ra.Next(1, 100).ToString();
                    }
                    ZC_SX.Add(sx);
                    j++;
                }
                catch
                {
                    break;
                }
            }
        }
    }
}

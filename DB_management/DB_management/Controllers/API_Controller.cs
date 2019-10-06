using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB_management.Model;
using DB_management.Models;
using DB_Server;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DB_management.Views.Home
{
    public class API_Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]//查询资产类别的信息
        public List<List<string>> Get_Type(string type)
        {
            //从数据库获取类别信息并返回 获取到的元属性名称
            List<string> zy_name = new List<string>();
            List<string> kz_name = new List<string>();

            List<string> key = new List<string>();
            key.Add("ID");
            Class_template old = new Class_template(type);
            for (int i = 0; i < old.ZC_SX.Count; i++)
            {
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<YSX_Model>("Meta_attribute");
                List<string> value = new List<string>();
                value.Add(old.ZC_SX[i]);
                var resu = m_Db_Operate.Inquire_Data(collection, key, value);
                if (resu[0].Dimension_dic["资产类别维度"].Contains("专用集"))
                    zy_name.Add(resu[0].Name);
                else if (resu[0].Dimension_dic["资产类别维度"].Contains("扩展集"))
                    kz_name.Add(resu[0].Name);
            }
            List<List<string>> data = new List<List<string>>();
            data.Add(zy_name);
            data.Add(kz_name);
            return data;
        }
        [HttpGet]//获取已有类别下的类别名称
        public List<string> Get_Two_Type(string type)
        {
            //从数据库获取类别信息并返回 获取到的元属性名称
            List<string> data = new List<string>();
            List<string> key = new List<string>();
            List<string> value = new List<string>();
            key.Add("F_id");
            value.Add(type);
            M_Db_Operate m_Db_Operate = new M_Db_Operate();
            var collection = m_Db_Operate.database.GetCollection<Class_template>("Class_templates");
            var resule = m_Db_Operate.Inquire_Data(collection, key, value);
            for (int i = 0; i < resule.Count; i++)
            {
                data.Add(resule[i].TPYE);
            }
            return data;
        }
        [HttpGet]//根据资产类别获取该类别下全部维度名称
        public List<string> Get_WD_name(string Type)
        {
            List<string> name = new List<string>();
            try
            {
                List<string> key = new List<string>();
                List<string> value = new List<string>();
                key.Add("TYPE");
                value.Add(Type);
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");
                var resule = m_Db_Operate.Inquire_Data(collection, key, value);
                for (int i = 0; i < resule.Count; i++)
                {
                    name.Add(resule[i].NAME);
                }
            }
            catch
            {
                return null;
            }
            return name;
        }
        [HttpGet] //获取维度信息
        public Dimension_Information_Model Get_WD_XX(string Name)
        {
            try
            {
                List<string> key = new List<string>();
                List<string> value = new List<string>();
                key.Add("NAME");
                value.Add(Name);
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");
                var resule = m_Db_Operate.Inquire_Data(collection, key, value);
                return resule[0];
            }
            catch
            {
                return null;
            }
        }
        [HttpGet]//获取元属性信息
        public YSX_Model Get_YSX_XX(string Name, string Type)
        {
            M_Db_Operate m_Db_Operate = new M_Db_Operate();
            var collection = m_Db_Operate.database.GetCollection<YSX_Model>("Meta_attribute");

            List<string> key = new List<string>();
            List<string> value = new List<string>();
            key.Add("Dimension_dic." + "资产类别维度");
            value.Add(Type);
            key.Add("Name");
            value.Add(Name);
            var Result = m_Db_Operate.Inquire_Data(collection, key, value);
            return Result[0];
        }
        [HttpGet]//获取维度下全部元属性名称
        public Dictionary<string,string> Get_YSXS_Name(string WD, string Name)
        {

            M_Db_Operate m_Db_Operate = new M_Db_Operate();
            var collection = m_Db_Operate.database.GetCollection<YSX_Model>("Meta_attribute");

            List<string> key = new List<string>();
            List<string> value = new List<string>();
            key.Add("Dimension_dic." + WD);
            value.Add(Name);
            var Result = m_Db_Operate.Inquire_Data(collection, key, value);
            Dictionary<string, string> names = new Dictionary<string, string>();
            foreach (var i in Result)
                names.Add(i.ID,i.Name);
            return names;
        }
        [HttpGet]//获取一个类别下所有的资产名称和资产编码
        public List<List<string>> Get_ZC_NI(string Type,string Name = null,int n = 0)
        {
            try
            {
                List<string> key = new List<string>();
                List<string> value = new List<string>();
                if (Name != null)
                {
                    M_Db_Operate m_Db_Operate = new M_Db_Operate();
                    var collection = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");
                    FilterDefinitionBuilder<SJ_ZC_Model> builderFilters = Builders<SJ_ZC_Model>.Filter;
                    FilterDefinitionBuilder<SJ_SX_Model> builderFilter = Builders<SJ_SX_Model>.Filter;
                    var filter = builderFilters.ElemMatch("ZC_SX", builderFilter.And(builderFilter.Eq("V", Name), builderFilter.Eq("mp", "0004")));
                    var result = collection.Find(filter).ToList();
                    var s = from one in result
                            from sx in one.ZC_SX
                            where sx.mp == "0004"
                            select sx.V;
                    List<string> sn = s.GroupBy(p => p).Select(g => g.First()).ToList();
                    s = from one in result
                        from sx in one.ZC_SX
                        where sx.mp == "0003"
                        select sx.V;
                    List<string> si = new List<string>();
                    if (Name != null)
                        si = s.Distinct().ToList();
                    List<List<string>> resu = new List<List<string>>()
                    {
                        sn,si
                    };
                    return resu;
                }
                else
                {
                    M_Db_Operate m_Db_Operate = new M_Db_Operate();
                    var collection = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");
                    FilterDefinitionBuilder<SJ_ZC_Model> builderFilters = Builders<SJ_ZC_Model>.Filter;
                    FilterDefinitionBuilder<SJ_SX_Model> builderFilter = Builders<SJ_SX_Model>.Filter;
                    FilterDefinition<SJ_ZC_Model> filter = builderFilters.ElemMatch("ZC_SX", builderFilter.And(builderFilter.Eq("V", Type), builderFilter.Eq("mp", "0002")));
                    var result = collection.Find(filter).ToList();
                    var s = from one in result
                            from sx in one.ZC_SX
                            where sx.mp == "0004"
                            select sx.V;
                    List<string> sn = s.GroupBy(p => p).Select(g => g.First()).ToList();
                    s = from one in result
                        from sx in one.ZC_SX
                        where sx.mp == "0003"
                        select sx.V;
                    List<string> si = new List<string>();
                    if (Name != null)
                        si = s.Distinct().ToList();
                    List<List<string>> resu = new List<List<string>>()
                    {
                        sn,si
                    };
                    return resu;
                }
                //M_Db_Operate m_Db_Operate = new M_Db_Operate();
                //var collection = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");
                //FilterDefinitionBuilder<SJ_ZC_Model> builderFilter = Builders<SJ_ZC_Model>.Filter;
                ////约束条件
                //List<FilterDefinition<SJ_ZC_Model>> builderFilterss = new List<FilterDefinition<SJ_ZC_Model>>();
                //for (int i = 0; i < key.Count; i++)
                //    builderFilterss.Add(builderFilter.Eq(key[i], value[i]));
                //FilterDefinition<SJ_ZC_Model> filter = builderFilter.And(builderFilterss.ToArray());
                ////获取数据
                //var res = collection.Find(filter).Skip(n * 100).ToList();
                //var s = from one in res
                //         from sx in one.ZC_SX
                //         where sx.mp == "0004"
                //         select sx.V;
                //List<string> sn = s.GroupBy(p => p).Select(g => g.First()).ToList();
                //s = from one in res
                //    from sx in one.ZC_SX
                //    where sx.mp == "0003"
                //    select sx.V;
                //List<string> si = new List<string>();
                //if (Name != null)
                //    si = s.Distinct().ToList();
                //var res = m_Db_Operate.Inquire_Data(collection, key, value).Skip(1).Take(100).ToList();
                /*List<string> name = new List<string>();
                List<string> ID = new List<string>();
                for (int i = 0; i < res.Count; i++)
                {
                    for (int j = 0; j < res[i].ZC_SX.Count; j++)
                    {
                        if (res[i].ZC_SX[j].mp == "0004")
                            if (!name.Contains(res[i].ZC_SX[j].V))
                                name.Add(res[i].ZC_SX[j].V);
                        if(res[i].ZC_SX[j].mp == "0003")
                            ID.Add(res[i].ZC_SX[j].V);
                    }
                }*/
            }
            catch(Exception e)
            {
                return null;
            }
        }
        [HttpGet]//根据资产ID获取一个资产的全部信息
        public List<Dictionary<string, SJ_SX_Model>> Get_ZC_XX(string ID)
        {
            try
            {
                List<Dictionary<string, SJ_SX_Model>> resu = new List<Dictionary<string, SJ_SX_Model>>();
                Dictionary<string, SJ_SX_Model> TY_SX = new Dictionary<string, SJ_SX_Model>();
                Dictionary<string, SJ_SX_Model> ZY_SX = new Dictionary<string, SJ_SX_Model>();
                Dictionary<string, SJ_SX_Model> KZ_SX = new Dictionary<string, SJ_SX_Model>();
                Dictionary<string, SJ_SX_Model> GZ_WW = new Dictionary<string, SJ_SX_Model>();
                Dictionary<string, SJ_SX_Model> GZ_WLW = new Dictionary<string, SJ_SX_Model>();
                Dictionary<string, SJ_SX_Model> GZ_BB = new Dictionary<string, SJ_SX_Model>();
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");

                var result = m_Db_Operate.Get_Aggregate(
                    "ZC_data",
                    new List<string>() { "ZCBM" },
                    new List<string>() { ID },
                    new List<string>() { "Meta_attribute" },
                    new List<string>() { "ZC_SX.mp"},
                    new List<string>() { "ID"},
                    new List<string>() { "ZC_SXN"}
                    )[0];
                int j = 0;
                while (true)
                {
                    try
                    {
                        SJ_SX_Model sJ_SX_ = new SJ_SX_Model();
                        sJ_SX_.mp = result["ZC_SXN"][j]["ID"].ToString();
                        int u = 0;
                        while (true)
                        {
                            try
                            {
                                if (result["ZC_SX"][u]["mp"].ToString() == result["ZC_SXN"][j]["ID"].ToString())
                                {
                                    sJ_SX_.V = result["ZC_SX"][u]["V"].ToString();
                                    break;
                                }
                                u++;
                            }
                            catch
                            {
                                break;
                            }
                        }
                        if (result["ZC_SXN"][j]["Dimension_dic"]["资产类别维度"].ToString().Contains("通用集"))
                        {
                            TY_SX.Add(result["ZC_SXN"][j]["Name"].ToString(), sJ_SX_);
                        }
                        else if (result["ZC_SXN"][j]["Dimension_dic"]["资产类别维度"].ToString().Contains("专用集"))
                        {
                            ZY_SX.Add(result["ZC_SXN"][j]["Name"].ToString(), sJ_SX_);
                        }
                        else if (result["ZC_SXN"][j]["Dimension_dic"]["资产类别维度"].ToString().Contains("扩展集"))
                        {
                            if (result["ZC_SXN"][j]["Dimension_dic"]["应用特性维度"] == "文物信息")
                                GZ_WW.Add(result["ZC_SXN"][j]["Name"].ToString(), sJ_SX_);
                            else if(result["ZC_SXN"][j]["Dimension_dic"]["应用特性维度"] == "物联网信息")
                                GZ_WLW.Add(result["ZC_SXN"][j]["Name"].ToString(), sJ_SX_);
                            else if(result["ZC_SXN"][j]["Dimension_dic"]["应用特性维度"] == "标本信息")
                                GZ_BB.Add(result["ZC_SXN"][j]["Name"].ToString(), sJ_SX_);
                            else
                                KZ_SX.Add(result["ZC_SXN"][j]["Name"].ToString(), sJ_SX_);
                        }
                        j++;
                    }
                    catch
                    {
                        break;
                    }
                }
                resu.Add(ZY_SX);
                resu.Add(KZ_SX);
                resu.Add(TY_SX);
                resu.Add(GZ_WW);
                resu.Add(GZ_WLW);
                resu.Add(GZ_BB);
                return resu;
            }
            catch
            {
                return null;
            }
        }
    }
}
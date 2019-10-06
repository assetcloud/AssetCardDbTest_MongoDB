using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB_management.Model;
using DB_management.Models;
using DB_Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoRepository;

namespace DB_management.Controllers
{
    public class ZC_Controller : Controller
    {
        public List<string> ty_names = new List<string>() {
            "上级资产分类编码","上级资产分类名称","资产编码","资产类别名称","资产英文名称","数量","计量单位","账面价值",
            "价值类型","币种","国别","取得日期","开始使用日期","用途","使用状况","使用方向",
            "取得方式","会计凭证号","使用部门","使用人","备注"
        };
        public List<string> sx_name = new List<string>() {
            "上级资产分类编码","上级资产分类名称","资产编码","资产类别名称","资产英文名称","数量","计量单位","账面价值",
            "价值类型","币种","国别","取得日期","开始使用日期","用途","数量","使用状况","使用方向",
            "取得方式","会计凭证号","使用部门","使用人","备注","专用集","扩展集"
        };
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult add()
        {
            ViewData["ty_names"] = ty_names;
            return View();
        }
        //TODO:资产查询界面
        public IActionResult query_zc()
        {
            M_Db_Operate m_Db_Operate = new M_Db_Operate();

            //var zc_coll = m_Db_Operate.database.GetCollection<BsonDocument>("ZC_data");
            //string pipelineJson1 = "{$match:{'ZCBM.V':'1',}}";
            //string pipelineJson2 = "{$lookup:{from: 'Meta_attribute',localField: 'ZY_SX.mp',foreignField: 'ID',as: 'ZY_SX_N'}}";
            //string pipelineJson3 = "{$lookup:{from: 'Meta_attribute',localField: 'KJ_SX.mp',foreignField: 'ID',as: 'KJ_SX_N'}}";
            //IList<IPipelineStageDefinition> stages = new List<IPipelineStageDefinition>();
            //PipelineStageDefinition<BsonDocument, BsonDocument> stage1 =
            //    new JsonPipelineStageDefinition<BsonDocument, BsonDocument>(pipelineJson1);
            //PipelineStageDefinition<BsonDocument, BsonDocument> stage2 =
            //    new JsonPipelineStageDefinition<BsonDocument, BsonDocument>(pipelineJson2);
            //PipelineStageDefinition<BsonDocument, BsonDocument> stage3 =
            //    new JsonPipelineStageDefinition<BsonDocument, BsonDocument>(pipelineJson3);
            ////stages.Add(stage1);
            //stages.Add(stage2);
            //stages.Add(stage3);
            //PipelineDefinition<BsonDocument, BsonDocument> pipeline = new PipelineStagePipelineDefinition<BsonDocument, BsonDocument>(stages);
            //var result = zc_coll.Aggregate(pipeline).ToList();

            List<List<string>> ZC_datas = new List<List<string>>();
            //for (int i = 0; i < result.Count; i++)
            //{
            //    List<string> ZC_data = new List<string>();
            //    ZC_data.Add(sx_name[0] + ":" + result[i]["SJZCFLBM"]["V"].ToString());
            //    ZC_data.Add(sx_name[1] + ":" + result[i]["SJZCFLMC"]["V"].ToString());
            //    ZC_data.Add(sx_name[2] + ":" + result[i]["ZCBM"]["V"].ToString());
            //    ZC_data.Add(sx_name[3] + ":" + result[i]["ZCMC"]["V"].ToString());
            //    ZC_data.Add(sx_name[4] + ":" + result[i]["YWMC"]["V"].ToString());
            //    ZC_data.Add(sx_name[5] + ":" + result[i]["SL"]["V"].ToString());
            //    ZC_data.Add(sx_name[6] + ":" + result[i]["JLDW"]["V"].ToString());
            //    ZC_data.Add(sx_name[7] + ":" + result[i]["ZMJZ"]["V"].ToString());
            //    ZC_data.Add(sx_name[8] + ":" + result[i]["JZLX"]["V"].ToString());
            //    ZC_data.Add(sx_name[9] + ":" + result[i]["BiZ"]["V"].ToString());
            //    ZC_data.Add(sx_name[10] + ":" + result[i]["GB"]["V"].ToString());
            //    ZC_data.Add(sx_name[11] + ":" + result[i]["QDRQ"]["V"].ToString());
            //    ZC_data.Add(sx_name[12] + ":" + result[i]["KSKYRQ"]["V"].ToString());
            //    ZC_data.Add(sx_name[13] + ":" + result[i]["YT"]["V"].ToString());
            //    ZC_data.Add(sx_name[14] + ":" + result[i]["SYZK"]["V"].ToString());
            //    ZC_data.Add(sx_name[15] + ":" + result[i]["SYFX"]["V"].ToString());
            //    ZC_data.Add(sx_name[16] + ":" + result[i]["QDFS"]["V"].ToString());
            //    ZC_data.Add(sx_name[17] + ":" + result[i]["KJPZH"]["V"].ToString());
            //    ZC_data.Add(sx_name[18] + ":" + result[i]["SYBM"]["V"].ToString());
            //    ZC_data.Add(sx_name[19] + ":" + result[i]["SYR"]["V"].ToString());
            //    ZC_data.Add(sx_name[20] + ":" + result[i]["BZ"]["V"].ToString());
            //    string kz_sx = "";
            //    var j = 0;
            //    while (true)
            //    {
            //        try
            //        {
            //            var name1 = result[i]["ZY_SX_N"][j]["Name"].ToString();
            //            var Value = result[0]["ZY_SX"][j]["V"].ToString();
            //            kz_sx += name1 + " : " + Value + "     ";
            //            j++;
            //        }
            //        catch
            //        {
            //            break;
            //        }
            //    }
            //    j = 0;
            //    while (true)
            //    {
            //        try
            //        {
            //            var name1 = result[i]["KJ_SX_N"][j]["Name"].ToString();
            //            var Value = result[0]["KJ_SX"][j]["V"].ToString();
            //            kz_sx += name1 + " : " + Value + "     ";
            //            j++;
            //        }
            //        catch
            //        {
            //            break;
            //        }
            //    }
            //    ZC_data.Add(kz_sx);
            //    ZC_datas.Add(ZC_data);
            //}
            //查询管理者维度
            var collection1 = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");

            List<string> key = new List<string>();
            key.Add("TYPE");
            List<string> value = new List<string>();
            value.Add("管理者维度");
            var Result = m_Db_Operate.Inquire_Data(collection1, key, value);
            List<string> name = new List<string>();
            for (var i = 0; i < Result.Count; i++)
            {
                name.Add(Result[i].NAME);
            }
            ViewData["glz_list"] = name;
            ViewData["ZC_datas"] = ZC_datas;
            ViewData["sx_name"] = sx_name;
            return View("query_zc");
        }
        //按照指定的元属性值查找
        public IActionResult query_zc_1(IFormCollection f)
        {
            var ysx_name = f["ysx_name"];
            var ysx_value = f["ysx_value"];
            try
            {
                //查询管理者维度

                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection1 = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");

                List<string> key1 = new List<string>();
                key1.Add("TYPE");
                List<string> value1 = new List<string>();
                value1.Add("管理者维度");
                var Result = m_Db_Operate.Inquire_Data(collection1, key1, value1);
                List<string> name = new List<string>();
                for (var i = 0; i < Result.Count; i++)
                {
                    name.Add(Result[i].NAME);
                }
                ViewData["glz_list"] = name;
                //在这里查询数据库并整合数据

                M_Db_Operate m_Db_ = new M_Db_Operate();
                var coll = m_Db_.database.GetCollection<YSX_Model>("Meta_attribute");
                var res = m_Db_.Inquire_Data(coll, new List<string> { "Name" }, new List<string> { ysx_name.ToString() })[0];
                //var collection = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");
                //List<string> key = new List<string>();
                //List<string> value = new List<string>();
                //key.Add("ZC_SX.mp");
                //key.Add("ZC_SX.V");
                //value.Add(res.ID);
                //value.Add(ysx_value.ToString());
                //List <BsonDocument> result = m_Db_Operate.Get_Aggregate(
                //    "ZC_data",
                //    key,
                //    value,
                //    new List<string>() { "Meta_attribute" },
                //    new List<string>() { "ZC_SX.mp" },
                //    new List<string>() { "ID" },
                //    new List<string>() { "ZC_SXN" }
                //    ); 
                #region 聚合查询方法
                IList<IPipelineStageDefinition> stages = new List<IPipelineStageDefinition>();
                string match = "{$match:{ 'ZC_SX':{ 'V':'" + ysx_value.ToString() + "','mp':'" + res.ID + "'}}}";
                string lookup = "{$lookup:{from: 'Meta_attribute',localField: 'ZC_SX.mp',foreignField: 'ID', as: 'ZC_SXN'}}";
                string skip = "{$skip: 0}";
                string limit = "{$limit:30}";
                PipelineStageDefinition<BsonDocument, BsonDocument> stage1 =
                    new JsonPipelineStageDefinition<BsonDocument, BsonDocument>(match);
                PipelineStageDefinition<BsonDocument, BsonDocument> stage2 =
                    new JsonPipelineStageDefinition<BsonDocument, BsonDocument>(lookup);
                PipelineStageDefinition<BsonDocument, BsonDocument> stage3 =
                    new JsonPipelineStageDefinition<BsonDocument, BsonDocument>(skip);
                PipelineStageDefinition<BsonDocument, BsonDocument> stage4 =
                    new JsonPipelineStageDefinition<BsonDocument, BsonDocument>(limit);
                stages.Add(stage1);
                stages.Add(stage2);
                stages.Add(stage3);
                stages.Add(stage4);
                PipelineDefinition<BsonDocument, BsonDocument> pipeline = new PipelineStagePipelineDefinition<BsonDocument, BsonDocument>(stages);
                var oll = m_Db_Operate.database.GetCollection<BsonDocument>("ZC_data");
                List <BsonDocument> result = oll.Aggregate(pipeline).ToList();

                List<List<string>> ZC_datas = new List<List<string>>();
                for (var i = 0; i <result.Count; i++)
                {
                    List<string> ZC_data = new List<string>();
                    string kz_sx = "";
                    int j = 0;
                    while (true)
                    {
                        try
                        {
                            if (result[i]["ZC_SXN"][j]["Dimension_dic"]["资产类别维度"].ToString().Contains("通用集"))
                            {
                                string uu = result[i]["ZC_SXN"][j]["Name"].ToString() + ":";
                                int k = 0;
                                while (true)
                                {
                                    try
                                    {
                                        if (result[i]["ZC_SXN"][j]["ID"].ToString() == result[i]["ZC_SX"][k]["mp"].ToString())
                                        {
                                            uu += result[i]["ZC_SX"][k]["V"].ToString();
                                            break;
                                        }
                                        k++;
                                    }
                                    catch
                                    {
                                        ;
                                    }
                                }
                                ZC_data.Add(uu);
                            }
                            else
                            {
                                string uu = result[i]["ZC_SXN"][j]["Name"].ToString() + ":";
                                int k = 0;
                                while (true)
                                {
                                    try
                                    {
                                        if (result[i]["ZC_SXN"][j]["ID"].ToString() == result[i]["ZC_SX"][k]["mp"].ToString())
                                        {
                                            uu += result[i]["ZC_SX"][k]["V"].ToString();
                                            break;
                                        }
                                        k++;
                                    }
                                    catch
                                    {
                                        ;
                                    }
                                }
                                ZC_data.Add(uu);
                            }

                        }
                        catch
                        {
                            break;
                        }
                        j++;
                    }
                    ZC_data.Add(kz_sx);
                    ZC_datas.Add(ZC_data);
                }
                #endregion
                #region 其他方法
                //var collection = m_Db_Operate.database.GetCollection<BsonDocument>("ZC_data");
                //FilterDefinitionBuilder<BsonDocument> builderFilter = Builders<BsonDocument>.Filter;
                //var filter = builderFilter.ElemMatch("ZC_SX", builderFilter.And(builderFilter.Eq("V", ysx_value.ToString()), builderFilter.Eq("mp", res.ID)));
                //var result = collection.Find(filter).ToList();
                //List <List<string>> ZC_datas = new List<List<string>>();
                //FilterDefinitionBuilder<YSX_Model> builderFilter1 = Builders<YSX_Model>.Filter;
                //FilterDefinition<YSX_Model> filt = builderFilter1.And(builderFilter1.Ne("ID", ""));
                //var res2 = coll.Find(filt).ToList();
                //for (var i = 0; i < result.Count; i++)
                //{
                //    List<string> ZC_data = new List<string>();
                //    string kz_sx = "";
                //    int j = 0;
                //    while (true)
                //    {
                //        try
                //        {
                //            YSX_Model q = res2.TakeWhile(x => x.ID == result[i]["ZC_SX"][j]["mp"].ToString()).First();
                //            if (q.Dimension_dic["资产类别维度"].ToString().Contains("通用集"))
                //            {
                //                ZC_data.Add(q.Name.ToString() + ":" + result[i]["ZC_SX"][j]["V"].ToString());
                //            }
                //            else
                //            {
                //                kz_sx += q.Name.ToString() + ":" + result[i]["ZC_SX"][j]["V"].ToString();
                //            }
                //        }
                //        catch
                //        {
                //            break;
                //        }
                //        j++;
                //    }
                //    ZC_data.Add(kz_sx);
                //    ZC_datas.Add(ZC_data);
                //}
                #endregion
                ViewData["ZC_datas"] = ZC_datas;
                ViewData["sx_name"] = sx_name;

            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("query_ERROR");
            }

            ViewData["wd_type"] = null;
            ViewData["wd_xx"] = null;
            ViewData["zc_name"] = null;
            ViewData["ysx_name"] = ysx_name;
            ViewData["ysx_value"] = ysx_value;

            return View("query_zc");
        }
        //按照资产编码查找
        public IActionResult query_zc_2(IFormCollection f)
        {
            string zc_name = f["zc_name"];
            try
            {
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection1 = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");

                List<string> key1 = new List<string>();
                key1.Add("TYPE");
                List<string> value1 = new List<string>();
                value1.Add("管理者维度");
                var Result = m_Db_Operate.Inquire_Data(collection1, key1, value1);
                List<string> name = new List<string>();
                for (var i = 0; i < Result.Count; i++)
                {
                    name.Add(Result[i].NAME);
                }
                ViewData["glz_list"] = name;
                //在这里查询数据库并整合数据

                var collection = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");
                List<string> key = new List<string>();
                List<string> value = new List<string>();
                key.Add("ZCBM");
                value.Add(zc_name.ToString());

                var result = m_Db_Operate.Get_Aggregate(
                    "ZC_data",
                    key,
                    value,
                    new List<string>() { "Meta_attribute"},
                    new List<string>() { "ZC_SX.mp" },
                    new List<string>() { "ID" },
                    new List<string>() { "ZC_SXN" }
                    );
                List<List<string>> ZC_datas = new List<List<string>>();
                for (var i = 0; i < result.Count; i++)
                {
                    List<string> ZC_data = new List<string>();
                    string kz_sx = "";
                    int j = 0;
                    while (true)
                    {
                        try
                        {
                            if (result[i]["ZC_SXN"][j]["Dimension_dic"]["资产类别维度"].ToString().Contains("通用集"))
                            {
                                string uu = result[i]["ZC_SXN"][j]["Name"].ToString() + ":";
                                int k = 0;
                                while (true)
                                {
                                    try
                                    {
                                        if (result[i]["ZC_SXN"][j]["ID"].ToString() == result[i]["ZC_SX"][k]["mp"].ToString())
                                        {
                                            uu += result[i]["ZC_SX"][k]["V"].ToString();
                                            break;
                                        }
                                        k++;
                                    }
                                    catch
                                    {
                                        ;
                                    }
                                }
                                ZC_data.Add(uu);
                            }
                            else
                            {

                                string uu = result[i]["ZC_SXN"][j]["Name"].ToString() + ":";
                                int k = 0;
                                while (true)
                                {
                                    try
                                    {
                                        if (result[i]["ZC_SXN"][j]["ID"].ToString() == result[i]["ZC_SX"][k]["mp"].ToString())
                                        {
                                            uu += result[i]["ZC_SX"][k]["V"].ToString();
                                            break;
                                        }
                                        k++;
                                    }
                                    catch
                                    {
                                        ;
                                    }
                                }
                                ZC_data.Add(uu);
                            }

                        }
                        catch
                        {
                            break;
                        }
                        j++;
                    }
                    ZC_data.Add(kz_sx);
                    ZC_datas.Add(ZC_data);
                }
                ViewData["ZC_datas"] = ZC_datas;
                ViewData["sx_name"] = sx_name;
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("query_ERROR");
            }

            ViewData["wd_type"] = null;
            ViewData["wd_xx"] = null;
            ViewData["zc_name"] = null;
            ViewData["ysx_name"] = null;
            ViewData["ysx_value"] = null;

            return View("query_zc");
        }
        //按照管理者维度查找
        //public IActionResult query_zc_3(IFormCollection f)
        //{
        //    string guanlizhe = f["glz"];
        //    try
        //    {
        //        M_Db_Operate m_Db_Operate = new M_Db_Operate();
        //        var collection1 = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");

        //        List<string> key1 = new List<string>();
        //        key1.Add("TYPE");
        //        List<string> value1 = new List<string>();
        //        value1.Add("管理者维度");
        //        var Result1 = m_Db_Operate.Inquire_Data(collection1, key1, value1);
        //        List<string> name = new List<string>();
        //        for (var i = 0; i < Result1.Count; i++)
        //        {
        //            name.Add(Result1[i].NAME);
        //        }
        //        ViewData["glz_list"] = name;
        //        //在这里查询数据库并整合数据
        //        var collection = m_Db_Operate.database.GetCollection<YSX_Model>("Meta_attribute");

        //        List<string> key = new List<string>();
        //        key.Add("Dimension_dic.管理者维度");
        //        List<string> value = new List<string>();
        //        value.Add(guanlizhe);
        //        var Result = m_Db_Operate.Inquire_Data(collection, key, value);

        //        List<string> results = new List<string>();
        //        for (var i = 0; i < Result.Count; i++)
        //        {
        //            results.Add(Result[i].Name);
        //        }
        //        List<string> value2 = new List<string>();
        //        value2.Add("默认管理者");
        //        Result = m_Db_Operate.Inquire_Data(collection, key, value2);
        //        for (var i = 0; i < Result.Count; i++)
        //        {
        //            results.Add(Result[i].Name);
        //        }
        //        var collection2 = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");
        //        FilterDefinitionBuilder<SJ_ZC_Model> builderFilter = Builders<SJ_ZC_Model>.Filter;
        //        var result = collection2.FindSync<SJ_ZC_Model>(builderFilter.Empty).ToList();

        //        List<List<string>> ZC_datas = new List<List<string>>();
        //        for (var i = 0; i < result.Count; i++)
        //        {
        //            List<string> ZC_data = new List<string>();
        //            ZC_data.Add(sx_name[0] + ":" +result[i].SJZCFLBM.V.ToString());
        //            ZC_data.Add(sx_name[1] + ":" +result[i].SJZCFLMC.V.ToString());
        //            ZC_data.Add(sx_name[2] + ":" +result[i].ZCBM.V.ToString());
        //            ZC_data.Add(sx_name[3] + ":" +result[i].ZCMC.V.ToString());
        //            ZC_data.Add(sx_name[4] + ":" +result[i].YWMC.V.ToString());
        //            ZC_data.Add(sx_name[5] + ":" +result[i].SL.V.ToString());
        //            ZC_data.Add(sx_name[6] + ":" +result[i].JLDW.V.ToString());
        //            ZC_data.Add(sx_name[7] + ":" +result[i].ZMJZ.V.ToString());
        //            ZC_data.Add(sx_name[8] + ":" +result[i].JZLX.V.ToString());
        //            ZC_data.Add(sx_name[9] + ":" +result[i].BiZ.V.ToString());
        //            ZC_data.Add(sx_name[10] + ":" +result[i].GB.V.ToString());
        //            ZC_data.Add(sx_name[11] + ":" +result[i].QDRQ.V.ToString());
        //            ZC_data.Add(sx_name[12] + ":" +result[i].KSKYRQ.V.ToString());
        //            ZC_data.Add(sx_name[13] + ":" +result[i].YT.V.ToString());
        //            ZC_data.Add(sx_name[14] + ":" +result[i].SYZK.V.ToString());
        //            ZC_data.Add(sx_name[15] + ":" +result[i].SYFX.V.ToString());
        //            ZC_data.Add(sx_name[16] + ":" +result[i].QDFS.V.ToString());
        //            ZC_data.Add(sx_name[17] + ":" +result[i].KJPZH.V.ToString());
        //            ZC_data.Add(sx_name[18] + ":" +result[i].SYBM.V.ToString());
        //            ZC_data.Add(sx_name[19] + ":" +result[i].SYR.V.ToString());
        //            ZC_data.Add(sx_name[20] + ":" +result[i].BZ.V.ToString());
        //            string kz_sx = "";
        //            M_Db_Operate m_Db_ = new M_Db_Operate();
        //            var coll = m_Db_.database.GetCollection<YSX_Model>("Meta_attribute");
        //            List<string> key2 = new List<string>();
        //            key2.Add("ID");
        //            for (var j = 0; j < result[i].ZY_SX.Count; j++)
        //            {
        //                List<string> value3 = new List<string>();
        //                value3.Add(result[i].ZY_SX[j].mp.ToString());
        //                var su = m_Db_.Inquire_Data(coll, key2, value3);
        //                kz_sx += su[0].Name.ToString() + " : " + result[i].ZY_SX[j].V.ToString() + "     ";
        //            }
        //            for (var j = 0; j < result[i].KJ_SX.Count; j++)
        //            {
        //                List<string> value3 = new List<string>();
        //                value3.Add(result[i].KJ_SX[j].mp.ToString());
        //                var su = m_Db_.Inquire_Data(coll, key2, value3);
        //                kz_sx += su[0].Name.ToString() + " : " + result[i].KJ_SX[j].V.ToString() + "     ";
        //            }
        //            ZC_data.Add(kz_sx);
        //            ZC_datas.Add(ZC_data);
        //        }
        //        ViewData["ZC_datas"] = ZC_datas;
        //        ViewData["sx_name"] = sx_name;
        //    }
        //    catch (InvalidCastException e)
        //    {
        //        ViewData["e"] = e.ToString();
        //        return View("query_ERROR");
        //    }
        //    ViewData["glz"] = guanlizhe;
        //    ViewData["zc_name"] = null;
        //    ViewData["ysx_name"] = null;
        //    ViewData["ysx_value"] = null;
        //    return View("query_zc");
        //}

        //添加资产完成动作
        public IActionResult addZC_OK(IFormCollection f)
        {
            string Kindda, One_Class;
            Kindda = f["Kindda"];
            One_Class = f["One_Class"];
            //Two_Class = f["Two_Class"];
            SJ_ZC_Model SJ_ZC = new SJ_ZC_Model();
            for (int i = 1; i < ty_names.Count; i++)
            {
                SJ_SX_Model sJ_SX_ = new SJ_SX_Model();
                sJ_SX_.V = f[ty_names[i-1]];
                if (i > 9)
                    sJ_SX_.mp = "00" + i.ToString();
                else
                    sJ_SX_.mp = "000" + i.ToString();
                SJ_ZC.ZC_SX.Add(sJ_SX_);
            }
            SJ_ZC.ZCBM = f["资产编码"];
            Class_template old = new Class_template();
            if (One_Class == "不选择--")
            {
                old = new Class_template(Kindda);
            }
            else
            {
                old = new Class_template(One_Class);
            }
            List<string> key = new List<string>();
            key.Add("ID");
            M_Db_Operate m_Db_Operate = new M_Db_Operate();
            var collection = m_Db_Operate.database.GetCollection<YSX_Model>("Meta_attribute");
            for (var i = 1; i < old.ZC_SX.Count; i++)
            {
                SJ_SX_Model sj_sx = new SJ_SX_Model();
                List<string> value = new List<string>();
                value.Add(old.ZC_SX[i].ToString());
                var resu = m_Db_Operate.Inquire_Data(collection, key, value);
                sj_sx.V = f[resu[0].Name];
                sj_sx.mp = resu[0].ID.ToString();
                SJ_ZC.ZC_SX.Add(sj_sx);
                if (resu[0].Name == "文物标识")
                {
                    var re_ww = m_Db_Operate.Inquire_Data(collection, new List<string> { "Dimension_dic.应用特性维度"}, new List<string> { "文物信息"});
                    for (int j = 0; j < re_ww.Count; j++)
                    {
                        sj_sx = new SJ_SX_Model();
                        sj_sx.V = f[re_ww[j].Name];
                        sj_sx.mp = re_ww[j].ID.ToString();
                        SJ_ZC.ZC_SX.Add(sj_sx);
                    }
                }
                else if (resu[0].Name == "是否包含物联网传感")
                {
                    var re_ww = m_Db_Operate.Inquire_Data(collection, new List<string> { "Dimension_dic.应用特性维度" }, new List<string> { "物联网信息" });
                    for (int j = 0; j < re_ww.Count; j++)
                    {
                        sj_sx = new SJ_SX_Model();
                        sj_sx.V = f[re_ww[j].Name];
                        sj_sx.mp = re_ww[j].ID.ToString();
                        SJ_ZC.ZC_SX.Add(sj_sx);
                    }
                }
                else if (resu[0].Name == "陈列品标识")
                {
                    var re_ww = m_Db_Operate.Inquire_Data(collection, new List<string> { "Dimension_dic.应用特性维度" }, new List<string> { "标本信息" });
                    for (int j = 0; j < re_ww.Count; j++)
                    {
                        sj_sx = new SJ_SX_Model();
                        sj_sx.V = f[re_ww[j].Name];
                        sj_sx.mp = re_ww[j].ID.ToString();
                        SJ_ZC.ZC_SX.Add(sj_sx);
                    }
                }
            }
            try
            {
                M_Db_Operate m_Db = new M_Db_Operate();
                var coll = m_Db.database.GetCollection<SJ_ZC_Model>("ZC_data");
                coll.InsertOne(SJ_ZC);
                return View("../Home/add_OK");
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("../Home/add_ERROR");
            };
        }
        //资产删除页面
        public IActionResult Del_zc()
        {
            return View();
        }
        //资产删除完成动作 
        public IActionResult Del_zc_OK(IFormCollection f)
        {
            try
            {
                string id;
                if (f["zc_id"] == "-- 资产编码 --")
                    id = f["int_zc_"];
                else
                    id = f["zc_id"];
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var coll = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");
                if (!m_Db_Operate.Del_Data(coll, "ZCBM", id))
                    return View("../Home/add_ERROR");
                return View("../Home/add_OK");
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("../Home/add_ERROR");
            };
        }
        //资产修改页面
        public IActionResult Updata_zc()
        {
            return View();
        }
        //资产修改完成动作result[0].SJZCFLBM
        public IActionResult Updata_zc_OK(IFormCollection f)
        {
            try
            {
                string id;
                if (f["zc_id"] == "-- 资产编码 --")
                    id = f["int_zc_"];
                else
                    id = f["zc_id"];
                List<SJ_SX_Model> new_ZC_SX = new List<SJ_SX_Model>();
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var coll = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");
                var result = m_Db_Operate.Get_Aggregate(
                    "ZC_data",
                    new List<string>() { "ZCBM"},
                    new List<string>() { id },
                    new List<string>() { "Meta_attribute" },
                    new List<string>() { "ZC_SX.mp" },
                    new List<string>() { "ID" },
                    new List<string>() { "ZC_SXN" }
                    )[0];
                M_Db_Operate m_Db = new M_Db_Operate();
                var collection = m_Db.database.GetCollection<YSX_Model>("Meta_attribute");
                int j = 0;
                int[] flag = { 1, 1, 1 };
                List<SJ_SX_Model> del_sx = new List<SJ_SX_Model>();
                while (true)
                {
                    try
                    {
                        SJ_SX_Model sx = new SJ_SX_Model();
                        sx.V = f[result["ZC_SXN"][j]["ID"].ToString()];
                        int u = 0;
                        while (true)
                        {
                            try
                            {
                                if (result["ZC_SX"][u]["mp"].ToString() == result["ZC_SXN"][j]["ID"].ToString())
                                {
                                    sx.mp = result["ZC_SX"][u]["mp"].ToString();
                                    break;
                                }
                                u++;
                            }
                            catch
                            {
                                break;
                            }
                        }
                        //sx.mp = result["ZC_SX"][j]["mp"].ToString();
                        new_ZC_SX.Add(sx);
                        if (result["ZC_SXN"][j]["Name"].ToString() == "文物标识")
                        {
                            if (sx.V == "1")
                            {
                                var ww_resu = m_Db.Inquire_Data(collection, new List<string>() { "Dimension_dic.应用特性维度" }, new List<string>() { "文物信息" });
                                foreach (var ysx in ww_resu)
                                {
                                    SJ_SX_Model ww_sx = new SJ_SX_Model();
                                    ww_sx.V = f[ysx.ID];
                                    ww_sx.mp = ysx.ID;
                                    new_ZC_SX.Add(ww_sx);
                                }
                            }
                            if (sx.V == "0")
                            {
                                var ww_resu = m_Db.Inquire_Data(collection, new List<string>() { "Dimension_dic.应用特性维度" }, new List<string>() { "文物信息" });
                                foreach (var ysx in ww_resu)
                                {
                                    SJ_SX_Model ww_sx = new SJ_SX_Model();
                                    ww_sx.V = f[ysx.ID];
                                    ww_sx.mp = ysx.ID;
                                    del_sx.Add(ww_sx);
                                }
                            }
                        }
                        else if (result["ZC_SXN"][j]["Name"].ToString() == "是否包含物联网传感")
                        {
                            if (sx.V == "1")
                            {
                                var wlw_resu = m_Db.Inquire_Data(collection, new List<string>() { "Dimension_dic.应用特性维度" }, new List<string>() { "物联网信息" });
                                foreach (var ysx in wlw_resu)
                                {
                                    SJ_SX_Model wlw_sx = new SJ_SX_Model();
                                    wlw_sx.V = f[ysx.ID];
                                    wlw_sx.mp = ysx.ID;
                                    new_ZC_SX.Add(wlw_sx);
                                }
                            }
                            if (sx.V == "0")
                            {
                                var wlw_resu = m_Db.Inquire_Data(collection, new List<string>() { "Dimension_dic.应用特性维度" }, new List<string>() { "物联网信息" });
                                foreach (var ysx in wlw_resu)
                                {
                                    SJ_SX_Model wlw_sx = new SJ_SX_Model();
                                    wlw_sx.V = f[ysx.ID];
                                    wlw_sx.mp = ysx.ID;
                                    del_sx.Add(wlw_sx);
                                }
                            }
                        }
                        else if (result["ZC_SXN"][j]["Name"].ToString() == "陈列品标识")
                        {
                            if (sx.V == "1")
                            {
                                var bb_resu = m_Db.Inquire_Data(collection, new List<string>() { "Dimension_dic.应用特性维度" }, new List<string>() { "标本信息" });
                                foreach (var ysx in bb_resu)
                                {
                                    SJ_SX_Model bb_sx = new SJ_SX_Model();
                                    bb_sx.V = f[ysx.ID];
                                    bb_sx.mp = ysx.ID;
                                    new_ZC_SX.Add(bb_sx);
                                }
                            }
                            if (sx.V == "0")
                            {
                                var bb_resu = m_Db.Inquire_Data(collection, new List<string>() { "Dimension_dic.应用特性维度" }, new List<string>() { "标本信息" });
                                foreach (var ysx in bb_resu)
                                {
                                    SJ_SX_Model bb_sx = new SJ_SX_Model();
                                    bb_sx.V = f[ysx.ID];
                                    bb_sx.mp = ysx.ID;
                                    del_sx.Add(bb_sx);
                                }
                            }
                        }
                        j++;
                    }
                    catch
                    {
                        foreach(var del in del_sx)
                        {
                            new_ZC_SX.Remove(del);
                            for (int i = 0; i < new_ZC_SX.Count; i++)
                            {
                                if (new_ZC_SX[i].mp == del.mp)
                                {
                                    new_ZC_SX.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
                m_Db_Operate.Up_Data(coll, "ZCBM", f["0003"], "ZC_SX", new_ZC_SX);
                return View("../Home/add_OK");
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("../Home/add_ERROR");
            };
        }
        //TODO:添加一些资产
        public IActionResult add_zc_ten()
        {
            M_Db_Operate m_Db_Ct = new M_Db_Operate();
            var coll = m_Db_Ct.database.GetCollection<Class_template>("Class_templates");
            M_Db_Operate zc_db = new M_Db_Operate();
            var zc_coll = zc_db.database.GetCollection<SJ_ZC_Model>("ZC_data");
            int number = zc_db.Inquire_Data(zc_coll, type: 2).Count;
            var types = m_Db_Ct.Inquire_Data(coll,type:2);

            foreach (var type in types)
            {
                List<SJ_ZC_Model> sj_zc_s = new List<SJ_ZC_Model>();
                var res = m_Db_Ct.Get_Aggregate("Class_templates",
                    new List<string>() { "TPYE" },
                    new List<string>() { type.TPYE },
                    new List<string>() { "Meta_attribute"},
                    new List<string>() { "ZC_SX"},
                    new List<string>() { "ID"},
                    new List<string>() { "ZC_SX"})[0];
                for (int i = 0; i < 27273; i++)
                {
                    number++;
                    SJ_ZC_Model sJ_ZC = new SJ_ZC_Model(res,type.TPYE, number);
                    sj_zc_s.Add(sJ_ZC);
                }
                zc_coll.InsertMany(sj_zc_s);
            }
            //总条目数
            return query_zc();
        }
    }
}
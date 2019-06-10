using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DB_management.Models;
using Microsoft.AspNetCore.Http;
using DB_Server;
using MongoDB.Bson;
using MongoDB.Driver;
using DB_management.Model;

namespace DB_management.Controllers
{
    public class HomeController : Controller
    {
        //属性名称列表，实际应用时请从数据库中读取
        public List<string> sx_name = new List<string>() {
            "资产名称","资产代码","账面价值","计量单位","使用人","规格","存放地点","扩展属性"
        };
        public List<string> ty_names = new List<string>() {
            "存放地点","规格"
        };
        public List<string> zy_names = new List<string>() {
            "使用人","账面价值","计量单位","资产名称","资产代码"
        };
        public IActionResult Index()
        {
            List<string> sx_name = new List<string>();

            M_Db_Operate m_Db_Operate = new M_Db_Operate();
            var collection = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");
            var result = m_Db_Operate.Inquire_Data(collection,type:2);//查询全部资产

            //拼合 显示列表
            List<List<string>>ZC_datas = new List<List<string>>();
            for (var i = 0; i < result.Count; i++)
            {
                List<string> ZC_data = new List<string>();
                ZC_data.Add(result[i].Asset_Name.V.ToString());
                ZC_data.Add(result[i].Asset_Coding.V.ToString());
                ZC_data.Add(result[i].Book_Value.V.ToString());
                ZC_data.Add(result[i].Unit_of_measurement.V.ToString());
                ZC_data.Add(result[i].User.V.ToString());
                ZC_data.Add(result[i].Specifications.V.ToString());
                ZC_data.Add(result[i].Storage_location.V.ToString());
                string kz_sx = "";
                for (var j = 0; j < result[i].KJ_SX.Count; j++)
                {
                    kz_sx += result[i].KJ_SX[j].mp.ToString() + " : " + result[i].KJ_SX[j].V.ToString() + "  \n";
                }
                ZC_data.Add(kz_sx);
                ZC_datas.Add(ZC_data);
            }
            //像视图传值
            ViewData["ZC_datas"] = ZC_datas;
            ViewData["sx_name"] = sx_name;
            return View();
        }
        //TODO:添加界面
        public IActionResult add()
        {
            Class_template ww_type = new Class_template("文物");
            Class_template jsj_type = new Class_template("计算机设备");
            var KZJ_Names = new List<string>();
            ViewData["CR_M"] = ww_type.KZ_SX;
            ViewData["E_M"] = jsj_type.KZ_SX;
            ViewData["KZJ_Names"] = KZJ_Names;
            ViewData["zy_names"] = zy_names;
            ViewData["ty_names"] = ty_names;
            return View();
        }
        public IActionResult manage_zc_type()
        {
            ViewData["zy_names"] = zy_names;
            ViewData["ty_names"] = ty_names;
            //从数据库中读取数据
            M_Db_Operate m_Db_Operate = new M_Db_Operate();
            var collection = m_Db_Operate.database.GetCollection<YSX_Model>("Meta_attribute");
            var result = m_Db_Operate.Inquire_Data(collection,type:2).ToList();
            ViewData["KZJ_Names"] = result;

            Class_template ww_type = new Class_template("文物");
            Class_template jsj_type = new Class_template("计算机设备");
            ViewData["CR_M"] = ww_type.KZ_SX;
            ViewData["E_M"] = jsj_type.KZ_SX;

            return View();
        }
        public IActionResult add_ysx()
        {
            var p = new YSX_Model("1");

            ViewData["YSX_Model"] = p;
            //维度信息查询
            M_Db_Operate m_Db_Operate = new M_Db_Operate();
            var collection = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");

            //查询维度
            List<string> key = new List<string>();
            key.Add("TYPE");
            List<string> value = new List<string>();
            value.Add("资产类别维度");
            var type0 = m_Db_Operate.Inquire_Data(collection, key, value);
            value[0] = "应用特性维度";
            var type1 = m_Db_Operate.Inquire_Data(collection, key, value);
            value[0] = "管理者维度";
            var type2 = m_Db_Operate.Inquire_Data(collection, key, value);
            value[0] = "自定义维度";
            var type3 = m_Db_Operate.Inquire_Data(collection, key, value);

            ViewData["type0"] = type0;
            ViewData["type1"] = type1;
            ViewData["type2"] = type2;
            ViewData["type3"] = type3;

            return View();
        }
        public IActionResult add_wd()
        {
            return View();
        }
        public IActionResult add_wdxx()
        {
            return View();
        }
        public IActionResult add_ERROR()
        {
            return View();
        }
        public IActionResult query_ERROR()
        {
            return View();
        }
        //TODO:添加完成及动作
        public IActionResult add_OK()
        {
            return View("add_OK");
        }
        public IActionResult addWD_OK(IFormCollection f)
        {
            Dimension_Information_Model D_M = new Dimension_Information_Model();
            D_M.NAME = f["wd_name"];
            D_M.ID = f["wd_id"];
            D_M.Remarks = f["bg"];

            //在这里将数据插入数据库
            try
            {
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");
                m_Db_Operate.Insert_Data(collection, D_M);
                return View("add_OK");
            }
            catch(InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("add_ERROR");
            }
        }
        public IActionResult addWDXX_OK(IFormCollection f)
        {
            Dimension_Information_Model D_I_M = new Dimension_Information_Model();
            D_I_M.NAME = f["wdxx_name"];
            D_I_M.ID   = f["wdxx_id"];
            D_I_M.TYPE= f["wd_name"];
            D_I_M.F_ID = f["fl_name"];
            D_I_M.Remarks = f["bz"];
            try
            {
                //在这里将数据插入数据库
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");
                m_Db_Operate.Insert_Data(collection, D_I_M);

                return View("add_OK");
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("add_ERROR");
            }
        }
        public IActionResult addZC_OK(IFormCollection f)
        {

            string Kindda, One_Class, Two_Class;
            Kindda = f["Kindda"];
            One_Class = f["One_Class"];
            Two_Class = f["Two_Class"];

            SJ_ZC_Model SJ_ZC = new SJ_ZC_Model();
            SJ_ZC.Asset_Coding.V = f["资产代码"];
            SJ_ZC.Asset_Coding.mp = "资产代码";
            SJ_ZC.Asset_Name.V = f["资产名称"];
            SJ_ZC.Asset_Name.mp = "资产名称";
            SJ_ZC.Book_Value.V = f["账面价值"];
            SJ_ZC.Book_Value.mp = "账面价值";
            SJ_ZC.User.V = f["使用人"];
            SJ_ZC.User.mp = "使用人";
            SJ_ZC.Unit_of_measurement.V = f["计量单位"];
            SJ_ZC.Unit_of_measurement.mp = "计量单位";
            SJ_ZC.Specifications.V = f["规格"];
            SJ_ZC.Specifications.mp = "规格";
            SJ_ZC.Storage_location.V = f["存放地点"];
            SJ_ZC.Storage_location.mp = "存放地点";
            Class_template old = new Class_template();
            if (Two_Class == "计算机设备")
            {
                old = new Class_template("计算机设备");
            }
            else if (Two_Class == "文物")
            {
                old = new Class_template("文物");
            }
            for (var i = 0; i < old.KZ_SX.Count; i++)
            {
                SJ_SX_Model sj_sx = new SJ_SX_Model();
                sj_sx.V = f[old.KZ_SX[i].ToString()];
                sj_sx.mp =  old.KZ_SX[i].ToString();
                SJ_ZC.KJ_SX.Add(sj_sx);
            }
            try
            {
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");
                collection.InsertOne(SJ_ZC);
                return View("add_OK");
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("add_ERROR");
            };
        }
        public IActionResult addYSX_OK(IFormCollection f)
        {
            var p = new YSX_Model();
            p.ID = f["ID"];
            p.Length = f["Length"];
            p.Name = f["Name"];
            p.Unit = f["Unit"];
            p.Remarks = f["Remarks"];
            p.Edition = f["Edition"];
            p.Data_Type = f["Data_Type"];
            List<string> keys = new List<string>();
            List<string> value = new List<string>();
            List<Dictionary<string, string>> WDs = new List<Dictionary<string, string>>();
            int j = 0;
            for (int i = 0; i < f.Count; i++)
            {
                if (f.Keys.ToArray()[i].Contains("ysx_wd_key_"))
                {
                    keys.Add(f["ysx_wd_key_"+(j+1).ToString()]);
                    value.Add(f["ysx_wd_value_" + (j + 1).ToString()]);
                    p.Dimension_dic[keys[j]] = value[j];
                    j++;
                }
            };
            //在这里将数据插入数据库
            try
            {

                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<YSX_Model>("Meta_attribute");

                m_Db_Operate.Insert_Data(collection, p);
                return View("add_OK");
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("add_ERROR");
            }
        }
        public IActionResult addZCTYPE_OK(IFormCollection f)
        {
            try
            {   
                Class_template zc_type = new Class_template();
                int i = 1;
                while(true)
                {
                    var name = f["kz_key_" + i.ToString()];
                    if (name.ToString() == "")
                    {
                        break;
                    }
                    else
                    {
                        zc_type.KZ_SX.Add(name.ToString());
                        i++;
                    }

                }
                zc_type.TPYE = f["Two_Class"];
                if (zc_type.TPYE == "-- 中类 --")
                {
                    ViewData["e"] = "类别错误";
                    return View("add_ERROR");

                }
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<Class_template>("Class_templates");
                Class_template old = new Class_template();
                if (zc_type.TPYE == "文物")
                {
                    old = new Class_template("文物");

                }
                else if (zc_type.TPYE == "计算机设备")
                {
                    old = new Class_template("计算机设备");
                }
                for (var j = 0; j < old.KZ_SX.Count; j++)
                {
                    zc_type.KZ_SX.Add(old.KZ_SX[j]);
                }
                collection.UpdateMany(Builders<Class_template>.Filter.Eq("TPYE", zc_type.TPYE), Builders<Class_template>.Update.Set("KZ_SX", zc_type.KZ_SX));
                return View("add_OK");
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("add_ERROR");
            }
        }
        //TODO:资产查询
        public IActionResult query_zc()
        {
            //在这里查询数据库并整合数据

            M_Db_Operate m_Db_Operate = new M_Db_Operate();
            var collection = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");
            FilterDefinitionBuilder<SJ_ZC_Model> builderFilter = Builders<SJ_ZC_Model>.Filter;
            var result = collection.FindSync<SJ_ZC_Model>(builderFilter.Empty).ToList();
            //整合数据
            List<List<string>> ZC_datas = new List<List<string>>();
            for (var i = 0; i < result.Count; i++)
            {
                List<string> ZC_data = new List<string>();
                ZC_data.Add(result[i].Asset_Name.V.ToString());
                ZC_data.Add(result[i].Asset_Coding.V.ToString());
                ZC_data.Add(result[i].Book_Value.V.ToString());
                ZC_data.Add(result[i].Unit_of_measurement.V.ToString());
                ZC_data.Add(result[i].User.V.ToString());
                ZC_data.Add(result[i].Specifications.V.ToString());
                ZC_data.Add(result[i].Storage_location.V.ToString());
                string kz_sx = "";
                for (var j = 0; j < result[i].KJ_SX.Count; j++)
                {
                    kz_sx += result[i].KJ_SX[j].mp.ToString() + " : " + result[i].KJ_SX[j].V.ToString() + "     ";
                }
                ZC_data.Add(kz_sx);
                ZC_datas.Add(ZC_data);
            }
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
            return View(ViewData);
        }
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
                var collection = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");

                List<string> key = new List<string>();
                List<string> value = new List<string>();
                switch (ysx_name.ToString())
                {
                    case "资产名称":key.Add("Asset_Name.V");value.Add(ysx_value.ToString()); break;
                    case "资产代码": key.Add("Asset_Coding.V"); value.Add(ysx_value.ToString()); break;
                    case "账面价值": key.Add("Book_Value.V"); value.Add(ysx_value.ToString()); break;
                    case "计量单位": key.Add("Unit_of_measurement.V"); value.Add(ysx_value.ToString()); break;
                    case "使用人": key.Add("User.V"); value.Add(ysx_value.ToString()); break;
                    case "规格": key.Add("Specifications.V"); value.Add(ysx_value.ToString()); break;
                    case "存放地点": key.Add("Storage_location.V"); value.Add(ysx_value.ToString()); break;
                    default: key.Add("KJ_SX.mp"); key.Add("KJ_SX.V"); value.Add(ysx_name.ToString()); value.Add(ysx_value.ToString()); break;
                }
                var result = m_Db_Operate.Inquire_Data(collection, key, value);


                List<List<string>> ZC_datas = new List<List<string>>();
                for (var i = 0; i < result.Count; i++)
                {
                    List<string> ZC_data = new List<string>();
                    ZC_data.Add(result[i].Asset_Name.V.ToString());
                    ZC_data.Add(result[i].Asset_Coding.V.ToString());
                    ZC_data.Add(result[i].Book_Value.V.ToString());
                    ZC_data.Add(result[i].Unit_of_measurement.V.ToString());
                    ZC_data.Add(result[i].User.V.ToString());
                    ZC_data.Add(result[i].Specifications.V.ToString());
                    ZC_data.Add(result[i].Storage_location.V.ToString());
                    string kz_sx = "";
                    for (var j = 0; j < result[i].KJ_SX.Count; j++)
                    {
                        kz_sx += result[i].KJ_SX[j].mp.ToString() + " : " + result[i].KJ_SX[j].V.ToString() + "  \n";
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
            ViewData["ysx_name"] = ysx_name;
            ViewData["ysx_value"] = ysx_value;

            return View("query_zc");
        }
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
                key.Add("Asset_Name.V");
                value.Add(zc_name.ToString());
                var result = m_Db_Operate.Inquire_Data(collection, key, value);


                List<List<string>> ZC_datas = new List<List<string>>();
                for (var i = 0; i < result.Count; i++)
                {
                    List<string> ZC_data = new List<string>();
                    ZC_data.Add(result[i].Asset_Name.V.ToString());
                    ZC_data.Add(result[i].Asset_Coding.V.ToString());
                    ZC_data.Add(result[i].Book_Value.V.ToString());
                    ZC_data.Add(result[i].Unit_of_measurement.V.ToString());
                    ZC_data.Add(result[i].User.V.ToString());
                    ZC_data.Add(result[i].Specifications.V.ToString());
                    ZC_data.Add(result[i].Storage_location.V.ToString());
                    string kz_sx = "";
                    for (var j = 0; j < result[i].KJ_SX.Count; j++)
                    {
                        kz_sx += result[i].KJ_SX[j].mp.ToString() + " : " + result[i].KJ_SX[j].V.ToString() + "  \n";
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
        public IActionResult query_zc_3(IFormCollection f)
        {
            string guanlizhe = f["glz"];
            try
            {
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection1 = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");

                List<string> key1 = new List<string>();
                key1.Add("TYPE");
                List<string> value1 = new List<string>();
                value1.Add("管理者维度");
                var Result1 = m_Db_Operate.Inquire_Data(collection1, key1, value1);
                List<string> name = new List<string>();
                for (var i = 0; i < Result1.Count; i++)
                {
                    name.Add(Result1[i].NAME);
                }
                ViewData["glz_list"] = name;
                //在这里查询数据库并整合数据
                var collection = m_Db_Operate.database.GetCollection<YSX_Model>("Meta_attribute");

                List<string> key = new List<string>();
                key.Add("Dimension_dic.管理者维度");
                List<string> value = new List<string>();
                value.Add(guanlizhe);
                var Result = m_Db_Operate.Inquire_Data(collection, key, value);

                List<string> results = new List<string>();
                for (var i = 0; i < Result.Count; i++)
                {
                    results.Add(Result[i].Name);
                }
                List<string> value2 = new List<string>();
                value2.Add("默认管理者");
                Result = m_Db_Operate.Inquire_Data(collection, key, value2);
                for (var i = 0; i < Result.Count; i++)
                {
                    results.Add(Result[i].Name);
                }
                var collection2 = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");
                FilterDefinitionBuilder<SJ_ZC_Model> builderFilter = Builders<SJ_ZC_Model>.Filter;
                var result = collection2.FindSync<SJ_ZC_Model>(builderFilter.Empty).ToList();
                List<List<string>> ZC_datas = new List<List<string>>();
                for (var i = 0; i < result.Count; i++)
                {
                    List<string> ZC_data = new List<string>();
                    ZC_data.Add(result[i].Asset_Name.V.ToString());
                    ZC_data.Add(result[i].Asset_Coding.V.ToString());
                    ZC_data.Add(result[i].Book_Value.V.ToString());
                    ZC_data.Add(result[i].Unit_of_measurement.V.ToString());
                    ZC_data.Add(result[i].User.V.ToString());
                    ZC_data.Add(result[i].Specifications.V.ToString());
                    ZC_data.Add(result[i].Storage_location.V.ToString());
                    string kz_sx = "";
                    for (var j = 0; j < result[i].KJ_SX.Count; j++)
                    {
                        if (results.Contains(result[i].KJ_SX[j].mp.ToString()))
                        {
                            kz_sx += result[i].KJ_SX[j].mp.ToString() + " : " + result[i].KJ_SX[j].V.ToString() + "     ";
                        }
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
            ViewData["glz"] = guanlizhe;
            ViewData["zc_name"] = null;
            ViewData["ysx_name"] = null;
            ViewData["ysx_value"] = null;
            return View("query_zc");
        }
        //TODO:元属性查询
        public IActionResult query_ysx()
        {
            //这里用来查询数据库，整合数据
            //
            ViewData["ysx_name"] = null;
            ViewData["wd_name"] = null;
            ViewData["f_l"] = null;
            ViewData["wd_list"] = new List<string>();
            ViewData["YSX_DATA"] =new List<YSX_Model>();
            return View();
        }
        public IActionResult query_ysx_1(IFormCollection f)
        {
            string wd = f["wd_name"];
            string wdxx = f["wdxx_name"];
            var p = new YSX_Model();
            try
            {
                //这里用来查询数据库，整合数据
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<YSX_Model>("Meta_attribute");

                List<string> key = new List<string>();
                key.Add("Dimension_dic."+ wd.ToString());
                List<string> value = new List<string>();
                value.Add(wdxx);
                var Result = m_Db_Operate.Inquire_Data(collection, key, value);
                
                string[] wd_name = new string[4];
                wd_name[0] = "应用特性维度";
                wd_name[1] = "管理者维度";
                wd_name[2] = "资产类别维度";
                wd_name[3] = "自定义维度";
                for (var i = 0; i < Result.Count; i++)
                {
                    for (var j = 0; j < wd_name.Length; j++)
                    {
                        try
                        {
                            var a = Result[i].Dimension_dic[wd_name[j]];
                        }
                        catch
                        {
                            Result[i].Dimension_dic.Add(wd_name[j], "null");
                        };
                    }
                }
                ViewData["ysx_name"] = null;
                ViewData["wdxx_name"] = wdxx;
                ViewData["wd_list"] = wd_name.ToList<string>();
                ViewData["YSX_DATA"] = Result;
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("query_ERROR");
            }
            return View("query_ysx");
        }
        public IActionResult query_ysx_2(IFormCollection f)
        {
            string ysx_2 = f["ysx_name"];
            var p = new YSX_Model();
            try
            {
                //这里用来查询数据库，整合数据
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<YSX_Model>("Meta_attribute");

                List<string> key = new List<string>();
                key.Add("Name");
                List<string> value = new List<string>();
                value.Add(ysx_2);
                var Result = m_Db_Operate.Inquire_Data(collection, key, value);

                string[] wd_name = new string[4];
                wd_name[0] = "应用特性维度";
                wd_name[1] = "管理者维度";
                wd_name[2] = "扩展资产类别维度";
                wd_name[3] = "自定义维度";
                for (var i = 0; i < Result.Count; i++)
                {
                    for (var j = 0; j < wd_name.Length; j++)
                    {
                        try
                        {
                            var a = Result[i].Dimension_dic[wd_name[j]];
                        }
                        catch
                        {
                            Result[i].Dimension_dic.Add(wd_name[j], "null");
                        };
                    }
                }

                ViewData["ysx_name"] = ysx_2;
                ViewData["wd_name"] = null;
                ViewData["f_l"] = null;
                ViewData["wd_list"] = wd_name.ToList<string>();
                ViewData["YSX_DATA"] = Result;
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("query_ERROR");
            }
            ViewData["YSX_Model"] = p;
            return View("query_ysx");
        }
        //TODO:维度查询
        public IActionResult query_wd()
        {
            try
            {
                ViewData["wd_name1"] = null;
                ViewData["wd_name2"] = null;
                ViewData["pf_name2"] = null;
                ViewData["data_name3"] = null;
                ViewData["data"] = new List<Dimension_Information_Model>();
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("query_ERROR");
            }
            return View(ViewData);
        }
        public IActionResult query_wd_1(IFormCollection f)
        {
            string wd_name = f["wd_name"];
            try
            {
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");

                List<string> key = new List<string>();
                key.Add("TYPE");
                List<string> value = new List<string>();
                value.Add(wd_name);
                var Result = m_Db_Operate.Inquire_Data(collection, key, value);
                ViewData["wd_name1"] = wd_name;
                ViewData["wd_name2"] = null;
                ViewData["pf_name2"] = null;
                ViewData["data_name3"] = null;
                ViewData["data"] = Result;
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("query_ERROR");
            }
            return View("query_wd");
        }
        public IActionResult query_wd_2(IFormCollection f)
        {
            string pf_name = f["pf_name"];
            string wd_name = f["wd_name"];
            try
            {
                //这里用来查询数据库整合数据

                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");

                List<string> key = new List<string>();
                key.Add("TYPE");
                key.Add("F_ID");
                List<string> value = new List<string>();
                value.Add(wd_name);
                value.Add(pf_name);
                var Result = m_Db_Operate.Inquire_Data(collection, key, value);
                ViewData["wd_name1"] = null;
                ViewData["wd_name2"] = wd_name;
                ViewData["pf_name2"] = pf_name;
                ViewData["data_name3"] = null;
                ViewData["data"] = Result;
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("query_ERROR");
            }
            return View("query_wd");
        }
        public IActionResult query_wd_3(IFormCollection f)
        {
            string data_name = f["data_name"];
            try
            {
                //这里用来查询数据库整合数据
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");

                List<string> key = new List<string>();
                key.Add("NAME");
                List<string> value = new List<string>();
                value.Add(data_name);
                var Result = m_Db_Operate.Inquire_Data(collection, key, value);
                ViewData["wd_name1"] = null;
                ViewData["wd_name2"] = null;
                ViewData["pf_name2"] = null;
                ViewData["data_name3"] = data_name;
                ViewData["data"] = Result;
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("query_ERROR");
            }
            return View("query_wd");
        }
        //TODO:添加十条资产
        public IActionResult add_zc_ten(IFormCollection f)
        {
            M_Db_Operate m_Db_Operate = new M_Db_Operate();
            var collection1 = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");

            List<string> key1 = new List<string>();
            key1.Add("TYPE");
            List<string> value1 = new List<string>();
            value1.Add("管理者维度");
            var Result1 = m_Db_Operate.Inquire_Data(collection1, key1, value1);
            List<string> name1 = new List<string>();
            for (var i = 0; i < Result1.Count; i++)
            {
                name1.Add(Result1[i].NAME);
            }
            ViewData["glz_list"] = name1;

            var collection = m_Db_Operate.database.GetCollection<SJ_ZC_Model>("ZC_data");

            List<string> key = new List<string>();
            List<string> value = new List<string>();
            key.Add("Asset_Name.V");
            value.Add("汉代古籍");
            var add_flag = m_Db_Operate.Inquire_Data(collection, key, value);
            if (add_flag.Count != 0 )
            {
                FilterDefinitionBuilder<SJ_ZC_Model> builderFilter1 = Builders<SJ_ZC_Model>.Filter;
                var result1 = collection.FindSync<SJ_ZC_Model>(builderFilter1.Empty).ToList();

                List<List<string>> ZC_datas1 = new List<List<string>>();
                for (var i = 0; i < result1.Count; i++)
                {
                    List<string> ZC_data = new List<string>();
                    ZC_data.Add(result1[i].Asset_Name.V.ToString());
                    ZC_data.Add(result1[i].Asset_Coding.V.ToString());
                    ZC_data.Add(result1[i].Book_Value.V.ToString());
                    ZC_data.Add(result1[i].Unit_of_measurement.V.ToString());
                    ZC_data.Add(result1[i].User.V.ToString());
                    ZC_data.Add(result1[i].Specifications.V.ToString());
                    ZC_data.Add(result1[i].Storage_location.V.ToString());
                    string kz_sx = "";
                    for (var j = 0; j < result1[i].KJ_SX.Count; j++)
                    {
                        kz_sx += result1[i].KJ_SX[j].mp.ToString() + " : " + result1[i].KJ_SX[j].V.ToString() + "  \n";
                    }
                    ZC_data.Add(kz_sx);
                    ZC_datas1.Add(ZC_data);
                }
                ViewData["ZC_datas"] = ZC_datas1;
                ViewData["sx_name"] = sx_name;
                return View("query_zc");
            }

            string[] name = { "汉代古籍", "宋代古籍", "唐伯虎字画", "齐白石画" };
            string[] age = { "1680", "1790", "1630", "1455" };
            string[] level = { "1", "3", "2", "1" };
            for (var i = 0; i < 4; i++)
            {
                //插入资产
                SJ_ZC_Model SJ_ZC = new SJ_ZC_Model();
                SJ_ZC.Asset_Coding.V = "a601"+i.ToString();
                SJ_ZC.Asset_Coding.mp = "资产代码";
                SJ_ZC.Asset_Name.V = name[i];
                SJ_ZC.Asset_Name.mp = "资产名称";
                SJ_ZC.Book_Value.V = (i+1).ToString()+"000";
                SJ_ZC.Book_Value.mp = "账面价值";
                SJ_ZC.User.V = "xxx";
                SJ_ZC.User.mp = "使用人";
                SJ_ZC.Unit_of_measurement.V = "个";
                SJ_ZC.Unit_of_measurement.mp = "计量单位";
                SJ_ZC.Specifications.V = "20*20";
                SJ_ZC.Specifications.mp = "规格";
                SJ_ZC.Storage_location.V = "行政楼";
                SJ_ZC.Storage_location.mp = "存放地点";
                Class_template old = new Class_template("文物");
                for (var j = 0; j < old.KZ_SX.Count; j++)
                {
                    SJ_SX_Model sj_sx = new SJ_SX_Model();
                    sj_sx.mp = old.KZ_SX[j].ToString();
                    if (old.KZ_SX[j].ToString() == "藏品年代")
                        sj_sx.V = age[i];
                    else if (old.KZ_SX[j].ToString() == "文物等级")
                        sj_sx.V = level[i];
                    else
                        sj_sx.V = i.ToString() + " : " + j.ToString();
                    SJ_ZC.KJ_SX.Add(sj_sx);
                }
                collection.InsertOne(SJ_ZC);
            }
            
            FilterDefinitionBuilder<SJ_ZC_Model> builderFilter = Builders<SJ_ZC_Model>.Filter;
            var result = collection.FindSync<SJ_ZC_Model>(builderFilter.Empty).ToList();

            List<List<string>> ZC_datas = new List<List<string>>();
            for (var i = 0; i < result.Count; i++)
            {
                List<string> ZC_data = new List<string>();
                ZC_data.Add(result[i].Asset_Name.V.ToString());
                ZC_data.Add(result[i].Asset_Coding.V.ToString());
                ZC_data.Add(result[i].Book_Value.V.ToString());
                ZC_data.Add(result[i].Unit_of_measurement.V.ToString());
                ZC_data.Add(result[i].User.V.ToString());
                ZC_data.Add(result[i].Specifications.V.ToString());
                ZC_data.Add(result[i].Storage_location.V.ToString());
                string kz_sx = "";
                for (var j = 0; j < result[i].KJ_SX.Count; j++)
                {
                    kz_sx += result[i].KJ_SX[j].mp.ToString() + " : " + result[i].KJ_SX[j].V.ToString() + "  \n";
                }
                ZC_data.Add(kz_sx);
                ZC_datas.Add(ZC_data);
            }
            ViewData["ZC_datas"] = ZC_datas;
            ViewData["sx_name"] = sx_name;
            return View("query_zc");
        }
        //TODO:隐私声明
        public IActionResult Privacy()
        {
            return View();
        }
        //TODO:错误界面
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB_management.Models;
using DB_Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DB_management.Controllers
{
    public class YSX_Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //添加元属性页面
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
        //添加元属性完成动作
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
                    keys.Add(f["ysx_wd_key_" + (j + 1).ToString()]);
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
                return View("../Home/add_OK");
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("../Home/add_ERROR");
            }
        }
        //元属性查询页面
        public IActionResult query_ysx()
        {
            //这里用来查询数据库，整合数据
            //
            ViewData["ysx_name"] = null;
            ViewData["wd_name"] = null;
            ViewData["f_l"] = null;
            ViewData["wd_list"] = new List<string>();
            ViewData["YSX_DATA"] = new List<YSX_Model>();
            return View();
        }
        //元属性查询方式1：维度信息查询
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
                key.Add("Dimension_dic." + wd.ToString());
                List<string> value = new List<string>();
                value.Add(wdxx);
                var Result = m_Db_Operate.Inquire_Data(collection, key, value);

                string[] wd_name = new string[4];
                wd_name[0] = "应用特性维度";
                wd_name[1] = "管理者维度";
                wd_name[2] = "资产类别维度";
                wd_name[3] = "自定义维度";
                //for (var i = 0; i < Result.Count; i++)
                //{
                //    for (var j = 0; j < wd_name.Length; j++)
                //    {
                //        try
                //        {
                //            var a = Result[i].Dimension_dic[wd_name[j]];
                //        }
                //        catch
                //        {
                //            Result[i].Dimension_dic.Add(wd_name[j], "null");
                //        };
                //    }
                //}
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
        //元属性查询方式2：元属性名称查询
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
        //元属性删除页面
        public IActionResult Del_ysx()
        {
            return View();
        }
        //元属性删除完成动作
        public IActionResult Del_ysx_OK(IFormCollection f)
        {
            try
            {
                var name = f["One_Class"];
                if (name == "")
                    return View("../Home/add_ERROR");
                var type = f["Kindda"];
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var coll = m_Db_Operate.database.GetCollection<YSX_Model>("Meta_attribute");
                //List<string> KEY = new List<string>();
                //List<string> Value = new List<string>();
                //KEY.Add("Dimension_dic.资产类别维度");
                //KEY.Add("Name");
                //Value.Add(type);
                //Value.Add(name);
                //var res = m_Db_Operate.Inquire_Data(coll, KEY, Value);
                m_Db_Operate.Del_Data(coll, "ID", name);
            }
            catch
            {
                return View("../Home/add_ERROR");
            }
            return View("../Home/add_OK");
        }
        //元属性修改页面
        public IActionResult Up_Ysx()
        {
            return View();
        }
        //元属性修改完成动作
        public IActionResult Up_Ysx_OK(IFormCollection f)
        {
            try
            {
                var p = new YSX_Model();
                p.ID = f["ID"];
                p.Name = f["One_Class"];
                p.Length = f["Length"];
                p.Unit = f["Unit"];
                p.Remarks = f["Remarks"];
                p.Edition = f["Edition"];
                p.Data_Type = f["Data_Type"];

                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var coll = m_Db_Operate.database.GetCollection<YSX_Model>("Meta_attribute");
                //m_Db_Operate.Up_Data(coll, "ID", p.ID, "ID", p.ID);
                m_Db_Operate.Up_Data(coll, "ID", p.ID, "Length", p.Length);
                m_Db_Operate.Up_Data(coll, "ID", p.ID, "Remarks", p.Remarks);
                m_Db_Operate.Up_Data(coll, "ID", p.ID, "Unit", p.Unit);
                m_Db_Operate.Up_Data(coll, "ID", p.ID, "Data_Type", p.Data_Type);
                m_Db_Operate.Up_Data(coll, "ID", p.ID, "Edition", p.Edition);
                //m_Db_Operate.Up_Data(coll, "ID", p.ID, "Dimension_dic", p.Dimension_dic);
            }
            catch
            {
                return View("../Home/add_ERROR");
            }
            return View("../Home/add_OK");
        }
    }
}
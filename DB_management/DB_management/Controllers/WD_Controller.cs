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
    public class WD_Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //添加维度页面---没有用到
        public IActionResult add_wd()
        {
            return View();
        }
        //添加维度完成 -- 没有用到
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
                return View("../Home/add_OK");
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("../Home/add_ERROR");
            }
        }
        //添加维度信息页面
        public IActionResult add_wdxx()
        {
            return View();
        }
        //添加维度信息完成
        public IActionResult addWDXX_OK(IFormCollection f)
        {
            Dimension_Information_Model D_I_M = new Dimension_Information_Model();
            D_I_M.NAME = f["wdxx_name"];
            D_I_M.ID = f["wdxx_id"];
            D_I_M.TYPE = f["wd_name"];
            D_I_M.F_ID = f["fl_name"];
            D_I_M.Remarks = f["bz"];
            try
            {
                //在这里将数据插入数据库
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");
                m_Db_Operate.Insert_Data(collection, D_I_M);

                return View("../Home/add_OK");
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("../Home/add_ERROR");
            }
        }
        //查询维度信息
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
        //查询维度方式1：维度类型
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
                return View("../Home/query_ERROR");
            }
            return View("query_wd");
        }
        //查询维度方式2：父类
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
                return View("../Home/query_ERROR");
            }
            return View("query_wd");
        }
        //查询维度方式3：维度名称
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
                return View("../Home/query_ERROR");
            }
            return View("query_wd");
        }
        //修改维度信息页面
        public IActionResult Up_wd()
        {
            return View();
        }
        //修改维度信息完成
        public IActionResult Up_wd_OK(IFormCollection f)
        {
            try
            {
                Dimension_Information_Model now = new Dimension_Information_Model();
                now.ID = f["wd_id"];
                now.TYPE = f["wd_type"];
                now.F_ID = f["wd_F_id"];
                now.Remarks = f["bg"];
                now.NAME = f["One_Class"];
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var coll = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");
                m_Db_Operate.Up_Data(coll, "NAME", now.NAME, "ID", now.ID);
                m_Db_Operate.Up_Data(coll, "NAME", now.NAME, "TYPE", now.TYPE);
                m_Db_Operate.Up_Data(coll, "NAME", now.NAME, "F_ID", now.F_ID);
                m_Db_Operate.Up_Data(coll, "NAME", now.NAME, "Remarks", now.Remarks);
            }
            catch
            {
                return View("../Home/add_ERROR");
            }
            return View("../Home/add_OK");
        }
        //删除维度信息页面
        public IActionResult Del_wd()
        {
            return View();
        }
        //删除维度完成
        public IActionResult Del_wd_OK(IFormCollection f)
        {
            try
            {
                var name = f["One_Class"];
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var coll = m_Db_Operate.database.GetCollection<Dimension_Information_Model>("Dimension_Information");
                m_Db_Operate.Del_Data(coll, "NAME", name);
            }
            catch
            {
                return View("../Home/add_ERROR");
            }
            return View("../Home/add_OK");
        }
    }
}
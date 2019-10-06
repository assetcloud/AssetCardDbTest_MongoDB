using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB_management.Model;
using DB_management.Models;
using DB_Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace DB_management.Controllers
{
    public class ZC_TypeController : Controller
    {
        public List<string> ty_names = new List<string>() {
            "上级资产分类编码","上级资产分类名称","资产编码","资产名称","资产英文名称","数量","计量单位","账面价值",
            "价值类型","币种","国别","取得日期","开始使用日期","用途","使用状况","使用方向",
            "取得方式","会计凭证号","使用部门","使用人","备注"
        };
        public IActionResult Index()
        {
            return View();
        }
        //资产类别修改界面
        public IActionResult manage_zc_type()
        {
            ViewData["ty_names"] = ty_names;
            //从数据库中读取数据
            M_Db_Operate m_Db_Operate = new M_Db_Operate();
            var collection = m_Db_Operate.database.GetCollection<YSX_Model>("Meta_attribute");
            var result = m_Db_Operate.Inquire_Data(collection, type: 2).ToList();
            return View();
        }   
        //资产类别修改完成
        public IActionResult addZCTYPE_OK(IFormCollection f)
        {
            try
            {
                string Kindda, One_Class, type;
                Kindda = f["Kindda"];
                One_Class = f["One_Class"];
                if (One_Class == "不选择--")
                    type = Kindda;
                else if (One_Class == "-- 大类 -- ")
                    type = null;
                else if (Kindda == "-- 门类 -- ")
                    type = null;
                else
                    type = One_Class;
                if (type == null)
                {
                    ViewData["e"] = "类别错误";
                    return View("../Home/add_ERROR");

                }
                Class_template zc_type = new Class_template();
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<YSX_Model>("Meta_attribute");
                List<string> key = new List<string>();
                key.Add("Name");
                int i = 1;
                while (true)
                {
                    var name = f["kz_key_" + i.ToString()];
                    if (name.ToString() == "")
                    {
                        break;
                    }
                    else
                    {
                        List<string> value = new List<string>();
                        value.Add(name);
                        var Result = m_Db_Operate.Inquire_Data(collection, key, value);
                        zc_type.ZC_SX.Add(Result[0].ID.ToString());
                        i++;
                    }
                }
                Class_template old = new Class_template();
                old = new Class_template(type);
                for (var j = 0; j < old.ZC_SX.Count; j++)
                {
                    zc_type.ZC_SX.Add(old.ZC_SX[j]);
                }
                i = 1;
                while (true)
                {
                    var name = f["sc_key_" + i.ToString()];
                    if (name.ToString() == "")
                    {
                        break;
                    }
                    else
                    {
                        List<string> value = new List<string>();
                        value.Add(name);
                        var Result = m_Db_Operate.Inquire_Data(collection, key, value);
                        zc_type.ZC_SX.Remove(Result[0].ID.ToString());
                        while(zc_type.ZC_SX.Equals(Result[0].ID.ToString()))
                        {
                            zc_type.ZC_SX.Remove(Result[0].ID.ToString());
                        }
                    }
                    i++;
                }
                M_Db_Operate m_Db_Ct = new M_Db_Operate();
                var coll = m_Db_Ct.database.GetCollection<Class_template>("Class_templates");
                m_Db_Ct.Up_Data(coll, "TPYE", type, "ZC_SX", zc_type.ZC_SX);
                return View("../Home/add_OK");
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("../Home/add_ERROR");
            }
        }
        //资产类别添加界面
        public IActionResult add_zc_type()
        {
            return View();
        }
        //资产类类别添加完成
        public IActionResult addTYPE_OK(IFormCollection f)
        {
            var type_name = f["type_name"].ToString();
            var F_name = f["F_name"].ToString();
            try
            {
                Class_template old = new Class_template(F_name);
                old.TPYE = type_name;
                old.F_id = F_name;
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<Class_template>("Class_templates");
                m_Db_Operate.Insert_Data(collection,old);
                return View("../Home/add_OK");
            }
            catch (InvalidCastException e)
            {
                ViewData["e"] = e.ToString();
                return View("../Home/add_ERROR");
            }
        }
        //资产类别删除界面
        public IActionResult del_zc_type()
        {
            return View();
        }
        //资产类别删除完成
        public IActionResult del_type(IFormCollection f)
        {
            var Kindda = f["Kindda"];
            var One_Class = f["One_Class"];
            string type;
            try
            {
                if(Kindda == "-- 门类 -- ")
                    return View("../Home/add_ERROR");
                if (One_Class == "不选择--")
                    type = Kindda;
                else if (One_Class == "-- 大类 --")
                    return View("../Home/add_ERROR");
                else
                    type = One_Class;
                M_Db_Operate m_Db_Operate = new M_Db_Operate();
                var collection = m_Db_Operate.database.GetCollection<Class_template>("Class_templates");
                if (m_Db_Operate.Del_Data(collection, "TPYE", type) == false)
                    return View("../Home/add_ERROR"); 
            }
            catch
            {
                return View("../Home/add_ERROR");
            }
            return View("../Home/add_OK");
        }
        //资产类别查询页面
        public IActionResult qu_type()
        {
            return View();
        }
    }
}
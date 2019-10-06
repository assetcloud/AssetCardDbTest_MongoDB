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
        public List<string> zy_names = new List<string>();
        public IActionResult Index()
        {

            //M_Db_Operate m_Db_Operate = new M_Db_Operate();

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

            //List<List<string>> ZC_datas = new List<List<string>>();
            //for (int i = 0; i < 100; i++)
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
            //像视图传值
            //ViewData["ZC_datas"] = ZC_datas;
            //ViewData["sx_name"] = sx_name;
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

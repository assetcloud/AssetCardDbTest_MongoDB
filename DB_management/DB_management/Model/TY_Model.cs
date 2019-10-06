using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_management.Model
{
    public class TY_Model
    {
        public SJ_SX_Model SJZCFLBM { set; get; } //上级资产分类编码
        public SJ_SX_Model SJZCFLMC { set; get; } //上级资产分类名称
        public SJ_SX_Model ZCBM { set; get; }   //资产编码
        public SJ_SX_Model ZCMC { set; get; }     //资产名称
        public SJ_SX_Model YWMC { set; get; }     //资产英文名称
        public SJ_SX_Model SL { set; get; }         //数量
        public SJ_SX_Model JLDW { set; get; }//计量单位
        public SJ_SX_Model ZMJZ { set; get; }     //账面价值
        public SJ_SX_Model JZLX { set; get; }     //价值类型
        public SJ_SX_Model BiZ { set; get; }       //币种
        public SJ_SX_Model GB { set; get; }       //国别
        public SJ_SX_Model QDRQ { set; get; }       //取得日期
        public SJ_SX_Model KSKYRQ{ set; get; }       //开始使用日期
        public SJ_SX_Model YT { set; get; }       //用途
        public SJ_SX_Model SYZK { set; get; }       //使用状况
        public SJ_SX_Model SYFX { set; get; }       //使用方向
        public SJ_SX_Model QDFS { set; get; }       //取得方式
        public SJ_SX_Model KJPZH { set; get; }       //会计凭证号
        public SJ_SX_Model SYBM { set; get; }       //使用部门
        public SJ_SX_Model SYR { set; get; }           //使用人
        public SJ_SX_Model BZ { set; get; }  //备注
        public TY_Model()
        {
            SJZCFLBM = new SJ_SX_Model();
            SJZCFLMC = new SJ_SX_Model();
            ZCBM = new SJ_SX_Model();
            ZCMC = new SJ_SX_Model();
            YWMC = new SJ_SX_Model();
            SL = new SJ_SX_Model();
            JLDW = new SJ_SX_Model();
            ZMJZ = new SJ_SX_Model();
            JZLX = new SJ_SX_Model();
            BiZ = new SJ_SX_Model();
            GB = new SJ_SX_Model();
            QDRQ = new SJ_SX_Model();
            KSKYRQ = new SJ_SX_Model();
            YT = new SJ_SX_Model();
            SYZK = new SJ_SX_Model();
            SYFX = new SJ_SX_Model();
            QDFS = new SJ_SX_Model();
            KJPZH = new SJ_SX_Model();
            SYBM = new SJ_SX_Model();
            SYR = new SJ_SX_Model();
            BZ = new SJ_SX_Model();
        }
    }
}

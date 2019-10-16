using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketHTPay
{
    /// <summary>
    /// 支付请求
    /// </summary>
    public partial class PayRequest
    {
        /// <summary>
        /// 支付方式
        /// </summary>
        public PayType PayType { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 总费用   单位分
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>
        /// 附加信息
        /// </summary>
        public string Attach { get; set; }
    }


    public enum PayType
    {
        /// <summary>
        /// 微信
        /// </summary>
        WX=1,
        /// <summary>
        /// 支付宝
        /// </summary>
        ALI=2,
        /// <summary>
        /// 京东
        /// </summary>
        JD=3,
        /// <summary>
        /// 手机QQ
        /// </summary>
        QQ=4,
        /// <summary>
        /// 银联
        /// </summary>
        UNION=5,
        /// <summary>
        /// 翼支付
        /// </summary>
        BEST=6
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketHTPay
{
    /// <summary>
    /// 支付返回结果
    /// </summary>
    public class PayResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string errTypeMsg { get; set; }
        /// <summary>
        /// 错误码 成功返回0000
        /// </summary>
        public string errCode { get; set; }
        /// <summary>
        /// 错误消息 成功返回SUCCESS
        /// </summary>
        public string errMsg { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public PayResultData data { get; set; }
        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime requestTime { get; set; }
        /// <summary>
        /// 请求流水号
        /// </summary>
        public string requestId { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

    }

    /// <summary>
    /// 返回信息
    /// </summary>
    public class PayResultData
    {
        /// <summary>
        /// 交易状态
        /// </summary>
        public int tradeStatus { get; set; }

        /// <summary>
        /// 交易信息
        /// </summary>
        public string tradeMsg { get; set; }
        /// <summary>
        /// 交易类型 WX-微信；ALI-支付宝；JD-京东；QQ-手机QQ；UNION-银联；BEST-翼支付（支付服务类型为ActiveCode、JSApi、H5、MiniProgram、App时，此参数比传）【PassiveCode-被扫支付传递相应参数值，可用来区分京东和银联支付渠道信息】
        /// </summary>

        public string tradeType { get; set; }
        /// <summary>
        /// 商户交易订单号
        /// </summary>

        public string outTradeNo { get; set; }
        /// <summary>
        /// 请求流水号
        /// </summary>

        public string reqId { get; set; }

        /// <summary>
        ///费用
        /// </summary>

        public int totalFee { get; set; }

        /// <summary>
        /// 支付二维码
        /// </summary>

        public string payQRCodeUrl { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>

        public int expiredTime { get; set; }
    }
}

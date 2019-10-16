using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketHTPay
{
    /// <summary>
    /// 交易查询
    /// </summary>
    public class PayQuery
    {
        public string errCode { get; set; }
        public string errMsg { get; set; }
        public PayQueryData data { get; set; }
        public string requestId { get; set; }
        public string sign { get; set; }
    }

    /// <summary>
    /// 返回数据
    /// </summary>
    public class PayQueryData
    {
        /// <summary>
        /// -5-银行退票成功；-4-交易关闭；-3-部分退款；-2-已撤销；-1-交易失败；0-交易中；1-交易成功；2-撤销成功；3-退款成功；
        /// </summary>
        public int tradeStatus { get; set; }

        public string tradeMsg { get; set; }

        public string merchantSn { get; set; }

        public string merchantName { get; set; }

        public string outTradeNo { get; set; }

        public string tradeNo { get; set; }

        public int payMode { get; set; }

        public int payType { get; set; }

        public string tradeType { get; set; }

        public string tradeTypeInfo { get; set; }

        public string devicePosSn { get; set; }

        public string reqId { get; set; }

        public string reqDate { get; set; }

        public string reqTime { get; set; }

        public string goodsBody { get; set; }

        public string feeType { get; set; }

        public int totalFee { get; set; }

        public string tradeTime { get; set; }

        public string payTime { get; set; }

        public PayDetailInfo payDetailInfo { get; set; }

        public string sign { get; set; }
    }

    public class PayDetailInfo
    {
        public string bankName { get; set; }

        public string bankIssno { get; set; }

        public string acquiringBank { get; set; }

        public string cardNo { get; set; }

        public string expDate { get; set; }

        public string batchNo { get; set; }

        public string f11 { get; set; }

        public string f22 { get; set; }

        public string f38 { get; set; }

        public string f61 { get; set; }

        public string unionPayCode { get; set; }

        public string unionRefNo { get; set; }

        public string unionTerminalSn { get; set; }

        public string terminalTxNo { get; set; }

        public string settleCardNum { get; set; }
    }


}

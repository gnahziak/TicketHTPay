using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketHTPay
{
    /// <summary>
    /// 异步通知结果
    /// </summary>
    public class PayNotify
    {
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

        public payDetailInfo payDetailInfo { get; set; }

        public string sign { get; set; }
    }

    public class payDetailInfo
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

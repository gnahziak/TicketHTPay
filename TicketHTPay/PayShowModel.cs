using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketHTPay
{
    public class PayShowModel
    {
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string tradeNo { get; set; }

        /// <summary>
        /// 生成的二维码地址
        /// </summary>
        public string PayImgUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using TicketHTPay;

namespace PayMvcDemo.Controllers
{
    public class PayController : Controller
    {
        public ActionResult Index()
        {
            string privateKey =ConfigurationManager.AppSettings["privateKey"];

            PayShowModel payShowModel = new PayShowModel()
            {
                tradeNo = Guid.NewGuid().ToString("N")
            };
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("signType", "RSA");
            dict.Add("appId", "qTCH30NzGx4Kws9k");
            dict.Add("payMode", "ActiveCode");
            dict.Add("tradeType",ConfigurationManager.AppSettings["tradeType"]);
            dict.Add("merchantSn", ConfigurationManager.AppSettings["merchantSn"]);
            dict.Add("outTradeNo", payShowModel.tradeNo);
            dict.Add("totalFee", 1);
            dict.Add("notifyUrl","https://uat_activity.niceloo.com/Notify");//HttpUtility.UrlEncode("
            dict.Add("attach", "000");
            string sign = PayHelper.Sign(dict, privateKey);

            dict.Add("goodsBody", "商品名称介绍");
            dict.Add("goodsDetail", "商品详细介绍");

            dict.Add("sign", sign);
            //发送请求
            string result = PayHelper.SendPost("https://open.smart4s.com/Api/Service/Pay/Mode/trade", dict);
            Response.Write("请求结果：" + result);

            PayResult payResult = Newtonsoft.Json.JsonConvert.DeserializeObject<PayResult>(result);
            if (payResult != null && payResult.data != null)
            {
                payShowModel.PayImgUrl = PayHelper.GenerateImgCode("", payResult.data.payQRCodeUrl);
            }
            return View(payShowModel);
        }



        /// <summary>
        /// 检查交易状态
        /// </summary>
        /// <param name="tradeNo"></param>
        /// <returns></returns>
        public ActionResult CheckTrade(string tradeNo)
        {
            try
            {

                string privateKey =ConfigurationManager.AppSettings["privateKey"];
                string sign = "";


                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("signType", "RSA");
                dict.Add("appId", "qTCH30NzGx4Kws9k");
                dict.Add("merchantSn", "TEST0000000001");
                dict.Add("outOrderNo", tradeNo);
                sign = PayHelper.Sign(dict, privateKey);

                dict.Add("sign", sign);
                //发送请求
                string result = PayHelper.SendPost("https://open.smart4s.com/Api/Service/Pay/Mode/query", dict);
                LogHelper.WriteInfoLog(result);

                PayQuery payResult = Newtonsoft.Json.JsonConvert.DeserializeObject<PayQuery>(result);
                if (payResult != null)
                {
                    return Json(new { payResult.errCode, status = payResult.data.tradeStatus, msg = payResult.data.tradeMsg,result });
                }

                else
                {
                    return Json(new { errCode = "-1", status = -99, msg = "请求失败", result });
                }
            }
            catch (Exception ex)
            {
                return Json(new { errCode = "-1", status = -100, msg = "查询异常", result = ex.ToString() });
            }
        }

    }

    

}
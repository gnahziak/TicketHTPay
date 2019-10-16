using Newtonsoft.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TicketHTPay;

namespace PayMvcDemo.Controllers
{
    public class NotifyController : Controller
    {
        /// <summary>
        /// 异步通知
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            using (var reader = new System.IO.StreamReader(Request.InputStream))
            {
                string result = HttpUtility.UrlDecode(reader.ReadToEnd(),Encoding.UTF8);

                if (!string.IsNullOrEmpty(result))
                {
                    //业务处理
                    LogHelper.WriteInfoLog("异步通知Json：" + result, false, FileType.Notify);
                    PayNotify payNotify = JsonConvert.DeserializeObject<PayNotify>(result);


                    LogHelper.WriteInfoLog("序列化结果：" + JsonConvert.SerializeObject(payNotify), false, FileType.Notify);

                    if (payNotify.tradeStatus == 1)
                    {
                         LogHelper.WriteInfoLog("交易成功", false, FileType.Notify);
                    }
                }
            }

            return Json(new { result="SUCCESS"});
        }
    }
}
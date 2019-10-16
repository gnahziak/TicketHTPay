using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace TicketHTPay
{
    public class PayHelper
    {

        #region 参数按ASCII码排序
        /// <summary>
        /// 参数按ASCII码排序
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string GetOrderContent(Dictionary<string, object> dict)
        {
            var keys = dict.Keys.ToArray();
            Array.Sort(keys, string.CompareOrdinal);

            StringBuilder str = new StringBuilder();
            foreach (var item in keys)
            {
                str.Append(item + "=" + dict[item] + "&");
            }

            return str.ToString().Substring(0, str.ToString().Length - 1);
        }
        #endregion

        #region 生成签名
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="dict">参数字典</param>
        /// <param name="privateKey">私钥字符串</param>
        /// <returns></returns>
        public static string Sign(Dictionary<string, object> dict, string privateKey)
        {
            return RSAHelper.Sign(GetOrderContent(dict), privateKey);
        }
        #endregion

        #region 发送请求
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
         public static String SendPost(String url, Dictionary<String,Object> Params)
        {
            string result = "";
            HttpWebRequest req = (System.Net.HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.KeepAlive = true;

            byte[] data = Encoding.UTF8.GetBytes(SerializeDict(Params));//把字符串转换为字节

            req.ContentLength = data.Length; //请求长度

            using (Stream reqStream = req.GetRequestStream()) //获取
            {
                reqStream.Write(data, 0, data.Length);//向当前流中写入字节
                reqStream.Close(); //关闭当前流
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse(); //响应结果
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        #endregion

        #region 生成二维码图片
        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="domin">域名：http://www.baidu.com</param>
        /// <param name="context">二维码内容</param>
        /// <returns></returns>
        public static string GenerateImgCode(string domin,string context)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 8;//qrCodeEncoder.QRCodeVersion = 0;//版本(注意：设置为0主要是防止编码的字符串太长时发生错误)
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            Bitmap image = qrCodeEncoder.Encode(context);
            string path = "Upload/ImgCode/" + DateTime.Now.ToString("yyyyMMdd") + "/" + Guid.NewGuid() + ".jpg";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            if(!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
            fs.Close();
            image.Dispose();

            return domin+"/"+path;
        }
        #endregion

        #region 获取POST过来通知消息
        /// <summary>
        /// 获取POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public static SortedDictionary<string, string> GetRequestPost(NameValueCollection forms)
        {
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();

            // Get names of all forms into a string array.
            String[] requestItem = forms.AllKeys;

            for (int i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], forms[requestItem[i]]);
            }
            return sArray;
        }
        #endregion

        #region 序列化字典
        /// <summary>
        /// 序列化字典
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string SerializeDict(Dictionary<string,object> dict)
        {
            StringBuilder str = new StringBuilder();
            str.Append("{");
            foreach (var item in dict)
            {
                str.Append($"\"{item.Key}\":");
                if (item.Value is int || item.Value is double || item.Value is decimal || item.Value is long)
                {
                    str.Append(item.Value);
                }
                else
                {
                    str.Append($"\"{item.Value}\"");
                }
                str.Append(",");
            }
            string result = str.ToString().TrimEnd(',') + "}";
            return result;
        }
        #endregion
    }
}

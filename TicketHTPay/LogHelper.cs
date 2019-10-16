using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TicketHTPay
{
    #region 日志帮助类
    /// <summary>
    /// 日志帮助类  
    /// 20190727 创建 by simple
    /// </summary>
    public class LogHelper
    {
        #region 写常规日志
        /// <summary>
        /// 写常规日志
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileType"></param>
        /// <param name="isEncrypt"></param>
        /// <param name="isShowConsole"></param>
        public static void WriteInfoLog(string content, bool isEncrypt = false, FileType fileType = FileType.ShortDate, bool isShowConsole = false)
        {
            WriteLog(content, LogType.Info, fileType, isEncrypt, isShowConsole);
        }
        #endregion

        #region 写警告日志
        /// <summary>
        /// 写警告日志
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileType"></param>
        /// <param name="isEncrypt"></param>
        /// <param name="isShowConsole"></param>
        public static void WriteWarnLog(string content, bool isEncrypt = false, FileType fileType = FileType.ShortDate, bool isShowConsole = false)
        {
            WriteLog(content, LogType.Warn, fileType, isEncrypt, isShowConsole);
        }
        #endregion

        #region  写错误日志
        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileType"></param>
        /// <param name="isEncrypt"></param>
        /// <param name="isShowConsole"></param>
        public static void WriteErrorLog(string content, bool isEncrypt = false, FileType fileType = FileType.ShortDate, bool isShowConsole = false)
        {
            WriteLog(content, LogType.Error, fileType, isEncrypt, isShowConsole);
        }
        #endregion

        #region  写异常日志
        /// <summary>
        /// 写异常日志
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="fileType"></param>
        /// <param name="isEncrypt"></param>
        /// <param name="isShowConsole"></param>
        public static void WriteExceptionLog(Exception ex, bool isEncrypt = false, FileType fileType = FileType.ShortDate, bool isShowConsole = false)
        {
            WriteLog(ex.ToString(), LogType.Error, fileType, isEncrypt, isShowConsole);
        }
        #endregion

        #region 写日志

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <param name="logType">日志类型</param>
        /// <param name="fileType">文件名类型</param>
        /// <param name="isEncrypt">内容是否加密</param>
        /// <param name="isShowConsole">是否显示在控制台</param>
        public static void WriteLog(string content, LogType logType = LogType.Info, FileType fileType = FileType.ShortDate, bool isEncrypt = false, bool isShowConsole = false)
        {
            try
            {
                if (isShowConsole)
                {
                    Console.WriteLine(content);
                }
                string Path = AppDomain.CurrentDomain.BaseDirectory + $@"\Logs\" + DateTime.Now.ToString("yyyyMMdd") + @"\";
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                if (fileType == FileType.ShortDate)
                {
                    Path += DateTime.Now.ToString("yyyyMMdd") + "_" + logType.ToString() + ".log";
                }
                else if (fileType == FileType.HourDate)
                {
                    Path += DateTime.Now.ToString("yyyyMMdd_HH") + "_" + logType.ToString() + ".log";
                }
                else
                {
                    Path += fileType.ToString() + ".log";
                }
                StringBuilder str = new StringBuilder();
                if (isEncrypt && File.Exists(Path))
                {
                    str.AppendLine(AESEncryptHelper.AESDecrypt(File.ReadAllText(Path)));
                }

                str.AppendLine($"/***************Begin(时间:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")})********************/");
                str.AppendLine(content);
                str.AppendLine("/***************End********************/");
                str.AppendLine("");

                if(isEncrypt)
                {
                    using (StreamWriter sw = new StreamWriter(Path, false))
                    {
                         sw.WriteLine(AESEncryptHelper.AESEncrypt(str.ToString()));
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(Path, true))
                    {
                       sw.WriteLine(str.ToString());
                    }
                }
               
            }
            catch(Exception ex) {  }
        }

        #endregion
    }
    #endregion

    #region 枚举类型
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 常规
        /// </summary>
        Info = 10,
        /// <summary>
        /// 警告
        /// </summary>
        Warn = 20,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 30,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 40
    }

    /// <summary>
    /// 保存文件名名称
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// 日期格式  20190727
        /// </summary>
        ShortDate = 10,
        /// <summary>
        /// 小时格式  20190727_17
        /// </summary>
        HourDate = 20,
        /// <summary>
        /// 异步通知日志
        /// </summary>
        Notify=30
    }

    #endregion

    #region 加解密算法
    public class AESEncryptHelper
    {

        /// <summary>
        /// 获取密钥
        /// </summary>
        private static string Key
        {
            get { return @")O[NB]6,YF}+efcaj{+oESb9d8>Z'e9M"; }
        }

        /// <summary>
        /// 获取向量
        /// </summary>
        private static string IV
        {
            get { return @"L+\~f4,Ir)b$=pkf"; }
        }

        #region 参数是string类型

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainStr">明文字符串</param>
        /// <returns>密文</returns>
        public static string AESEncrypt(string plainStr)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);
            byte[] byteArray = Encoding.UTF8.GetBytes(plainStr);

            string encrypt = null;
            Rijndael aes = Rijndael.Create();
            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                {
                    cStream.Write(byteArray, 0, byteArray.Length);
                    cStream.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(mStream.ToArray());
                }
            }
            aes.Clear();
            return encrypt;
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainStr">明文字符串</param>
        /// <param name="returnNull">加密失败时是否返回 null，false 返回 String.Empty</param>
        /// <returns>密文</returns>
        public static string AESEncrypt(string plainStr, bool returnNull)
        {
            string encrypt = AESEncrypt(plainStr);
            return returnNull ? encrypt : (encrypt == null ? String.Empty : encrypt);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="encryptStr">密文字符串</param>
        /// <returns>明文</returns>
        public static string AESDecrypt(string encryptStr)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);
            byte[] byteArray = Convert.FromBase64String(encryptStr);

            string decrypt = null;
            Rijndael aes = Rijndael.Create();
            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                {
                    cStream.Write(byteArray, 0, byteArray.Length);
                    cStream.FlushFinalBlock();
                    decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                }
            }
            aes.Clear();
            return decrypt;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="encryptStr">密文字符串</param>
        /// <param name="returnNull">解密失败时是否返回 null，false 返回 String.Empty</param>
        /// <returns>明文</returns>
        public static string AESDecrypt(string encryptStr, bool returnNull)
        {
            string decrypt = AESDecrypt(encryptStr);
            return returnNull ? decrypt : (decrypt == null ? String.Empty : decrypt);
        }


        #endregion

        #region 256位AES加密算法

        /// <summary>
        /// 256位AES加密
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt)
        {
            // 256-AES key    
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(Key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 256位AES解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt)
        {
            // 256-AES key    
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(Key);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        #endregion
    }
    #endregion

}
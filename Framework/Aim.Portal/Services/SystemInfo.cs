using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Security;
using System.Management;
using Aim.Security;

namespace Aim.Portal
{
    public class SystemInfo
    {
        private static string FILE_NAME = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sn.txt");
        #region 属性

        /// <summary>
        /// 系统时间
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public SystemInfo()
        {
            Date = DateTime.Now;
        }
        #endregion

        //检验序列号是否有效,序列号有序列号生成工具生成,放在web根目录中,sn.txt
        public bool CheckIsValidSN()
        {
            bool isValid = false;
            string text = "";
            if (File.Exists(FILE_NAME)) //如果文件存在,则创建File.AppendText对象
            {
                try
                {
                    text = File.ReadAllText(FILE_NAME);
                    string[] texts = text.Split(';');
                    DESEncrypt en = new DESEncrypt();
                    string date = en.DecryptString(texts[0]).Split(' ')[0];
                    string days = en.DecryptString(texts[1]).Replace("\0\0", "");
                    string sn = texts[2].TrimEnd();
                    string snOld = GetRegHead2(GetMathineID(days));
                    snOld += "-";
                    snOld += GetRegTail3(GetMathineID(days));
                    if (DateTime.Now <= DateTime.Parse(date).AddDays(double.Parse(days)) && snOld == sn)
                        return true;
                }
                catch (Exception e)
                {

                }
            }
            return isValid;
        }
        /// <summary>
        /// 获取机器码
        /// </summary>
        /// <param name="序列号"></param>
        /// <returns>带“-”的四段数字</returns>
        string GetMathineID(string seriesNum)
        {
            string macAdd = GetMacAddress();
            string md5Str = FormsAuthentication.HashPasswordForStoringInConfigFile(macAdd + seriesNum, "MD5");
            return GetCode(md5Str, 4);
        }

        /// <summary>
        /// 得到以“-”分隔，每段5个字母的编码
        /// </summary>
        /// <param name="src"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        string GetCode(string src, int num)
        {
            string re = "";
            for (int i = 0; i < num; i++)
            {
                string tem = src.Substring(i * num, 5);
                re += tem;
                if (i < num - 1)
                    re += "-";
            }
            return re;
        }

        string GetMacAddress()
        {
            try
            {
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }
        /// <summary>
        /// 获取注册码的后三段。此函数不能流失出去
        /// </summary>
        /// <param name="machineCode"></param>
        /// <returns></returns>
        string GetRegTail3(string machineCode)
        {
            string regTail3;
            regTail3 = FormsAuthentication.HashPasswordForStoringInConfigFile(machineCode, "MD5");
            regTail3 = FormsAuthentication.HashPasswordForStoringInConfigFile(regTail3, "SHA1");
            regTail3 = FormsAuthentication.HashPasswordForStoringInConfigFile(regTail3, "SHA1");
            regTail3 = FormsAuthentication.HashPasswordForStoringInConfigFile(regTail3, "MD5");
            regTail3 = FormsAuthentication.HashPasswordForStoringInConfigFile(regTail3, "SHA1");
            regTail3 = FormsAuthentication.HashPasswordForStoringInConfigFile(regTail3, "MD5");
            regTail3 = FormsAuthentication.HashPasswordForStoringInConfigFile(regTail3, "MD5");

            regTail3 = GetCode(regTail3, 3);
            return regTail3;
        }

        /// <summary>
        /// 获取注册码的头两端。用来做注册码正确性的简单判断，判断失败会给提示。
        /// </summary>
        /// <param name="machineCode"></param>
        /// <returns></returns>
        string GetRegHead2(string machineCode)
        {
            string regHead2 = FormsAuthentication.HashPasswordForStoringInConfigFile(machineCode, "MD5");
            regHead2 = GetCode(regHead2, 2);
            return regHead2;
        }
    }
}

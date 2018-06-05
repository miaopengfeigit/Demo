/** 1. 功能：操作正则表达式的公共类
 *  2. 作者：何平
 *  3. 创建日期：2008-2-4
 *  4. 最后修改日期：2008-8-1
**/
#region 引用命名空间
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
#endregion
//51aspx.com 提供下载

namespace Common
{
    /// <summary>
    /// 操作正则表达式的公共类
    /// </summary>    
    public class RegexHelper
    {
        #region 验证输入的字符串是否合法
        /// <summary>
        /// 验证输入的字符串是否合法，合法返回true,否则返回false。
        /// </summary>
        /// <param name="strInput">输入的字符串</param>
        /// <param name="strPattern">模式字符串</param>        
        public static bool Validate( string strInput , string strPattern )
        {
            return Regex.IsMatch( strInput , strPattern );
        }
        /// <summary>
        /// 公式解析
        /// </summary>
        /// <param name="msgIn">输入字符串</param>
        /// <param name="rule">规则语法</param>
        /// <returns>从输入字符串获取到的信息</returns>
        public static string Filter(string rule, string msgIn)
        {
            string msgOut = "";
            string[] ruleArray = rule.Split(',');
            if (ruleArray.Length == 1)
            {
                msgOut = rule;
            }
            else if (ruleArray.Length == 2)
            {
                if (ruleArray[0].StartsWith("length:"))
                {
                    Match markStart = Regex.Match(msgIn, ruleArray[1]);
                    if (markStart.Success)
                    {
                        int length = Convert.ToInt32(ruleArray[0].Split(':')[1]);
                        int startIndex = msgIn.IndexOf(markStart.ToString()) - length;// + markStart.Length;

                        msgOut = msgIn.Substring(startIndex, length);
                    }
                    else
                    {
                        throw new Exception("length模式第二个参数失败！可能信息中没有找到内容");
                    }
                }
                else if (ruleArray[1].StartsWith("length:"))
                {
                    Match markStart = Regex.Match(msgIn, ruleArray[0]);
                    if (markStart.Success)
                    {
                        int startIndex = msgIn.IndexOf(markStart.ToString()) + markStart.Length;
                        int length = Convert.ToInt32(ruleArray[1].Split(':')[1]);
                        msgOut = msgIn.Substring(startIndex, length);
                    }
                    else
                    {
                        throw new Exception("length模式第一个参数失败！可能信息中没有找到内容");
                    }
                }
                else
                {
                    Match markStart = Regex.Match(msgIn, ruleArray[0]);
                    if (markStart.Success)
                    {
                        string msgTemp = msgIn.Substring(msgIn.IndexOf(markStart.ToString()));
                        Match markEnd = Regex.Match(msgTemp, ruleArray[1]);
                        if (markEnd.Success)
                        {
                            int startIndex = markStart.Length;
                            int length = msgTemp.IndexOf(markEnd.ToString()) - startIndex;
                            msgOut = msgTemp.Substring(startIndex, length);
                        }
                        else
                        {
                            throw new Exception("between模式第二个参数失败！可能信息中没有找到内容");
                        }
                    }
                    else
                    {
                        throw new Exception("between模式第一个参数失败！可能信息中没有找到内容");
                    }


                }
            }
            else
            {
                throw new Exception("逗号过多错误！");
            }

            return msgOut;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using weixin.Models;

namespace weixin.Bll
{
    public class AutoResponse
    {
        public static string xmlStr;
        public AutoResponse(string Content, string UserName)
        {
            string resxml = "<xml> <ToUserName><![CDATA[" + UserName + "]]></ToUserName><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[你好！开发人员请联系：15258789532]]></Content> </xml>";
            xmlStr = resxml;
            AutoResonseMes();
        }
        public string AutoResonseMes()
        {
            return (xmlStr);
        }
    }
}

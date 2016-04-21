using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using weixin.Bll;
using weixin.Models;

namespace weixin.Controllers
{
    public class weixinController : Controller
    {
        //
        // GET: /weixin/
		//Wechat Develop

        public ActionResult Index()
        {
            string result = "";

            weixin bll = new weixin();
            string postStr = "";
            if (Request.HttpMethod.ToLower() == "post")
            {
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = Encoding.UTF8.GetString(b);

                if (!string.IsNullOrEmpty(postStr)) //请求处理  
                {
                    //封装请求类  
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(postStr);
                    XmlElement rootElement = doc.DocumentElement;
                    //MsgType  
                    XmlNode MsgType = rootElement.SelectSingleNode("MsgType");
                    //接收的值--->接收消息类(也称为消息推送)  
                    RequestXML requestXML = new RequestXML();
                    requestXML.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
                    requestXML.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
                    requestXML.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
                    requestXML.MsgType = MsgType.InnerText;

                    //根据不同的类型进行不同的处理  
                    switch (requestXML.MsgType)
                    {
                        case "text": //文本消息  
                            requestXML.Content = rootElement.SelectSingleNode("Content").InnerText;
                            break;
                        case "image": //图片  
                            requestXML.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
                            break;
                        case "location": //位置  
                            requestXML.Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
                            requestXML.Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
                            requestXML.Scale = rootElement.SelectSingleNode("Scale").InnerText;
                            requestXML.Label = rootElement.SelectSingleNode("Label").InnerText;
                            break;
                        case "link": //链接  
                            break;
                        case "event": //事件推送 支持V4.5+  
                            break;
                    }
                    result = bll.Handle(requestXML);
                }
            }
            else
            {
                bll.Auth();
            }
            return Content(result);
        }
    }
}

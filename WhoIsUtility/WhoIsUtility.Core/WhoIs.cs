using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace WhoIsUtility.Core
{
    public class WhoIs : IWhoIs
    {
        // http://www.webservicex.net/New/Home/ServiceDetail/52
        // http://www.webservicex.net/whois.asmx?WSDL
        public string GetHostInfo(string hostName)
        {
            var soapEnv = @"<?xml version=""1.0"" encoding=""utf-8""?><soapenv:Envelopexmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/""xmlns:web=""http://www.webservicex.net""><soapenv:Header/><soapenv:Body><web:GetWhoIS><!--Optional:--><web:HostName>yandex.ru</web:HostName></web:GetWhoIS></soapenv:Body></soapenv:Envelope>";
            var url = "http://www.webservicex.net/whois.asmx";

            var byteData = Encoding.ASCII.GetBytes(soapEnv);

            var req = (HttpWebRequest) WebRequest.Create(url);
            req.Method = "POST";
            req.ContentLength = byteData.Length;
            req.Headers.Add("SOAPAction", "http://www.webservicex.net/GetWhoIS");
            req.Accept = "text/xml";
            req.ContentType = "text/xml; charset=utf-8";

            var soapEnvelopeXml = new XmlDocument();
            //soapEnvelopeXml.LoadXml();

            using (var strWriter = req.GetRequestStream())
            {
                strWriter.Write(byteData, 0, byteData.Length);
            }

            using (var resp = (HttpWebResponse)req.GetResponse())
            using (var str = resp.GetResponseStream())
            using (var strReader = new StreamReader(str))
            {
                return strReader.ReadToEnd();
            }
        }
    }
}

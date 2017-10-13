using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
            var url = "http://localhost:8080/axis2/services/HelloWorldService";

            var client = new HttpClient();

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post
            };

            request.Content = new StringContent(File.ReadAllText(@""), Encoding.UTF8, "text/xml");
            request.Headers.Clear();
            // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
            request.Headers.Add("SOAPAction", "http://www.webservicex.net/GetWhoIS");

            HttpResponseMessage response = client.SendAsync(request).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(result);

            return result;
        }

        private void TestSoap()
        {
            const string url = "http://www.webservicex.net/whois.asmx";
            const string action = "http://www.webservicex.net/whois.asmx/GetWhoIS";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
            HttpWebRequest webRequest = CreateWebRequest(url, action);

            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            string result;
            using (WebResponse response = webRequest.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    result = rd.ReadToEnd();
                }
            }
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope()
        {
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                    <soap:Body>
                    <GetWhoIS xmlns=""http://www.webservicex.net"">
                      <HostName>yandex.com</HostName>
                    </GetWhoIS>
                    </soap:Body>
                    </soap:Envelope>");
            return soapEnvelopeXml;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}

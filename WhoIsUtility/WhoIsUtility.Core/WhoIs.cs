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
        public async Task<string> GetHostInfo(string hostName)
        {
            var url = "http://www.webservicex.net/whois.asmx";
            var xml = GetSoapXml(hostName);

            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post,
                Content = new StringContent(xml, Encoding.UTF8, "text/xml")
            };

            request.Headers.Clear();
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");

            HttpResponseMessage response = client.SendAsync(request).Result;

            return await response.Content.ReadAsStringAsync();
        }

        private string GetSoapXml(string hostName)
        {
            return $@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                      <soap:Body>
                        <GetWhoIS xmlns=""http://www.webservicex.net"">
                          <HostName>{hostName}</HostName>
                        </GetWhoIS>
                      </soap:Body>
                    </soap:Envelope>";
        }
    }
}

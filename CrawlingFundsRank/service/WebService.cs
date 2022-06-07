using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CrawlingFundsRank.service
{
    public class WebService
    {
        public string GetContentByUrl(string url) {
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            return  client.DownloadString(url);
        }
    }
}

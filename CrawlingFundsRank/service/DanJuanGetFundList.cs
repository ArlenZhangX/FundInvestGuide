using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CrawlingFundsRank.service
{
    public class DanJuanGetFundList : GetFundListAb
    {
        public DanJuanGetFundList()
        {
            this.Url = "https://danjuanapp.com/djapi/index_eva/dj";
        }

        public override List<string> GetFundList()
        {
            var fundIds = new List<string>();
            var apiContent = webService.GetContentByUrl(this.Url);
            JObject jobJ = JObject.Parse(apiContent);
            foreach (var item in jobJ["data"]["items"])
            {
                var fundUrl = item["url"].ToString();
                Regex regex = new Regex(@"[0-9]+");
                var value = regex.Match(fundUrl).Value;
                if (!string.IsNullOrEmpty(value))
                {
                    fundIds.Add(value);
                }
            }
            return fundIds;
        }
    }
}

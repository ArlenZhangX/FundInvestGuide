
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CrawlingFundsRank.service
{
    public class GetFundListFromRankService : GetFundListAb
    {
        public enum SearchType
        {
            /// <summary>
            /// 股票
            /// </summary>
            GP,
            /// <summary>
            /// 指数
            /// </summary>
            ZS,
            /// <summary>
            /// 混合
            /// </summary>
            Mix,
        }

        public GetFundListFromRankService(SearchType type = SearchType.ZS)
        {
            switch (type)
            {
                case SearchType.ZS:
                    {
                        this.Url = "http://fund.ijijin.cn/js/datacenter/data/pj_zsx.js";
                        break;
                    }
                case SearchType.GP:
                    {
                        this.Url = "http://fund.ijijin.cn/js/datacenter/data/pj_gpx.js";
                        break;
                    }
                case SearchType.Mix:
                    {
                        this.Url = "http://fund.ijijin.cn/js/datacenter/data/pj_hhx.js";
                        break;
                    }
                default:
                    break;
            }
        }

        public override List<string> GetFundList()
        {
            var fundIds = new List<string>();
            var apiContent = webService.GetContentByUrl(this.Url);
            Regex m = new Regex("[0-9]+");
            apiContent = apiContent = apiContent.Substring(0, 15000);
            var match = m.Matches(apiContent);
            fundIds = match.Where(i => i.Value.Length == 6).ToList().Select(i=>i.Value).ToList();
            return fundIds;
        }
    }
}

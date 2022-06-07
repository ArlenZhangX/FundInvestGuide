using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using CrawlingFundsRank.model;

namespace CrawlingFundsRank.service
{
    /// <summary>
    /// 获取基金的历史净值
    /// </summary>
    public class DailyFundsService
    {
        private FundAnalisysService fundAnalisys;
        private WebService webService;

        public DailyFundsService()
        {
            webService = new WebService();
        }

        public InvestContent GetFundInfo(string fundId,int fundRate=3)
    {
            try
            {
                if (string.IsNullOrEmpty(fundId))
                {
                    return new InvestContent();
                }

                //获取评级、类型

                //"type_desc":"指数型","rating_desc":"三星基金"
                var url = $@"https://danjuanapp.com/funding/{fundId}";
                var apiContent = webService.GetContentByUrl(url);

                Regex r = new Regex("(?<=type_desc\":\").*?(?=\",\"rating_source)");
                var fundType = r.Match(apiContent).Value;
                var i = fundType.IndexOf("\"");
                if (i>0)
                {
                    fundType = fundType.Substring(0, i);
                }
                r = new Regex("(?<=rating_desc\":\").*?(?=\",\"follower_)");
                var fundRateStr = r.Match(apiContent).Value;
                var fundRateInt = 0;

                if (fundRateStr.Equals("三星基金"))
                {
                    fundRateInt = 3;
                }
                else if (fundRateStr.Equals("四星基金"))
                {
                    fundRateInt = 4;
                }
                else if (fundRateStr.Equals("五星基金"))
                {
                    fundRateInt = 5;
                }

                if (fundRateInt<fundRate)
                {
                    return new InvestContent();
                }

                url = $@"http://fund.eastmoney.com/pingzhongdata/{fundId}.js";
                apiContent = webService.GetContentByUrl(url);

                var start = apiContent.IndexOf("var fS_name = \"") + 15; //这里出来的Index从var算起，因此要去除
                var end = apiContent.IndexOf("\";var fS_code");
                var getLength = apiContent.Length - start - (apiContent.Length - end);
                var fundName = apiContent.Substring(start, getLength);

                start = apiContent.IndexOf("var Data_netWorthTrend") + 27; //同上
                end = apiContent.IndexOf("/*累计净值走势*/") - 3; //同上
                getLength = apiContent.Length - start - (apiContent.Length - end);
                var newT = apiContent.Substring(start, getLength);

                var strArr = newT.Split("},{");

                List<FundData> dataList = new List<FundData>();
                foreach (var item in strArr)
                {
                    var list = item.Split(",");
                    var d = new FundData();
                    foreach (var f in list)
                    {
                        var index = f.IndexOf(":") + 1;
                        if (f.Contains("\"x\""))
                        {
                            d.X = long.Parse(f.Substring(index));
                        }
                        else if (f.Contains("\"y\""))
                        {
                            d.Y = double.Parse(f.Substring(index));
                        }
                        else if (f.Contains("\"equityReturn\""))
                        {
                            d.EquityReturn = double.Parse(f.Substring(index));
                        }


                    }
                    dataList.Add(d);
                }

                fundAnalisys = new FundAnalisysService(new InvestContent() { 
                    FundId = fundId,
                    FundName = fundName,
                    FundType = fundType,
                    FundRate = fundRateInt,
                    DataList = dataList
                }, dataList);
                return fundAnalisys.Do();
            }
            catch (Exception)
            {
                return new InvestContent();
            }
        }
    }

}


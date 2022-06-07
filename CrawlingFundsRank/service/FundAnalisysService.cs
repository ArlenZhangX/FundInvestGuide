using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrawlingFundsRank.model;

namespace CrawlingFundsRank.service
{
    /// <summary>
    /// 分析基金的历史净值
    /// </summary>
    public class FundAnalisysService
    {
        private InvestContent investContent;

        public FundAnalisysService(string _fundId, string _fundName, List<FundData> _dataList)
        {
            investContent = new InvestContent();
            investContent.FundId = _fundId;
            investContent.FundName= _fundName;
        }

        public FundAnalisysService(InvestContent _investContent, List<FundData> _dataList)
        {
            investContent = _investContent;
        }

        public InvestContent Do()
        {
            try
            {
                if (investContent.DataList.Where(i => i.Time < DateTime.Now.AddYears(-3)).Count()<=0)
                {
                    return investContent;
                }
                var dataList = investContent.DataList;

                //获取过去一年的交易记录
                dataList = dataList.Where(i => i.Time > DateTime.Now.AddYears(-1)).ToList();
                //测试获得两年前可投资项目
                //var buyDateTime = DateTime.Now.AddYears(-2);
                //dataList = dataList.Where(i => i.Time < buyDateTime && i.Time > buyDateTime.AddYears(-1)).ToList();


                //获取最低、最高值和当前值
                investContent.LowestValue = dataList.Min(i => i.Y) + dataList.Min(i => i.Y) * 0.01; //带权重0.01
                investContent.HighestValue = dataList.Max(i => i.Y) - dataList.Min(i => i.Y) * 0.01; //带权重0.01
                investContent.CurrentValue = dataList.OrderByDescending(i => i.Time).FirstOrDefault().Y;

                var currProfitLange = investContent.HighestValue - investContent.CurrentValue;

                //收益太低不投资
                if (investContent.HighestProfit <= 0.15)
                {
                    return investContent;
                }
                else
                {
                    investContent.IsInvest = true;
                }

                //生成止盈数据
                    investContent.LowSellValue = currProfitLange * 0.60 + investContent.CurrentValue;
                    investContent.LowSellProfit = (investContent.LowSellValue - investContent.CurrentValue) / investContent.CurrentValue;
                    investContent.HighSellValue = currProfitLange * 0.80+ investContent.CurrentValue;
                    investContent.HighSellProfit = (investContent.HighSellValue - investContent.CurrentValue) / investContent.CurrentValue;

                return investContent;
            }
            catch (Exception)
            {
                    return investContent;
            }
           
        }
    }
}

using CrawlingFundsRank.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrawlingFundsRank.service
{
    public class GetHistoryValueService
    {
        public List<TestProfitModel> GetHistoryProfit(string fundId,double currentValue) {
            var buyDateTime = DateTime.Now.AddYears(-3);
            DailyFundsService dailyFundsService = new DailyFundsService();
            var s = dailyFundsService.GetFundInfo(fundId,0); 
            if (currentValue == 0.0f)
            {
                currentValue = s.DataList.OrderByDescending(i => i.Time).FirstOrDefault().Y;
            }
            var dataList = s.DataList.Where(i => i.Time > buyDateTime && i.Time < DateTime.Now).ToList();
            var buyValue = currentValue;

            dataList = dataList.OrderBy(i => i.Time).ToList();
            List<TestProfitModel> l = new List<TestProfitModel>();
            foreach (var item in dataList)
            {
                TestProfitModel testProfitModel = new TestProfitModel() { BuyValue = buyValue, FundId = s.FundId, FundName = s.FundName };
                testProfitModel.SellValue = item.Y;
                testProfitModel.Time = item.Time;
                l.Add(testProfitModel);
            }

            l = l.OrderByDescending(i => i.Profit).ToList();
            return l;
        }
    }
}

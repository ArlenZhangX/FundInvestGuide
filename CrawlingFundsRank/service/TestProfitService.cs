using CrawlingFundsRank.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrawlingFundsRank.service
{
    public class TestProfitService
    {
        public List<TestProfitModel> GetTest(string id)
        {
            var buyDateTime = DateTime.Now.AddYears(-2);
            DailyFundsService dailyFundsService = new DailyFundsService();
            var s = dailyFundsService.GetFundInfo(id);
            var dataList = s.DataList.Where(i => i.Time > buyDateTime && i.Time< DateTime.Now).ToList();
            dataList = dataList.OrderBy(i=>i.Time).ToList();
            var buyValue = dataList.FirstOrDefault().Y;
            List<TestProfitModel> l = new List<TestProfitModel>() ;
            foreach (var item in dataList)
            {
                TestProfitModel testProfitModel = new TestProfitModel() { BuyValue = buyValue ,FundId = s.FundId,FundName=s.FundName};
                testProfitModel.SellValue = item.Y;
                testProfitModel.Time = item.Time;
                l.Add(testProfitModel);
                    }

            return l;
        }
    }
}

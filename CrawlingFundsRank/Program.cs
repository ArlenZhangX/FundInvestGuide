using System;
using System.Collections.Generic;
using CrawlingFundsRank.helper;
using CrawlingFundsRank.Interface;
using CrawlingFundsRank.model;
using CrawlingFundsRank.service;
using System.Linq;

namespace CrawlingFundsRank
{
    class Program
    {
        static ExcelHelper excelHelper = new ExcelHelper();
        static void Main(string[] args)
        {
            try
            {
                //GetHistoryProfit("110003");
                TestProfit("110003");
                //GetFundList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 获取可以投资的基金列表
        /// </summary>
        public static void GetFundList() {
            IGetFundListInterface getFundListInterface;
            //创建时可以传入指数、股票、混合用于检索
            getFundListInterface = new GetFundListFromRankService();
            var fundList = getFundListInterface.GetFundList();

            getFundListInterface = new DanJuanGetFundList();
            fundList.AddRange(getFundListInterface.GetFundList());
            fundList= fundList.Distinct().ToList();
            DailyFundsService dailyFunds = new DailyFundsService();
            List<InvestContent> investList = new List<InvestContent>();
            foreach (var item in fundList)
            {
                var i = dailyFunds.GetFundInfo(item);
                if (i.IsInvest)
                {
                    investList.Add(i);
                }
            }

            excelHelper.ExportRankExcel(investList);
        }

        /// <summary>
        /// 模拟2年前投资
        /// </summary>
        /// <param name="id"></param>
        public static void TestProfit(string id) {
            TestProfitService testProfitService = new TestProfitService();
        var t = testProfitService.GetTest(id);
        excelHelper.ExportTestProfit(t);
        }
        /// <summary>
        /// 查看当前净值在过去3年里的位置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currentValue"></param>
        public static void GetHistoryProfit(string id,double currentValue=0.0f)
        {
            GetHistoryValueService historyValueService = new GetHistoryValueService();
            var t = historyValueService.GetHistoryProfit(id, currentValue);
            excelHelper.ExportTestProfit(t);
        }
    }
}

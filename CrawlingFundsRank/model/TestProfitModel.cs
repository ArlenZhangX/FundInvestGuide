using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlingFundsRank.model
{
    public class TestProfitModel
    {
        public string FundId { get; set; }
        public string FundName { get; set; }
        public double BuyValue { get; set; }
        public double SellValue { get; set; }
        public double Profit { get {
                return (SellValue - BuyValue) / BuyValue;
            } }
        public DateTime Time { get; set; }
    }
}

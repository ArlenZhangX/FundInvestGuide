using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlingFundsRank.model
{
    public class InvestContent
    {
        public bool IsInvest { get; set; } = false;
        public string FundId { get; set; }
        public string FundName { get; set; }
        public string FundType { get; set; }
        public int FundRate { get; set; }
        public double CurrentValue { get; set; }
        public double HighestValue { get; set; }
        public double LowestValue { get; set; }

        public double LowSellValue { get; set; }
        public double LowSellProfit { get; set; }
        public double HighSellValue { get; set; }
        public double HighSellProfit { get; set; }

        /// <summary>
        ///最高收益 = (最高净值-最高投资净值)/最高投资净值
        /// </summary>
        public double HighestProfit {
            get {
                return (HighestValue - CurrentValue) / CurrentValue;
            }
        }

        public List<FundData> DataList { get; set; }= new List<FundData>();
    }
}

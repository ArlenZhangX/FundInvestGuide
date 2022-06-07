using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlingFundsRank.model
{
    public class FundData
    {
        //时间戳
        public long X { get; set; }

        public DateTime Time
        {
            get
            {
                TimeSpan timeSub = TimeSpan.FromMilliseconds(X);
                // DateTime.UnixEpoch对应的时间的时间戳为0
                return DateTime.UnixEpoch.Add(timeSub).AddHours(8);
            }
        }
        //净值
        public double Y { get; set; }
        //变化
        public double EquityReturn { get; set; }
    }
}

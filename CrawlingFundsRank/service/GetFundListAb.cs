using System;
using System.Collections.Generic;
using System.Text;
using CrawlingFundsRank.Interface;

namespace CrawlingFundsRank.service
{
    public abstract class GetFundListAb : IGetFundListInterface
    {
        internal string Url { get; set; } = string.Empty;

        internal WebService webService = new WebService();

        public abstract List<string> GetFundList();
    }
}

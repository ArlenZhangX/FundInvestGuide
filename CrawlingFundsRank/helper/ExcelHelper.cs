using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using CrawlingFundsRank.model;
using System.Linq;

namespace CrawlingFundsRank.helper
{
    public class ExcelHelper
    {
        public void ExportRankExcel(List<InvestContent> list)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();//实例化XSSF
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet();//创建一个sheet

            IRow frowO = sheet.CreateRow(0);//添加一行（第一行为表头）
            frowO.CreateCell(0).SetCellValue("基金名");
            frowO.CreateCell(1).SetCellValue("基金号");
            frowO.CreateCell(2).SetCellValue("基金类型");
            frowO.CreateCell(3).SetCellValue("基金评级");
            frowO.CreateCell(4).SetCellValue("当前净值");
            frowO.CreateCell(5).SetCellValue("最低净值");
            frowO.CreateCell(6).SetCellValue("最高净值");
            frowO.CreateCell(7).SetCellValue("低收益出售净值（根据当前净值计算");
            frowO.CreateCell(8).SetCellValue("低收益出售获利%");
            frowO.CreateCell(9).SetCellValue("高收益出售净值（根据当前净值计算");
            frowO.CreateCell(10).SetCellValue("高收益出售获利%");

            //循环添加list中的内容放到表格里
            var i = 0;
            foreach (var item in list)
            {
                IRow frow1 = sheet.CreateRow(i + 1);//第一行已经有表头了，所以从第二行开始
                frow1.CreateCell(0).SetCellValue(item.FundName);
                frow1.CreateCell(1).SetCellValue(item.FundId);
                frow1.CreateCell(2).SetCellValue(item.FundType);
                frow1.CreateCell(3).SetCellValue(item.FundRate.ToString());
                frow1.CreateCell(4).SetCellValue(Math.Round(item.CurrentValue, 3));
                frow1.CreateCell(5).SetCellValue(Math.Round(item.LowestValue, 3));
                frow1.CreateCell(6).SetCellValue(Math.Round(item.HighestValue, 3));
                frow1.CreateCell(7).SetCellValue(Math.Round(item.LowSellValue, 3));
                frow1.CreateCell(8).SetCellValue(Math.Round( item.LowSellProfit * 100, 3) );
                frow1.CreateCell(9).SetCellValue(Math.Round(item.HighSellValue, 3));
                frow1.CreateCell(10).SetCellValue(Math.Round(item.HighSellProfit * 100, 3));
                i++;
            }

            //设置宽度
            for (int f = 0; f< list.Count;f++)
            { sheet.SetColumnWidth(f, 256 * 12); }

            //保存路径（提前在E盘下创建Excel文件夹）
            string savaFileName ="E:\\" + "Excel\\" + "可投资基金列表"+DateTime.Now.ToString("yyddMMhhmmss") + ".xlsx";
            try
            {
                using (FileStream fs = new FileStream(savaFileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);//写入文件
                    workbook.Close();//关闭流
                }
            }
            catch (Exception ex)
            {
                workbook.Close();
            }
        }


        public void ExportTestProfit(List<TestProfitModel> list)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();//实例化XSSF
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet();//创建一个sheet

            IRow frowO = sheet.CreateRow(0);//添加一行（第一行为表头）
            frowO.CreateCell(0).SetCellValue("基金号");
            frowO.CreateCell(1).SetCellValue("基金名");
            frowO.CreateCell(2).SetCellValue("时间");
            frowO.CreateCell(3).SetCellValue("买入净值");
            frowO.CreateCell(4).SetCellValue("卖出净值");
            frowO.CreateCell(5).SetCellValue("卖出收益%");

            //循环添加list中的内容放到表格里
            var i = 0;
            foreach (var item in list)
            {
                IRow frow1 = sheet.CreateRow(i + 1);//第一行已经有表头了，所以从第二行开始
                frow1.CreateCell(0).SetCellValue(item.FundId);
                frow1.CreateCell(1).SetCellValue(item.FundName);
                frow1.CreateCell(2).SetCellValue(item.Time.ToString("d"));
                frow1.CreateCell(3).SetCellValue(item.BuyValue);
                frow1.CreateCell(4).SetCellValue(item.SellValue);
                frow1.CreateCell(5).SetCellValue(Math.Round(item.Profit * 100, 3));
                i++;
            }

            //设置宽度
            for (int f = 0; f < list.Count; f++)
            { sheet.SetColumnWidth(f, 256 * 12); }

            //保存路径（提前在E盘下创建Excel文件夹）
            string savaFileName = "E:\\" + "Excel\\" + "历史净值"+list.FirstOrDefault().FundName+ list.Min(i=>i.Time).ToString("d") + DateTime.Now.ToString("mmss") + ".xlsx";
            try
            {
                using (FileStream fs = new FileStream(savaFileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);//写入文件
                    workbook.Close();//关闭流
                }
            }
            catch (Exception ex)
            {
                workbook.Close();
            }
        }
    }
}
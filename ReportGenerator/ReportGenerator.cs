using System.Text;
using TradeReport.Model;

namespace TradeReport.ReportGenerator
{
    internal class ReportGenerator
    {
        private List<Trade> trades;

        public ReportGenerator(List<Trade> trades)
        {
            this.trades = trades;
        }

        public string GenerateReport(string productType, string brokerName, DateTime tradeDate)
        {
            List<Trade> filteredTrades = trades;

            if (productType != null)
            {
                filteredTrades = filteredTrades.Where(trade => trade.TradeReference.Split('-')[1] == productType).ToList();
            }

            if (!string.IsNullOrEmpty(brokerName))
            {
                filteredTrades = filteredTrades.Where(trade => trade.Broker == brokerName).ToList();
            }

            if (tradeDate != null && tradeDate != DateTime.MinValue)
            {
                filteredTrades = filteredTrades.Where(trade => trade.TradeDate == tradeDate).ToList();
            }

            string reportHeader = GetReportHeader(productType);

            StringBuilder reportBuilder = new StringBuilder();
            reportBuilder.Append(reportHeader);

            foreach (Trade trade in filteredTrades)
            {
                string tradeLine = GetTradeLine(trade, productType);
                reportBuilder.AppendLine(tradeLine);
            }

            return reportBuilder.ToString();
        }

        private string GetReportHeader(string productType)
        {
            string reportHeader = "";

            switch (productType)
            {
                case "FWD":
                    reportHeader = "tradeRef,productId,productName,tradeDate,qty,buysell,price\n";
                    break;
                case "BF":
                    reportHeader = "tradeRef,productId,productName,tradeDate,qty,buysell,price,underlyingAsset,expiryDate\n";
                    break;
                case "IRS":
                    reportHeader = "tradeRef,productId,productName,tradeDate,qty,buysell,price,fixedRate,notional,effectiveDate,endDate\n";
                    break;
                case "OPT":
                    reportHeader = "tradeRef,productId,productName,tradeDate,qty,buysell,price,optionType,strikePrice,expiryDate\n";
                    break;
                case "CDS":
                    reportHeader = "tradeRef,productId,productName,tradeDate,qty,buysell,price,underlyingAsset,notional,effectiveDate,endDate\n";
                    break;
                default:
                    reportHeader = "tradeRef,productId,productName,tradeDate,qty,buysell,price\n";
                    break;
                    //throw new ArgumentException("Invalid product type");
            }

            return reportHeader;
        }

        private string GetTradeLine(Trade trade, string productType)
        {
            string tradeLine = $"{trade.TradeReference},{trade.ProductId},{trade.ProductName},{trade.TradeDate.ToString("yyyyMMdd")},{trade.Quantity},{trade.BuySellIndicator},{trade.Price}";

            if (productType == "BF")
            {
                tradeLine += $",{trade.UnderlyingAsset},{trade.ExpiryDate?.ToString("yyyyMMdd")}";
            }
            else if (productType == "IRS")
            {
                tradeLine += $",{trade.FixedRate},{trade.Notional},{trade.EffectiveDate?.ToString("yyyyMMdd")},{trade.EndDate?.ToString("yyyyMMdd")}";
            }
            else if (productType == "OPT")
            {
                tradeLine += $",{trade.OptionType},{trade.StrikePrice},{trade.ExpiryDate?.ToString("yyyyMMdd")}";
            }
            else if (productType == "CDS")
            {
                tradeLine += $",{trade.UnderlyingAsset},{trade.Notional},{trade.EffectiveDate?.ToString("yyyyMMdd")},{trade.EndDate?.ToString("yyyyMMdd")}";
            }

            return tradeLine;
        }
    }
}

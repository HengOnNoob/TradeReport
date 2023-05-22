using TradeReport.Model;

namespace TradeReport.ReportGenerator
{
    internal class TradeRepository
    {
        private string filePath;

        public TradeRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public List<Trade> LoadTrades()
        {
            List<Trade> trades = new List<Trade>();

            using (var reader = new StreamReader(filePath))
            {
                // Read the header line
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var trade = new Trade
                    {
                        TradeReference = values[0],
                        ProductId = int.Parse(values[1]),
                        ProductName = values[2],
                        TradeDate = DateTime.ParseExact(values[3], "yyyyMMdd", null),
                        Quantity = int.Parse(values[4]),
                        BuySellIndicator = values[5],
                        Price = decimal.Parse(values[6]),
                        UnderlyingAsset = values[7],
                        ExpiryDate = string.IsNullOrEmpty(values[8]) ? (DateTime?)null : DateTime.ParseExact(values[8], "yyyyMMdd", null),
                        OptionType = values[9],
                        StrikePrice = string.IsNullOrEmpty(values[10]) ? (decimal?)null : decimal.Parse(values[10]),
                        FixedRate = string.IsNullOrEmpty(values[11]) ? (decimal?)null : decimal.Parse(values[11]),
                        Notional = string.IsNullOrEmpty(values[12]) ? (decimal?)null : decimal.Parse(values[12]),
                        EffectiveDate = string.IsNullOrEmpty(values[13]) ? (DateTime?)null : DateTime.ParseExact(values[13], "yyyyMMdd", null),
                        EndDate = string.IsNullOrEmpty(values[14]) ? (DateTime?)null : DateTime.ParseExact(values[14], "yyyyMMdd", null),
                        Broker = values[15]
                    };
                    trades.Add(trade);
                }

                return trades;
            }
        }
    }
}

namespace TradeReport.Model
{
    public class Trade
    {
        public string TradeReference { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime TradeDate { get; set; }
        public int Quantity { get; set; }
        public string BuySellIndicator { get; set; }
        public decimal Price { get; set; }
        public string UnderlyingAsset { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string OptionType { get; set; }
        public decimal? StrikePrice { get; set; }
        public decimal? FixedRate { get; set; }
        public decimal? Notional { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Broker { get; set; }
    }
}

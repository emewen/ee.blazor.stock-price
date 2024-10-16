namespace ee.blazor.stock_price.Data
{
    public class Stock
    {
        public string? id { get; set; }
        public string? stockId { get; set; }
        public string? symbol { get; set; }
        public decimal price { get; set; }
        public string? timestamp { get; set; }
        public string? range { get; set; }
    }
}

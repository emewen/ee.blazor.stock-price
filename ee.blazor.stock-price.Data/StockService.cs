using Microsoft.Azure.Cosmos;

namespace ee.blazor.stock_price.Data
{
    public class StockService
    {
        private CosmosContainerClient _cosmosContainerClient;

        public StockService(CosmosContainerClient cosmosContainerClient) 
        {
            _cosmosContainerClient = cosmosContainerClient;
        }

        public async Task<List<Stock>> GetStocks()
        {
            List<Stock> stocks = new List<Stock>();
            try
            {
                var container = _cosmosContainerClient.ContainerClient();
                var sqlQuery = "SELECT * FROM c";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
                FeedIterator<Stock> queryResultSetIterator = container.GetItemQueryIterator<Stock>(queryDefinition);

                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<Stock> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (Stock stock in currentResultSet)
                    {
                        stocks.Add(stock);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return stocks;
        }
    }
}

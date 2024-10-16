using Microsoft.Azure.Cosmos;

namespace ee.blazor.stock_price.Data
{
    public class CosmosContainerClient : IDisposable
    {
        private readonly string CosmosDBAccountUri = "https://cosmosdb-default-account.documents.azure.com:443/";
        private readonly string CosmosDBAccountPrimaryKey = "P17fZ77OjGhvjRyuMHXh6R98wXwp5Co8SLnHt6eb6vaufxsL2HipXVaj6jwJzUfRTtx6G5172XnKACDbwbRqMw==";
        private readonly string CosmosDbName = "StocksDB";
        private readonly string CosmosDbContainerName = "StocksContainer";
        private readonly CosmosClient _cosmosDbClient;

        public CosmosContainerClient()
        {
            _cosmosDbClient = new CosmosClient(CosmosDBAccountUri, CosmosDBAccountPrimaryKey);
        }

        public Container ContainerClient()
        {
            Container containerClient = _cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
            return containerClient;
        }

        public void Dispose()
        {
            if (_cosmosDbClient != null)
            {
                _cosmosDbClient.Dispose();
            }
        }
    }
}

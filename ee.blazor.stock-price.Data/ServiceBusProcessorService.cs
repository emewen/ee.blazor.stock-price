using Azure.Messaging.ServiceBus;

namespace ee.blazor.stock_price.Data
{
    public class ServiceBusProcessorService
    {
        private readonly ServiceBusClient _serviceBusClient;

        public ServiceBusProcessorService(ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }

        public ServiceBusProcessor GetProcessorForTopicAndSubscription(string queueName)
        {
            return _serviceBusClient.CreateProcessor(queueName);
        }
    }
}

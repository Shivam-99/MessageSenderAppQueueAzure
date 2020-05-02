using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
namespace MessagingApp
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://messagesapp.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Rt2n2FWz2GbSMuzqTbx6tVcvpxG/2SOzZoizUd13rkk=";
        const string QueueName = "myqueue";
        static IQueueClient queueClient;
        public static async Task Main(string[] args)
        {
            const int numberOfMessages = 10;
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            await SendMessages(numberOfMessages);

            Console.ReadKey();

            await queueClient.CloseAsync();
        }
        static async Task SendMessages(int numberOfMessagesToSend)
        {
            for (int i = 0; i < numberOfMessagesToSend; i++)
            {
                string messageBody = $"Customer {i}";
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                Console.WriteLine($"Sending message: {messageBody}"); 
                await queueClient.SendAsync(message);
                
            }
        }
    }
}

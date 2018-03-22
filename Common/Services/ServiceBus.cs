using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Services
{
    public class ServiceBus
    {
        const string ServiceBusConnectionString = "Endpoint=sb://erfahrungsaustausch-alexa.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Ts7YmMJ6rnChG+sFKYYKc9Kc8EAcRMqPxtxR6xmlLL0=";

        private readonly IQueueClient client;
        private readonly Action<string> handler;
        public ServiceBus(string queueName, Action<string> handler = null)
        {
            this.client = new QueueClient(ServiceBusConnectionString, queueName);
            this.handler = handler;

            if (handler != null)
            {
                RegisterMessageHandler();
            }
        }

        public async Task SendMessage(string text)
        {
            var message = new Message(Encoding.UTF8.GetBytes(text));

            // Send the message to the queue
            await client.SendAsync(message);

            await client.CloseAsync();
        }

        public async Task CloseAsync()
        {
            await client.CloseAsync();
        }

        private void RegisterMessageHandler()
        {
            // Configure the message handler options in terms of exception handling, number of concurrent messages to deliver, etc.
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // Maximum number of concurrent calls to the callback ProcessMessagesAsync(), set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether the message pump should automatically complete the messages after returning from user callback.
                // False below indicates the complete operation is handled by the user callback as in ProcessMessagesAsync().
                AutoComplete = false
            };

            // Register the function that processes messages.
            client.RegisterMessageHandler(HandleMessage, messageHandlerOptions);
        }

        private async Task HandleMessage(Message message, CancellationToken token)
        {
            var body = Encoding.UTF8.GetString(message.Body);
            // Process the message.
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{body}");

            // Complete the message so that it is not received again.
            // This can be done only if the queue Client is created in ReceiveMode.PeekLock mode (which is the default).
            await client.CompleteAsync(message.SystemProperties.LockToken);

            // Note: Use the cancellationToken passed as necessary to determine if the queueClient has already been closed.
            // If queueClient has already been closed, you can choose to not call CompleteAsync() or AbandonAsync() etc.
            // to avoid unnecessary exceptions.

            this.handler(body);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
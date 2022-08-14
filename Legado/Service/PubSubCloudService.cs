using Google.Cloud.PubSub.V1;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boletador.Service
{
    public static class PubSubCloudService
    {
        public static async Task<List<PubsubMessage>> getFila(SubscriptionName subscriptionName)
        {
            List<PubsubMessage> receivedMessages = new List<PubsubMessage>();

            try
            {
                // Conectando a inscrição
                SubscriberClient subscriber = await SubscriberClient.CreateAsync(subscriptionName);

                // Consumindo mensagens e montando objetos
                await subscriber.StartAsync((msg, cancellationToken) =>
                {
                    receivedMessages.Add(msg);
                    subscriber.StopAsync(TimeSpan.FromSeconds(15));

                    // Return Reply.Ack to indicate this message has been handled.
                    return Task.FromResult(SubscriberClient.Reply.Ack);
                });
            }
            catch
            {
                throw;
            }

            return receivedMessages;
        }
    }
}
using Google.Cloud.PubSub.V1;
using System;
using System.Threading.Tasks;

namespace Boletador.Service
{
    public static class PubSubCloundService
    {
        public static async Task<string> Publish(TopicName topicName, string mensagem)
        {
            try
            {
                PublisherClient publisher = await PublisherClient.CreateAsync(topicName);

                // Publicando mensagem no tópico/fila
                string messageId = await publisher.PublishAsync(mensagem);

                // Finalizando o serviço
                await publisher.ShutdownAsync(TimeSpan.FromSeconds(15));

                return messageId;
            }
            catch
            {
                throw;
            }
        }
    }
}
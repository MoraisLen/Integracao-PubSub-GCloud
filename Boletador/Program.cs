using Boletador.Model;
using Boletador.Model.Enum;
using Boletador.Service;
using Google.Cloud.PubSub.V1;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Boletador
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                // Pegando credenciais do Google
                System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "GoogleCloudKey.json");

                // Informações do tópico/fila
                TopicName topicName = new TopicName("IdDoProjeto", "NomeDoTopico");

                for (; ; )
                {
                    BoletoModel newBoleto = new BoletoModel()
                    {
                        NomeContraparte = "Nome da contraparte",
                        CPF = "00000000000",
                        ValorGeral = 12500,
                        TipoBoleto = TipoBoletoEnum.RendaFixa
                    };

                    string jsonBoleto = JsonConvert.SerializeObject(newBoleto);
                    string messageId = await PubSubCloudService.Publish(topicName, jsonBoleto);

                    Console.WriteLine($"Processo {messageId} => Enviado.\n");
                    await Task.Delay(5000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Houve algum erro com o envio. Por favor, contate o administrador. Erro => {ex.Message}.");
            }
        }
    }
}
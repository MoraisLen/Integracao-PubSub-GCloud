using Boletador.Model;
using Boletador.Service;
using Google.Cloud.PubSub.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Legado
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                // Pegando credenciais do Google
                System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "GoogleCloudKey.json");

                // Informações da inscrição
                SubscriptionName subscriptionName = new SubscriptionName("IdDoProjeto", "NomeDaAssinatura");

                Console.WriteLine("Iniciando monitor de integração\n");

                for (; ; )
                {
                    List<PubsubMessage> mensagens = await PubSubCloudService.getFila(subscriptionName);

                    Parallel.ForEach(mensagens, async mensagem =>
                    {
                        string jsonBoleto = mensagem.Data.ToStringUtf8();
                        BoletoModel boleto = JsonConvert.DeserializeObject<BoletoModel>(jsonBoleto);

                        Console.WriteLine($"" +
                            $"Nova solicitação de integração:\n" +
                            $"Contraparte: {boleto.NomeContraparte}\n" +
                            $"CPF: {boleto.CPF}\n" +
                            $"Valor Geral: {boleto.ValorGeral}\n" +
                            $"Tipo: {boleto.TipoBoleto}\n\n");

                        await Task.Delay(5000);

                        Console.WriteLine($"Processo {mensagem.MessageId} finalizado.\n");
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Houve algum erro com o monitor. Por favor, contatar o administrador. Erro => {ex.Message}.\n");
            }
        }
    }
}
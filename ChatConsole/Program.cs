using System.Net.Http.Json;
using ChatApi.DTOs.Conversation;
using ChatApi.DTOs.Message;
using ChatApi.Models;

namespace BenimProjem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            HttpClient client = new();
            client.BaseAddress = new Uri("http://localhost:5273/");

            Console.Write("İsminizi giriniz: ");

            string userName = Console.ReadLine()!;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Hos geldiniz {userName}");
                Console.WriteLine();
                Console.WriteLine("1- Sohbetleri Listele");
                Console.WriteLine("2- Yeni Sohbet Oluştur");
                Console.WriteLine("3- Sohbete Gir");
                Console.WriteLine("0- Çıkış");
                Console.Write("\nSeçiminiz: ");
                string? secim = Console.ReadLine();
                switch (secim)
                {
                    case "1":
                        await Listele(client);
                        break;

                    case "2":
                        await SohbetOlustur(client);
                        break;
                    case "3":
                        await SohbeteGir(client, userName);
                        break;
                    case "0":
                        return;
                }
            }
            static async Task Listele(HttpClient client)
            {
                var conversations = await client.GetFromJsonAsync<List<ConversationList>>
                ("api/conversation");
                Console.Clear();
                if (conversations != null)
                {
                    foreach (var item in conversations)
                    {
                        Console.WriteLine($"{item.Id}-{item.Title}");
                    }
                    Console.WriteLine();
                    Console.Write("Devam etmek için Enter...");
                    Console.ReadLine();
                }
            }
            static async Task SohbetOlustur(HttpClient client)
            {
                Console.WriteLine("sohbet oluşturun");
                string title = Console.ReadLine()!;
                CreateConversationRequest request = new()
                {
                    Title = title
                };
                await client.PostAsJsonAsync("api/conversation", request);
                Console.WriteLine("Sohbet oluşturuldu.");

                Console.ReadLine();
            }


            static async Task SohbeteGir(HttpClient client, string userName)
            {
                int lastMessageId = 0;
                CancellationTokenSource cts = new();

                Console.WriteLine("Sohbet Id:");
                int id = Convert.ToInt32(Console.ReadLine());

                var messages = await client.GetFromJsonAsync<List<MessageResponse>>($"api/conversation/{id}/messages");

                foreach (var message in messages ?? [])
                {
                    Console.WriteLine($"{message.Sender}: {message.Content}");
                    lastMessageId = message.Id;
                }
                Console.WriteLine();

                _ = Task.Run(async () =>
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                       var messages =
                           await client.GetFromJsonAsync<List<MessageResponse>>
                           ($"api/conversation/{id}/messages");

                       foreach (var message in messages!)
                       {
                           if (message.Id > lastMessageId)
                           {
                               Console.WriteLine($"{message.Sender}: {message.Content}");
                               lastMessageId = message.Id;
                           }
                       }

                        await Task.Delay(2000);
                    }

                });



                Console.WriteLine("Mesajınızı yazın.");
                Console.WriteLine("Çıkmak için /exit");

                while (true)
                {
                    string text = Console.ReadLine()!;
                    if (text == "/exit")
                    {
                        cts.Cancel();
                        break;
                    }
                    await client.PostAsJsonAsync($"api/conversation/{id}/messages",
                    new CreateMessageRequest()
                    {
                        Sender = userName,
                        Content = text
                    });
                }
            }
        }
    }
}


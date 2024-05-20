using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class SocialMediaService
{
    private HttpClient httpClient;

    public SocialMediaService()
    {
        httpClient = new HttpClient();
    }

    public async Task<bool> ShareToWhatsApp(string filePath)
    {
        // Send file to WhatsApp API
        // Example code - replace with actual API endpoint and request format
        var response = await httpClient.PostAsync("WhatsAppAPIEndpoint", new StringContent(JsonConvert.SerializeObject(new { filePath })));

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ShareToTelegram(string filePath)
    {
        // Send file to Telegram Bot API
        // Example code - replace with actual bot token and chat ID
        var response = await httpClient.PostAsync($"https://api.telegram.org/bot<YourBotToken>/sendDocument?chat_id=<ChatID>", new StringContent(JsonConvert.SerializeObject(new { document = filePath })));

        return response.IsSuccessStatusCode;
    }
}
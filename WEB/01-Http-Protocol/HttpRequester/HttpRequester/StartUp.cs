namespace HttpRequester
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class StartUp
    {
        public static async Task Main()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://softuni.bg/");
            string result = await response.Content.ReadAsStringAsync();
            File.WriteAllText("index.html", result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace Assignment1.Data
{
    public class CloudPersonService : IPersonService
    {
        // private string uri = "http://localhost:5003";
        private readonly HttpClient client;

        public CloudPersonService()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };

            client = new HttpClient(clientHandler);
        }

        public async Task AddPersonAsync(Adult adult)
        {
            string adultsAsJson = JsonSerializer.Serialize(adult);
            HttpContent content = new StringContent(adultsAsJson,
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage responseMessage = await client.PostAsync("https://localhost:5003/adults", content);
            if (responseMessage.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception($"Error, {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }
        }

        public async Task UpdatePersonAsync(Adult adult)
        {
            string todoAsJson = JsonSerializer.Serialize(adult);
            HttpContent content = new StringContent(todoAsJson,
                Encoding.UTF8,
                "application/json");
            await client.PatchAsync($"https://localhost:5003/adults", content);
        }

        public async Task RemovePersonAsync(int adultId)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync($"https://localhost:5003/adults/{adultId}");
           
            Console.WriteLine("removing adult with id" + adultId);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }
        }

        public async Task<Adult> GetAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:5003/adults");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error");
            }

            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
            List<Adult> result = JsonSerializer.Deserialize<List<Adult>>(message);

            Adult getAdult = result.Find(a => a.Id == id);

            return getAdult;
        }

        public async Task<IList<Adult>> GetAllAsync()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:5003/adults");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error");
            }

            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
            List<Adult> result = JsonSerializer.Deserialize<List<Adult>>(message);

            return result;
        }
    }
}
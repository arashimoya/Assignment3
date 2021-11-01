using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace Assignment1.Data
{
    public class CloudPersonService : IPersonService
    {
        private string uri = "http://localhost:5003";
        private readonly HttpClient client;

        public CloudPersonService()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => {
                return true;
            };

            client = new HttpClient(clientHandler);
        }
        public Task AddPersonAsync(Adult adult)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdatePersonAsync(Adult adult)
        {
            throw new System.NotImplementedException();
        }

        public Task RemovePersonAsync(int AdultId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Adult> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Adult>> GetAllAsync()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:5003/controller");
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
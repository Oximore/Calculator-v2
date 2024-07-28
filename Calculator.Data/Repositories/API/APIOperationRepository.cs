using Calculator.Data.Models;
using System.Net.Http.Json;

namespace Calculator.Data.Repositories.API
{
    public class APIOperationRepository : IOperationRepository
    {
        public readonly HttpClient client;
        private const string url = @"http://localhost:44370/api/operation";

        public APIOperationRepository()
        {
            client = new HttpClient();
        }


        public IEnumerable<Operation> ReadAll()
        {
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (!response.IsSuccessStatusCode)
            {
                return Array.Empty<Operation>();
            }
            return response.Content.ReadFromJsonAsync<IEnumerable<Operation>>().Result;
        }

        public Operation Create(Operation model)
        {
            HttpResponseMessage response = client.PostAsJsonAsync($"{url}", model).Result;
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response.Content.ReadFromJsonAsync<Operation>().Result;
        }

        public void Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result;
        }
    }
}

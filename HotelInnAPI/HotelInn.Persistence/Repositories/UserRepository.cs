using HotelInn.Domain.IRepositories;
using HotelInn.Domain.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HotelInn.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;

        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<User> FindUserAsync(string httpAccessToken)
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(300);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", httpAccessToken);

            string accountDetailsUri = $"{configuration["Jwt:Issuer"]}/Account/Details";
            var httpResponse = await client.GetAsync(accountDetailsUri);
            if (httpResponse != null && httpResponse.IsSuccessStatusCode && httpResponse.Content != null)
            {
                string data = await httpResponse.Content.ReadAsStringAsync();
                User user = JsonConvert.DeserializeObject<User>(data);
                return user;
            }

            return null;
        }
    }
}

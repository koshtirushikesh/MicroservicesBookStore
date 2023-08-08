using BookStore.Order.Entity;
using BookStore.Order.Interface;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BookStore.Order.Service
{
    public class UserServices : IUserServices
    {
        public async Task<UserEntity> GetUserDetails(string jwtToken)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                HttpResponseMessage response = await client.GetAsync("https://localhost:7138/api/User/getUserProfile");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ResponseModel<UserEntity> responseModel = JsonConvert.DeserializeObject<ResponseModel<UserEntity>>(content);
                    return responseModel.Data;
                }
                return null;
            }
        }
    }
}

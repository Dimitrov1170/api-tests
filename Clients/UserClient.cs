using RestSharp;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Clients
{
    public class UserClient : IDisposable
    {
        private readonly RestClient _client;

        public UserClient()
        {
            _client = new RestClient("https://reqres.in/");
        }

        private RestRequest CreateRequest(string endpoint, Method method)
        {
            var request = new RestRequest(endpoint, method);
            request.AddHeader("x-api-key", "reqres-free-v1");
            return request;
        }

        public RestResponse Login(LoginRequest body)
        {
            var request = CreateRequest("api/login", Method.Post);
            request.AddJsonBody(body);
            return _client.Execute(request);
        }

        public RestResponse CreateUser(CreateUserRequest body)
        {
            var request = CreateRequest("api/users", Method.Post);
            request.AddJsonBody(body);
            return _client.Execute(request);
        }

        public RestResponse UpdateUser(int id, CreateUserRequest body)
        {
            var request = CreateRequest($"api/users/{id}", Method.Put);
            request.AddJsonBody(body);
            return _client.Execute(request);
        }

        public RestResponse DeleteUser(int id)
        {
            var request = CreateRequest($"api/users/{id}", Method.Delete);
            return _client.Execute(request);
        }

        public List<User> GetUsers(int page = 1)
        {
            var request = CreateRequest($"api/users?page={page}", Method.Get);
            var response = _client.Execute(request);

            var json = JObject.Parse(response.Content);
            var dataArray = json["data"].ToString();
            return JsonConvert.DeserializeObject<List<User>>(dataArray);
        }
        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}

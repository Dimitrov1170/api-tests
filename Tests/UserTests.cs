using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace Tests
{
    public class UserTests
    {
        private RestClient client;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://reqres.in/");
        }

        [Test]
        public void GetUsers_ShouldReturnListOfUsers()
        {
            var request = new RestRequest("api/users?page=2", Method.Get);
            var response = client.Execute(request);

            Assert.That(response.IsSuccessful, Is.True, "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var json = JObject.Parse(response.Content);
            var dataArray = json["data"].ToString();

            var users = JsonConvert.DeserializeObject<List<User>>(dataArray);

            Assert.That(users.Count, Is.GreaterThan(0));
            Assert.That(users[0].Email, Is.Not.Null.And.Contains("@"));

        }
        [TearDown]
        public void TearDown() 
        {
           client.Dispose();
        }

    }
}

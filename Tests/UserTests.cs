using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json.Linq;

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
            var data = json["data"];

            Assert.That(data.HasValues, Is.True, "No users returned");
        }
        [TearDown]
        public void TearDown() 
        {
           client.Dispose();
        }

    }
}

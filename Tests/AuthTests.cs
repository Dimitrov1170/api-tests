using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using Models;
using System.Threading.Tasks;

namespace Tests
{
    public class AuthTests
    {
        private RestClient client;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://reqres.in/");
        }

        [Test]
        public async Task Login_WithValidCredentials_ShouldReturnToken()
        {
            var loginRequest = new LoginRequest
            {
                Email = "eve.holt@reqres.in",
                Password = "cityslicka"
            };

            var request = new RestRequest("api/login", Method.Post);
            request.AddJsonBody(loginRequest);
            request.AddHeader("x-api-key", "reqres-free-v1");

            var response = await client.ExecuteAsync(request);

            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);

            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessful, Is.True);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Assert.That(loginResponse.Token, Is.Not.Null.And.Not.Empty);
            });
        }

        [Test]
        public async Task Login_WithoutPassword_ShouldReturnError()
        {
            var loginRequest = new LoginRequest
            {
                Email = "eve.holt@reqres.in"
            };

            var request = new RestRequest("api/login", Method.Post);
            request.AddJsonBody(loginRequest);
            request.AddHeader("x-api-key", "reqres-free-v1");

            var response = await client.ExecuteAsync(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessful, Is.False);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
                Assert.That(response.Content, Does.Contain("Missing password"));
            });
        }

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
        }
    }
}

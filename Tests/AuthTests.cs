using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using Models;
using System.Threading.Tasks;
using Clients;

namespace Tests
{
    public class AuthTests
    {
        private UserClient userClient;

        [SetUp]
        public void Setup()
        {
            userClient = new UserClient();
        }

        [Test]
        public async Task Login_WithValidCredentials_ShouldReturnToken()
        {
            var loginRequest = new LoginRequest
            {
                Email = "eve.holt@reqres.in",
                Password = "cityslicka"
            };

            var response = userClient.Login(loginRequest);

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

            var response = userClient.Login(loginRequest);

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
            userClient.Dispose();
        }
    }
}

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
            request.AddHeader("x-api-key", "reqres-free-v1");
            var response = client.Execute(request);

            Assert.That(response.IsSuccessful, Is.True, "Request failed");
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var json = JObject.Parse(response.Content);
            var dataArray = json["data"].ToString();

            var users = JsonConvert.DeserializeObject<List<User>>(dataArray);

            Assert.That(users.Count, Is.GreaterThan(0));
            Assert.That(users[0].Email, Is.Not.Null.And.Contains("@"));

        }

        [Test]
        public async Task CreateUser_ShouldReturnCreatedUserWithId()
        {
            var user = new CreateUserRequest
            {
                Name = "Georgi",
                Job = "QA Engineer"
            };

            var request = new RestRequest("api/users", Method.Post);
            request.AddHeader("x-api-key", "reqres-free-v1");
            request.AddJsonBody(user);

            var response = await client.ExecuteAsync(request);
            var createdUser = JsonConvert.DeserializeObject<CreateUserResponse>(response.Content);

            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessful, Is.True);
                Assert.That(createdUser.Name, Is.EqualTo("Georgi"));
                Assert.That(createdUser.Job, Is.EqualTo("QA Engineer"));
                Assert.That(createdUser.Id, Is.Not.Null.And.Not.Empty);
                Assert.That(createdUser.CreatedAt, Is.Not.Null.And.Not.Empty);
            });
        }

        [Test]
        public async Task UpdateUser_ShouldReturnUpdatedData()
        {
            var user = new CreateUserRequest
            {
                Name = "Georgi",
                Job = "Senior QA"
            };

            var request = new RestRequest("api/users/2", Method.Put);
            request.AddHeader("x-api-key", "reqres-free-v1");
            request.AddJsonBody(user);

            var response = await client.ExecuteAsync(request);
            var updatedUser = JsonConvert.DeserializeObject<CreateUserResponse>(response.Content);

            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessful, Is.True);
                Assert.That(updatedUser.Name, Is.EqualTo("Georgi"));
                Assert.That(updatedUser.Job, Is.EqualTo("Senior QA"));
                Assert.That(updatedUser.UpdatedAt, Is.Not.Null.And.Not.Empty);

            });
        }

        [Test]
        public async Task DeleteUser_ShouldReturnNoContent()
        {
            var request = new RestRequest("api/users/2", Method.Delete);
            request.AddHeader("x-api-key", "reqres-free-v1");

            var response = await client.ExecuteAsync(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessful, Is.True);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
                Assert.That(response.Content, Is.Empty);
            });
        }


        [TearDown]
        public void TearDown() 
        {
           client.Dispose();
        }

    }
}

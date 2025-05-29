using Models;
using Clients;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace Tests
{
    public class UserTests
    {
        private UserClient userClient;

        [SetUp]
        public void Setup()
        {
            userClient = new UserClient();
        }

        [Test]
        public void GetUsers_ShouldReturnListOfUsers()
        {
            var users = userClient.GetUsers(2);

            Assert.That(users.Count, Is.GreaterThan(0));
            Assert.That(users[0].Email, Is.Not.Null.And.Contains("@"));

        }

        [Test]
        public void CreateUser_ShouldReturnCreatedUser()
        {
            var user = new CreateUserRequest
            {
                Name = "Georgi",
                Job = "QA Engineer"
            };

            var response = userClient.CreateUser(user);
            var createdUser = JsonConvert.DeserializeObject<CreateUserResponse>(response.Content);

            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessful, Is.True);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Assert.That(createdUser.Name, Is.EqualTo("Georgi"));
                Assert.That(createdUser.Job, Is.EqualTo("QA Engineer"));
                Assert.That(createdUser.CreatedAt, Is.Not.Null.And.Not.Empty);
            });
        }


        [Test]
        public void UpdateUser_ShouldReturnUpdatedData()
        {
            var user = new CreateUserRequest
            {
                Name = "Georgi",
                Job = "Senior QA"
            };

            var response = userClient.UpdateUser(2, user);
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
        public void DeleteUser_ShouldReturnNoContent()
        {
            var response = userClient.DeleteUser(2);

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
           userClient.Dispose();
        }

    }
}

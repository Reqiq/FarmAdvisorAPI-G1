using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FarmAdvisor.Models.Models;
using System.Net;
namespace Integration_Test
{
    public class UserEndpointTest :IntegrationTest
    {
        [Fact]
        public async Task AddUser_WithValidInput_ReturnsOk()
        {
            //arrange
            var user = new UserRequest{
                name = "anwii",
                phone = "+2519234455445",
                authId = "sdlfhgsje;adf'eriotkwmf",
                email = "test@gmail.com"

            };


            //act
            var response = await httpClient.PostAsJsonAsync("http://localhost:7071/api/AddUserApi", user);
            var createdUser = await response.Content.ReadAsAsync<UserModel>();


            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            createdUser.Name.Should().Be("yoseph");


        }

    }
}

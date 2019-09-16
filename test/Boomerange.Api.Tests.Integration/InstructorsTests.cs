using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Boomerang.Api.Models;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using Newtonsoft.Json;

using Xunit;

namespace Boomerang.Api.Tests.Integration
{
    public class InstructorsTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        public InstructorsTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory =
                webApplicationFactory ?? throw new ArgumentNullException(nameof(webApplicationFactory));
        }

        private readonly WebApplicationFactory<Startup> _webApplicationFactory;

        [Fact]
        public async Task Post_NewInstructor_ShouldCreateInstructor()
        {
            // Arrange
            var client =
                _webApplicationFactory.CreateClient(
                    new WebApplicationFactoryClientOptions {BaseAddress = new Uri("https://localhost:5001")});

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = null;

            try
            {
                response =
                    await client.PostAsJsonAsync(
                        "instructors",
                        new
                        {
                            Name = "Robbie Turner"
                        });

                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.Created);

                response.Headers.Location.Should().NotBeNull();

                // Act
                //response = await client.GetAsync("classes");

                //var value = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

                //value.Should().Be("My Saturday Class");

                //// Assert
                //response.EnsureSuccessStatusCode(); // Status Code 200-299
            }
            finally
            {
                response?.Dispose();
            }
        }

        [Fact]
        public async Task Get_ExistingInstructor_ShouldReturnInstructor()
        {
            // Arrange
            var client =
                _webApplicationFactory.CreateClient(
                    new WebApplicationFactoryClientOptions { BaseAddress = new Uri("https://localhost") });

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = null;

            try
            {
                response =
                    await client.PostAsJsonAsync(
                        "instructors",
                        new
                        {
                            Name = "Robbie Turner"
                        });

                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.Created);

                response.Headers.Location.Should().NotBeNull();

                // Act
                response = await client.GetAsync(response.Headers.Location);

                // Assert
                response.EnsureSuccessStatusCode();

                var value = JsonConvert.DeserializeObject<Instructor>(await response.Content.ReadAsStringAsync());

                value.Name.Should().Be("Robbie Turner");
            }
            finally
            {
                response?.Dispose();
            }

        }
    }
}

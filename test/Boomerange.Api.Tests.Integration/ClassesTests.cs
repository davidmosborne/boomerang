using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Boomerang.Api.Tests.Integration
{
    public class ClassesTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        public ClassesTests(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory =
                webApplicationFactory ?? throw new ArgumentNullException(nameof(webApplicationFactory));
        }

        private readonly WebApplicationFactory<Startup> _webApplicationFactory;

        [Fact]
        public async Task Get_ExistingClass_ShouldReturnClass()
        {
            // Arrange
            var client = _webApplicationFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = null;

            try
            {
                // Act
                response = await client.GetAsync("classes");

                // Assert
                response.EnsureSuccessStatusCode(); // Status Code 200-299
                
            }
            finally
            {
                response?.Dispose();
            }
            
        }
    }
}

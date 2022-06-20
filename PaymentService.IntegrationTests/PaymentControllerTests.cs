using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using PaymentService.Entities;
using PaymentService.IntegrationTests.Filters;
using PaymentService.IntegrationTests.Utils;
using PaymentService.Utils.Contants;

namespace PaymentService.IntegrationTests
{
    public class PaymentControllerTests 
    {
        private HttpClient _client;

        public PaymentControllerTests()
        {
            var factory = CreateFactory();
            _client = factory.CreateDefaultClient();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {MockJwtTokens.GenerateJwtToken()}");
        }

        [Fact]
        public async Task validatetransaction_should_return_true()
        {
            var response = await _client.GetAsync("/api/payments/B2/validate?amount=600");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("true", content);
        }

        [Fact]
        public async Task validatetransaction_should_return_false_when_reference_and_amount_did_not_match()
        {
            var response = await _client.GetAsync("/api/payments/B2/validate?amount=700");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("false", content);
        }

        [Fact]
        public async Task validatetransaction_should_return_false_when_status_is_success()
        {
            var response = await _client.GetAsync("/api/payments/C3/validate?amount=700");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("false", content);
        }

        [Fact]
        public async Task completetransaction_should_return_success()
        {
            var response = await _client.PatchAsync("/api/payments/B2/complete", null);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Payment>(content);
            Assert.NotNull(obj);
            Assert.Equal(PaymentStatusConstant.Success, obj.Status);
        }

        [Fact]
        public async Task completetransaction_should_return_not_found_when_has_invalid_refnumber()
        {
            var response = await _client.PatchAsync("/api/payments/invalid/complete", null);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        private WebApplicationFactory<Program> CreateFactory()
        {
            var factory = new WebApplicationFactory<Program>();

            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            return factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile(configPath);
                });

                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc(opt =>
                    {
                        opt.Filters.Add(new AllowAnonymousFilter());
                        opt.Filters.Add(new UserActionFilter());
                    })
                    .AddNewtonsoftJson()
                    .AddApplicationPart(typeof(Program).Assembly);
                });

            });
        }
    }
}

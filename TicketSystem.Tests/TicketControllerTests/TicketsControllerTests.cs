using Microsoft.AspNetCore.Mvc.Testing;
using TicketSystemAPI;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TicketSystemAPI.Data;
using TaskSystemAPI.Functions.Tickets.Command;
using Newtonsoft.Json;
using System.Text.Unicode;
using System.Text;
using TaskSystemAPI.Functions.Tickets.Query;
using Microsoft.AspNetCore.Authorization.Policy;
using TicketSystem.Tests.Helpers;

namespace TicketSystem.Tests.TicketControllerTests
{
    public class TicketsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;

        public TicketsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DataContext>));
                        services.Remove(dbContextOptions);

                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                        services.AddMvc(options => options.Filters.Add(new FakeUserFilter()));

                        services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("TicketsDB"));
                    });
                })
                .CreateClient();
        }

        [Fact]
        public async Task Delete_ForNonExistingTicket_ReturnNotFound()
        {
            //arrange

            //act 
            var response = await _client.DeleteAsync("/api/tickets/6");
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        } 

        [Fact]
        public async Task GetTickets_WithoutParameters_ReturnsOkResult()
        {
            // arrange 
            
            // act 
            var response = await _client.GetAsync("/api/tickets");

            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }


        [Theory]
        [InlineData("id=5")]
        public async Task GetTicket_WithParameters_ReturnsUnathorizedResult(string queryParam)
        {
            // arrange 

            // act 
            var response = await _client.GetAsync("/api/tickets?" + queryParam);

            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }


        [Fact]
        public async Task CreateTask_WithValidModel_ReturnsOkStatus()
        {
            //arrange
            var model = new CreateTicketCommand()
            {
                TicketName = "Test ticket"
            };

            var httpContent = model.ToJsonHttpContent();
            //act
            var response = await _client.PostAsync("/api/tickets", httpContent);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateTask_WithInvalidModel_ReturnsOkStatus()
        {
            //arrange
            var model = new CreateTicketCommand()
            {
                TicketName = "Test ticketTest ticketTest ticketTest ticketTest ticketTest ticketTest ticketTest ticketTest ticket"
            };

            var httpContent = model.ToJsonHttpContent();
            //act
            var response = await _client.PostAsync("/api/tickets", httpContent);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}

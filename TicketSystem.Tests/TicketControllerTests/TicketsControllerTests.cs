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
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Startup> _factory;

        public TicketsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory
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
                });

                _client = _factory.CreateClient();
        }


        private void SeedTicket(Ticket ticket)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<DataContext>();

            _dbContext.Tickets.Add(ticket);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task Delete_ForTicketOwner_ReturnsOk()
        {
            //arrange
            var ticket = new Ticket()
            {
                AddedByUserId = 1,
                
            };

            SeedTicket(ticket);

            //act 
            var response = await _client.DeleteAsync("/api/tickets/" + ticket.Id);
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_ForAdmin_ReturnsOK()
        {
            //arrange
            var ticket = new Ticket()
            {
                AddedByUserId = 100
            };

            SeedTicket(ticket); 

            //act 
            var response = await _client.DeleteAsync("/api/tickets/" + ticket.Id);
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }


        [Fact]
        public async Task Delete_ForNonExistingTicket_ReturnNotFound()
        {
            //arrange

            //act 
            var response = await _client.DeleteAsync("/api/tickets/997");
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
        [InlineData("5")]
        public async Task GetTicket_ForExistingTicket_ReturnsOk(string id)
        {
            // arrange 

            var ticket = new Ticket()
            {
                Id = 5,
                TicketName = "Test"
            };

            SeedTicket(ticket);

            // act 
            var response = await _client.GetAsync("/api/tickets/" + id);

            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("6")]
        public async Task GetTicket_ForNonExistingTicket_ReturnsNotFound(string id)
        {
            // arrange 

            // act 
            var response = await _client.GetAsync("/api/tickets/" + id);

            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
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
        public async Task CreateTask_WithInvalidModel_ReturnsOkBadRequest()
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

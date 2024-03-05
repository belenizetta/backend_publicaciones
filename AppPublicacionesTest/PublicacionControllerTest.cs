using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using BusinessPublicacion.Interface;
using DataAccess.Interface;
using DataAccess.Models;
using Microsoft.Extensions.DependencyInjection;
using BusinessPublicacion.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using BusinessPublicacion.DTOs;

namespace AppPublicacionesTest
{
    public class PublicacionControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly Mock<IPublicacionService> _publicacionServiceMock = new();
        private readonly Mock<IPublicacionData> _publicacionDataMock = new();
        private readonly Mock<PublicacionContext> _publicacionContextMock = new();

        private readonly WebApplicationFactory<Program> _factory;

        private HttpClient _httpClientMock = null;

        public PublicacionControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        public async Task InitializeAsync()
        {

            var hostBuilder = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(service =>
                    {
                        service.AddSingleton<IPublicacionService>( provider =>
                        {
                            var publicacionData = provider.GetRequiredService<IPublicacionData>();
                            var logger = provider.GetRequiredService<ILogger>();
                            var mapper = provider.GetRequiredService<IMapper>();
                            var arhivosLocal = provider.GetRequiredService<IAlmacenadorArchivos>();
                            return new PublicacionService(mapper, publicacionData, logger, arhivosLocal);
                        });

                    });
                });
            var client = hostBuilder.CreateClient();
            _httpClientMock = client;
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
        [Fact]
        public async Task AddPublicacionOk()
        {
            var publicacionReq = new PublicacionRequest()
            {
                TipoOperacion = "Departamento",
                TipoPropiedad = "Departamento",
                Descripcion = "Venta de Departamento",
                Ambientes = 3,
                Antiguedad = 3,
                Latitud = 4444,
                Longitud = 55555,
                M2 = 400,
                
            };

            var client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(publicacionReq), Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri("http://localhost:7000");
            try
            {
                var response = await client.PostAsync("/api/crear", content);
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            //var returnedJson = await response.Content.ReadAsStringAsync();
            //var returnedSeller = JsonConvert.DeserializeObject<Seller>(returnedJson);
            //Assert.Equal(seller, returnedSeller);

        }
    }
}
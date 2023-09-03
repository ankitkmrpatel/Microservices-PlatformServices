using PlatformService.Dtos;
using System;
using System.Text;
using System.Text.Json;

namespace PlatformService.Services.Http
{
    public class CommandServiceClient : ICommandServiceClient
    {

        //private readonly HttpClient client;
        //private readonly IConfiguration config;
        //public CommandServiceClient(HttpClient client, IConfiguration config)
        //{
        //    this.client = client;
        //    this.config = config;
        //}

        private readonly HttpClient _clientFactory;
        private readonly IConfiguration configuration;
        public CommandServiceClient(IConfiguration configuration, HttpClient clientFactory)
        {
            this.configuration = configuration;
            _clientFactory = clientFactory;
        }

        public PlatformReadDtos GetPlatforms()
        {
            throw new NotImplementedException();
        }

        public async Task SendPlatform(PlatformReadDtos platform)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform),
                encoding: Encoding.UTF8, 
                "application/json");

            var response = await _clientFactory.PostAsync($"{configuration["CommandServiceUrl"]}api/c/Platforms", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Platfrom Entity has been sent to Command Service.");
            }
            else
            {
                Console.WriteLine("Platfrom Entity has failed to sent data to Command Service.");
            }
        }
    }
}

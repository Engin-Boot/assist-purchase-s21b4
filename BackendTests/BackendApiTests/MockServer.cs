using System.Net.Http;
using Microsoft.AspNetCore.Hosting;

namespace BackendTests.BackendApiTests
{
    public class MockServer
    {
        public HttpClient Client { get; private set; }
        private Microsoft.AspNetCore.TestHost.TestServer _server;

        public MockServer()
        {
            SetupClient();
        }

        void SetupClient()
        {
            _server = new Microsoft.AspNetCore.TestHost.TestServer(new WebHostBuilder().UseStartup<Backend.Startup>());
            Client = _server.CreateClient();
        }
    }
}

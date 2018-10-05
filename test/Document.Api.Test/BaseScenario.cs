using Document.Api.Test.Infrastructure;
using Xunit;

namespace Document.Api.Test
{
    public class BaseScenario :  IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected readonly StandardHttpClient client;

        public BaseScenario(CustomWebApplicationFactory<Startup> factory)
        {
            client = new StandardHttpClient(factory.CreateClient());
        }
    }
}

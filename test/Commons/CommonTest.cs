using API_EF_TEST.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API_EF_TEST.Commons
{
    public abstract class CommonTest
    {
        protected static IServiceScope scope;

        protected CommonTest()
        {
            ServiceProvider serviceProvider = ServiceProviderHelper.GenerateServiceProvider();
            scope = serviceProvider.CreateScope();

            InitServices();
        }

        protected abstract void InitServices();

        public static void AssertGetListado(ObjectResult response)
        {
            Assert.NotNull(response);
            Assert.True(response.StatusCode == StatusCodes.Status200OK);
        }

        public static void AssertGetData(ObjectResult response, string message = null)
        {
            if (response.StatusCode == StatusCodes.Status404NotFound)
            {
                Assert.True(response.StatusCode == StatusCodes.Status404NotFound);
                Assert.Contains(message, response.Value?.ToString());
                return;
            }
            var user = response.Value;
            Assert.NotNull(user);
            Assert.True(response.StatusCode == StatusCodes.Status200OK);
        }
    }
}

using API_EF_TEST.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API_EF_TEST
{
    public abstract class CommonTest : IDisposable
    {
        protected static IServiceScope scope;

        public CommonTest()
        {
            ServiceProvider serviceProvider = ServiceProviderHelper.GenerateServiceProvider();
            scope = serviceProvider.CreateScope();

            InitServices();
        }

        public void Dispose()
        {
            scope.Dispose();
        }

        protected abstract void InitServices();

        public static void AssertGetListado(ObjectResult response)
        {
            Assert.NotNull(response);
            Assert.True(response.StatusCode == StatusCodes.Status200OK);
        }

        public static void AssertGetData(ObjectResult response, string? message = null)
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

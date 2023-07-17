using API_EF_TEST.Helper;
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
    }
}

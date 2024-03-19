using API_EF.Controllers;
using API_EF_TEST.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_EF_TEST.Controllers
{
    public class ProductoControllerTest : CommonTest
    {
        private ProductoController productoController;
        protected override void InitServices()
        {
            productoController = (ProductoController)scope.ServiceProvider.GetService(typeof(ProductoController));
        }

        [Fact]
        public void GetProductos_Test()
        {
            var response = (ObjectResult)productoController.Get();
            AssertGetListado(response);
        }

        [Theory]
        [InlineData(30)]
        [InlineData(null)]
        public async Task GetProductoByCodigo_Test(int? codigo)
        {
            if (codigo is null)
            {
                var statusResponse = (StatusCodeResult)await productoController.Get("TEST");
                Assert.True(statusResponse.StatusCode == StatusCodes.Status404NotFound);
                return;

            }
            var response = (ObjectResult)await productoController.Get(string.Format("{0:D8}", codigo));
            AssertGetData(response);
        }
    }
}

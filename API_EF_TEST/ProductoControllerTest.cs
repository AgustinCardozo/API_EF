﻿using API_EF.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_EF_TEST
{
    public class ProductoControllerTest : CommonTest
    {
        private ProductoController productoController;
        protected override void InitServices()
        {
            productoController = (ProductoController)scope.ServiceProvider.GetService(typeof(ProductoController));
        }

        [Fact]
        public async Task GetProductos_Test()
        {
            var response = (ObjectResult)await productoController.Get();
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
            var response = (ObjectResult)await productoController.Get(String.Format("{0:D8}", codigo));
            AssertGetData(response);
        }
    }
}
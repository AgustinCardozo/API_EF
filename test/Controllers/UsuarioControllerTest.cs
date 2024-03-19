using API_EF.Controllers;
using API_EF_TEST.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_EF_TEST.Controllers
{
    public class UsuarioControllerTest : CommonTest
    {
        private UsuarioController usuarioController;
        private const string message = "No se encontro";

        protected override void InitServices()
        {
            usuarioController = (UsuarioController)scope.ServiceProvider.GetService(typeof(UsuarioController));
        }

        [Fact]
        public void GetUsuarios_Test()
        {
            var response = (ObjectResult)usuarioController.GetUsuarios();
            AssertGetListado(response);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(50)]
        public void GetUsuarioById_Test(int userId)
        {
            var response = (ObjectResult)usuarioController.GetUsuarioById(userId);
            AssertGetData(response, message);
        }

        [Theory]
        [InlineData(100)]
        public void DeleteUsuarioInvalido_Test(int userId)
        {
            var response = (ObjectResult)usuarioController.DeleteUsuario(userId);
            Assert.True(response.StatusCode == StatusCodes.Status400BadRequest);
            Assert.Contains(message, response.Value?.ToString());
        }
    }
}
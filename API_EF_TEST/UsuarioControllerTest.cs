using API_EF.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_EF_TEST
{
    public class UsuarioControllerTest : CommonTest
    {
        private UsuarioController usuarioController;
        protected override void InitServices()
        {
            usuarioController = (UsuarioController)scope.ServiceProvider.GetService(typeof(UsuarioController));
        }

        [Fact]
        public void GetUsuarios_Test()
        {
            var response = (ObjectResult)usuarioController.GetUsuarios();
            Assert.NotNull(response);
            Assert.True(response.StatusCode == StatusCodes.Status200OK);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(50)]
        public void GetUsuarioById_Test(int userId)
        {
            var response = (ObjectResult)usuarioController.GetUsuarioById(userId);
            if(response.StatusCode == StatusCodes.Status404NotFound)
            {
                const string message = "No se encontro al usuario";
                Assert.Contains(message, response.Value?.ToString());
                return;
            }
            var user = response.Value;
            Assert.NotNull(user);
            Assert.True(response.StatusCode == StatusCodes.Status200OK);
        }

        [Fact]
        public void DeleteUsuarioInvalido_Test()
        {
            const int userId = 100;
            const string message = "No se encontro el usuario";
            var response = (ObjectResult)usuarioController.DeleteUsuario(userId);
            Assert.True(response.StatusCode == StatusCodes.Status400BadRequest);
            Assert.Contains(message, response.Value?.ToString());
        }
    }
}
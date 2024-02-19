using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Venta.API.Models;
using Venta.API.Security;

namespace Venta.API.Controllers
{
    namespace Venta.Api.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class AuthController : Controller
        {

            [HttpPost]
            [AllowAnonymous]
            public async Task<IActionResult> Login(LoginModel model)
            {
                var token = TokenServices.CreateToken(model);

                return Ok(token);
            }
        }
    }
}

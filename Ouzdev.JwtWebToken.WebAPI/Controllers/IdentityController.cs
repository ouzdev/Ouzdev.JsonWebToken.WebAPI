using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ouzdev.JwtWebToken.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        /// api/identity/login => Bu metod sadece bu url çağrıldığı zaman çalışacaktır.
        [HttpGet("[action]")]
        public IActionResult Login()
        {
            //Bu metod çağrıldığı zaman Created metodu bir token oluşturacak ve return edicektir.
            return Created("", new TokenGenerator().CreateToken());
        }
        /// api/identity/adminLogin => Bu metod sadece bu url çağrıldığı zaman çalışacaktır.
        [HttpGet("[action]")]
        public IActionResult AdminLogin()
        {
            //Bu metod çağrıldığı zaman Created metodu bir admin token oluşturacak ve return edicektir.
            return Created("", new TokenGenerator().TokenAdminRoleGenerator());
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult Access()
        {
            //Login metodu çalıştıktan sonra Access metodunu çalıştırdığımızda "Authorize" yani yetki kontrolu yapılacaktır. 
            //Eğer geçerli bir token tanımı var ise bu metod başarılı bir şekilde çalışcaktır. 
            //Eğer bu halde uygulamayı çalıştırırsak 401 hatası alırız. Nedeni startup.cs de sadece Authorization tanımlamsının yanında Authentication tanımlaması yapmamış olmamız.
            return Ok("Token Doğru Ve Başarılı bir şekilde erişim sağladınız.");
        }


        [Authorize(Roles = "Admin,Member")]
        [HttpGet("[action]")]
        public IActionResult AdminAccess()
        {
            //Login metodu çalıştıktan sonra Access metodunu çalıştırdığımızda "Authorize" yani yetki kontrolu yapılacaktır. 
            //Eğer geçerli bir token tanımı var ise bu metod başarılı bir şekilde çalışcaktır. 
            //Eğer bu halde uygulamayı çalıştırırsak 401 hatası alırız. Nedeni startup.cs de sadece Authorization tanımlamsının yanında Authentication tanımlaması yapmamış olmamız.
            return Ok("Token Doğru Ve Başarılı bir şekilde Admin Sayfasına erişim sağladınız.");
        }
    }
}

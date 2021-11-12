using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;

        public UsersController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        [Authorize]
        [Route("purchases")]
        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            var userId = _currentUserService.UserId;
            var purchases = await _userService.GetPurchaseMovies(userId);
            return Ok(purchases);
        }
    }
}
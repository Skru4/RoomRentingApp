using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoomRentingApp.Controllers
{
    /// <summary>
    /// The base controller class which makes all other controllers locked with authorization.
    /// </summary>
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}

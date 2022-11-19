using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoomRentingApp.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}

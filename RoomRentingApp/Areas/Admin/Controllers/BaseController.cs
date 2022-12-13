using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static RoomRentingApp.Core.Constants.UserConstants.Roles;

namespace RoomRentingApp.Areas.Admin.Controllers
{
    /// <summary>
    /// Base controller class which makes all other controllers locked with authorization admin.
    /// </summary>
    [Authorize(Roles = AdministratorRole)]
    [Area("Admin")]
	public class BaseController : Controller
	{

    }
}

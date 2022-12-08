using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static RoomRentingApp.Core.Constants.UserConstants.Roles;

namespace RoomRentingApp.Areas.Admin.Controllers
{
	[Authorize(Roles = AdministratorRole)]
    [Area("Admin")]
	public class BaseController : Controller
	{

    }
}

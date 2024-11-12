using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AutoSpareParts.MVC.Areas.Admin.Controllers;

[AllowAnonymous]
[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
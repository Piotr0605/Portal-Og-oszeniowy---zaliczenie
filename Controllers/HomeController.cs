using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PortalOgloszeniowy.Models;

namespace PortalOgloszeniowy.Controllers;

public class HomeController : Controller
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

using ITPDevelopment.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace ITPDevelopment.WebApi.Controllers;

public class RootController : Controller
{
    [HttpGet("/")]
    public IActionResult RedirectToTheAngularSpa()
    {
        return Redirect($@"~{SpaRouting.Tasks}");
    }
}
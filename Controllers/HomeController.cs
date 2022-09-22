using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Models;

namespace Phonebook.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("username") == null)
        {
            return RedirectToAction("Index" , "Accounts" , new { area = "" });
        }
        else 
        {
            return View();
        }
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        if (HttpContext.Session.GetString("username") == null)
        {
            return RedirectToAction("Index" , "Accounts" , new { area = "" });
        }
        else 
        {
            return View();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

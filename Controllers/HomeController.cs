using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjetoCinemanticaMVC.Models;

namespace ProjetoCinemanticaMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if(HttpContext.Session.GetString("UsuarioNome") == null)
        {
            return RedirectToAction("Index", "Login");
        }

        // ViewBag -> Armazena as informações temporariamente na view
        ViewBag.Usuario = HttpContext.Session.GetString("UsuarioNome");
        return View();
    }
}
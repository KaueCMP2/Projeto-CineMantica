using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjetoCinemanticaMVC.Models;

namespace ProjetoCinemanticaMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("UsuarioNome") == null)
        {
            return RedirectToAction("Index", "Login");
        }

        // ViewBag -> Armazena as informações temporariamente na view
        ViewBag.Usuario = HttpContext.Session.GetString("UsuarioNome");
        return View();
    }

    [HttpPost]
    public IActionResult ReceiveMovieData([FromBody] MovieIdDto data)
    {
        if (data == null || data.MovieId <= 0)
            return BadRequest("ID inválido.");

        // Aqui você pode salvar no banco, session, cache, logs etc.
        HttpContext.Session.SetInt32("MovieId", data.MovieId);
        Console.WriteLine("Filme recebido: " + data.MovieId);

        return Json(new { ok = true, filmeIdRecebido = data.MovieId });
    }

}
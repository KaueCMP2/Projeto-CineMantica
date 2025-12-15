using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjetoCinemanticaMVC.Data;
using ProjetoCinemanticaMVC.Models;

namespace ProjetoCinemanticaMVC.Controllers;

public class HomeController : Controller
{

    private readonly AppDbContext _appDbContext;
    public HomeController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("UsuarioNome") == null)
        {
            return RedirectToAction("Index", "Login");
        }

        // ViewBag -> Armazena as informações temporariamente na view
        ViewBag.Usuario = HttpContext.Session.GetString("UsuarioNome");

        int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

        var usuario = _appDbContext.Usuarios.FirstOrDefault(usuario => usuarioId == usuario.id_usuario);

        var viewModel = new HomeViewModel
        {
            FotoUsuario = usuario?.foto_perfil != null
                        ? $"data:image/*;base64,{Convert.ToBase64String(usuario.foto_perfil)}"
                        : "~/assets/home-images/user.png"
        };
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult ReceiveMovieData([FromBody] MovieIdDto data)
    {
        if (data == null || data.MovieId <= 0)
            return BadRequest("ID inválido.");

        // Aqui você pode salvar no banco, session, cache, logs etc.
        HttpContext.Session.SetInt32("MovieId", data.MovieId);
        HttpContext.Session.SetString("MovieTitle", data.MovieTitle);
        HttpContext.Session.SetString("MovieImg", data.MovieImg);

        Console.WriteLine("Filme recebido: " + data.MovieId);
        Console.WriteLine("Filme recebido: " + data.MovieTitle);
        Console.WriteLine("Filme recebido: " + data.MovieImg);

        return Json(new { ok = true, filmeIdRecebido = data.MovieId });
    }
}
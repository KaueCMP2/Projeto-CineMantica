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
        if(HttpContext.Session.GetString("UsuarioNome") == null)
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
                        : "/assets/img/img-perfil.png"
        };
        return View(viewModel);
    }
}
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjetoCinemanticaMVC.Data;
using ProjetoCinemanticaMVC.Models;

namespace ProjetoCinemanticaMVC.Controllers // Troque pelo namespace do seu projeto
{
    public class DestaquesController : Controller
    {
         private readonly AppDbContext _appDbContext;

         public DestaquesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
           int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

           var usuario = _appDbContext.Usuarios.FirstOrDefault(usuario => usuarioId == usuario.id_usuario);

           var viewModel = new DestaqueViewModel
        {
            FotoUsuario = usuario?.foto_perfil != null 
                        ? $"data:image/*;base64,{Convert.ToBase64String(usuario.foto_perfil)}"
                        : "~/assets/home-images/user.png"
        };
        return View(viewModel);
        }
    }
}
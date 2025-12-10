using Microsoft.AspNetCore.Mvc;
using ProjetoCinemanticaMVC.Data;
using ProjetoCinemanticaMVC.Models;

namespace ProjetoCinemanticaMVC.Controllers
{
    public class PerfilController : Controller
    {
        // GET: /Perfil/Index?user=...

        private readonly AppDbContext _appDbContext;
        public PerfilController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                RedirectToAction("Index", "Login");
            }

            var usuario = _appDbContext.Usuarios.FirstOrDefault(usuario => usuarioId == usuario.id_usuario);

            if (usuario == null)
            {
                // Optionally, redirect or show an error view
                return RedirectToAction("Index", "Login");
            }

            int seguidoresCount = _appDbContext.Seguindos.Count(u => u.seguidor_id == usuario.id_usuario);
            int seguindoCount = _appDbContext.Seguindos.Count(u => u.seguindo_id == usuario.id_usuario);

            var viewModel = new PerfilViewModel
            {
                id_usuario = usuario.id_usuario,
                nome = usuario.nome,
                Email = usuario.email,
                desc_perfil = usuario.desc_perfil,
                FotoBase64 = usuario.foto_perfil != null ? Convert.ToBase64String(usuario.foto_perfil) : null,
                data_nascimento = usuario.data_nascimento,
                BannerBase64 = usuario.Banner != null ? Convert.ToBase64String(usuario.Banner) : null,
                seguidores_count = seguidoresCount,
                seguindo_count = seguindoCount
            };

            return View(viewModel);
        }

        
        [HttpPost]
        public IActionResult AtualizarFoto(IFormFile foto, IFormFile banner, int id_usuario, string nome, string desc_perfil)
        {

            // if (usuarioId == null)
            // {
            //     return RedirectToAction("Index", "Login");
            // }

            var usuario = _appDbContext.Usuarios.FirstOrDefault(usuario => usuario.id_usuario == id_usuario);

            usuario.nome = nome;
            usuario.desc_perfil = desc_perfil;

            if (usuario == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (foto != null && foto.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    foto.CopyTo(ms);
                    usuario.foto_perfil = ms.ToArray();
                }
            }

            if (banner != null && banner.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    banner.CopyTo(ms);
                    usuario.Banner = ms.ToArray();
                }
            }

            _appDbContext.SaveChanges();
                
            return RedirectToAction("Index");
    }
}

}
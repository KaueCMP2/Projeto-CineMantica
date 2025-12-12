using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjetoCinemanticaMVC.Data;
using ProjetoCinemanticaMVC.Models;

namespace ProjetoCinemanticaMVC.Controllers
{
    public class SeguindoController : Controller
    {

        private readonly AppDbContext _context;

        public SeguindoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

           

            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.id_usuario == usuarioId);

            var seguindo = _context.Seguindos
                .Where(s => s.seguindo_id == usuarioId)
                .ToList();

            // int? movieId = Comentario.id_filme;


            var seguindoIds = seguindo.Select(s => s.seguidor_id).ToList();

            var feed = _context.Comentarios
                .Include(c => c.id_usuarioNavigation)
                .Where(c => c.id_usuario.HasValue && seguindoIds.Contains(c.id_usuario.Value))
                .OrderByDescending(c => c.data_post)
                .ToList();

            
            var viewModel = new SeguindoViewModel
            {
                
                NomeUsuario = usuario?.nome,
                FotoUsuario = usuario?.foto_perfil != null ? Convert.ToBase64String(usuario.foto_perfil) : null,
                Seguindo = seguindo,
                Feed = feed

            };

            return View(viewModel);
        }

         // seguir usuário
        [HttpPost]
        public IActionResult Seguir(int idSeguido)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
                return Unauthorized();

            // evita seguir duas vezes
            bool existe = _context.Seguindos
                .Any(s => s.seguindo_id == usuarioId && s.seguidor_id == idSeguido);

            if (!existe)
            {
                var seguir = new Seguindo
                {
                    seguindo_id = usuarioId.Value,
                    seguidor_id = idSeguido
                };

                _context.Seguindos.Add(seguir);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // deixar de seguir
        [HttpPost]
        public IActionResult DeixarSeguir(int idSeguido)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
                return Unauthorized();

            var rel = _context.Seguindos
                .FirstOrDefault(s => s.seguindo_id == usuarioId && s.seguidor_id == idSeguido);

            if (rel != null)
            {
                _context.Seguindos.Remove(rel);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        

    }
}

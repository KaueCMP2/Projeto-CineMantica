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

        public IActionResult Index(int? id)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            int idPerfil;

            if (!id.HasValue || id.Value == usuarioId.Value)
            {
                idPerfil = usuarioId.Value;
            }
            else
            {
                idPerfil = id.Value;
            }

            bool jaSegue = false;

            if (usuarioId.HasValue && usuarioId.Value != idPerfil)
            {
                jaSegue = _appDbContext.Seguindos.Any(s =>
                    s.seguidor_id == usuarioId.Value &&
                    s.seguindo_id == idPerfil
                );
            }

            var usuario = _appDbContext.Usuarios.FirstOrDefault(usuario => usuario.id_usuario == idPerfil);

            if (usuario == null)
            {
                return RedirectToAction("Index", "Login");
            }

            int seguidoresCount = _appDbContext.Seguindos.Count(u => u.seguidor_id == usuario.id_usuario);
            int seguindoCount = _appDbContext.Seguindos.Count(u => u.seguindo_id == usuario.id_usuario);

            var perfil = new PerfilViewModel
            {
                id_usuario = usuario.id_usuario,
                nome = usuario.nome,
                NomeUsuario = usuario.nick_name,
                Email = usuario.email,
                desc_perfil = usuario.desc_perfil,
                FotoBase64 = usuario.foto_perfil != null ? Convert.ToBase64String(usuario.foto_perfil) : null,
                data_nascimento = usuario.data_nascimento,
                BannerBase64 = usuario.Banner != null ? Convert.ToBase64String(usuario.Banner) : null,
                seguidores_count = seguidoresCount,
                seguindo_count = seguindoCount,
                FotoPerfilTopo = usuario?.foto_perfil != null
                        ? $"data:image/*;base64,{Convert.ToBase64String(usuario.foto_perfil)}"
                        : "~/assets/home-images/user.png",
            };

            var avaliacoes = _appDbContext.Comentarios
                .Where(c => c.id_usuario == usuario.id_usuario)
                .OrderByDescending(c => c.data_post)
                .Select(c => new AvaliacoesViewModel
                {
                    nome_usuario = usuario.nick_name,
                    TituloFilme = c.nome_filme,
                    PosterFilme = c.img_path,
                    descricao = c.descricao,
                    data_post = c.data_post,
                    usuario = usuario,
                    tipo_comentario = c.tipo_comentario,
                })
                .ToList();

            var viewModel = new PerfilViewModel
            {
                Avaliacoes = avaliacoes,
                Perfil = perfil,
                isMeuPerfil = usuarioId.HasValue && usuarioId.Value == idPerfil,
                JaSegue = jaSegue,
                usuarioId = usuarioId ?? 0
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

            usuario.nick_name = nome;
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

        [HttpPost]
        public IActionResult ToggleFollow(int seguindoId)
        {
            int? seguidorId = HttpContext.Session.GetInt32("UsuarioId");

            if (!seguidorId.HasValue || seguidorId.Value == seguindoId)
                return RedirectToAction("Index", new { id = seguindoId });

            var relacionamento = _appDbContext.Seguindos.FirstOrDefault(s =>
                s.seguidor_id == seguidorId.Value &&
                s.seguindo_id == seguindoId
            );

            if (relacionamento == null)
            {
                _appDbContext.Seguindos.Add(new Seguindo
                {
                    seguidor_id = seguidorId.Value,
                    seguindo_id = seguindoId
                });
            }
            else
            {
                _appDbContext.Seguindos.Remove(relacionamento);
            }

            _appDbContext.SaveChanges();

            return RedirectToAction("Index", new { id = seguindoId });
        }

    }

}
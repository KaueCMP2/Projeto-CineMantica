using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoCinemanticaMVC.Data;
using ProjetoCinemanticaMVC.Models;

namespace ProjetoCinemanticaMVC.Controllers // Verifique se o namespace é o mesmo do seu projeto
{
    public class AvaliacoesController : Controller
    {
        // GET: Avaliacoes/Feed
        // Carrega o arquivo: Views/Avaliacoes/feed_avaliacoes.cshtml
        // public ActionResult Feed()
        // {
        //     return View("feed_avaliacoes");
        // }

        // GET: Avaliacoes/Nova
        // Carrega o arquivo: Views/Avaliacoes/nova_avaliacao.cshtml
        public ActionResult Nova(string title, string poster)
        {
            // Passamos os parâmetros via ViewBag caso queira usar C# no futuro, 
            // mas seu JS atual já pega via URL (QueryString).
            ViewBag.MovieTitle = title;
            ViewBag.MoviePoster = poster;

            return View("nova_avaliacao");
        }

        // GET: Avaliacoes/Ver
        // Carrega o arquivo: Views/Avaliacoes/ver_avaliacoes.cshtml
        public ActionResult Ver(string title, string poster)
        {
            return View("ver_avaliacoes");
        }


        public readonly AppDbContext _appDbContext;
        public AvaliacoesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult AvaliacoesGlobais(string title, string poster)
        {
            ViewBag.Title = title;
            ViewBag.Poster = poster;

            int? movieId = HttpContext.Session.GetInt32("MovieId");

            Console.WriteLine("MovieId = " + movieId);

            var comentarios = _appDbContext.Comentarios
      .Include(c => c.id_usuarioNavigation)
      .OrderByDescending(c => c.data_post)
      .Select(c => new AvaliacoesViewModel
      {
          nome_usuario = c.id_usuarioNavigation.nick_name,
          TituloFilme = c.nome_filme,
        PosterFilme = c.img_path,   
          descricao = c.descricao,
          data_post = c.data_post,
          usuario = c.id_usuarioNavigation,
          foto_perfil = c.id_usuarioNavigation.foto_perfil != null
              ? Convert.ToBase64String(c.id_usuarioNavigation.foto_perfil)
              : null
      })
      .ToList();

            return View("~/Views/Avaliacoes/feed_avaliacoes.cshtml", comentarios);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using ProjetoCinemanticaMVC.Data;
using ProjetoCinemanticaMVC.Models;

namespace ProjetoCinemanticaMVC.Controllers
{
    public class ComentarioController : Controller
    {
        public readonly AppDbContext _appDbContext;
        public ComentarioController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index(string title, string poster)
        {
            ViewBag.Title = title;
            ViewBag.Poster = poster;

            int? movieId = HttpContext.Session.GetInt32("MovieId");

            Console.WriteLine("MovieId = " + movieId);

            var comentarios = _appDbContext.Comentarios
                .Include(c => c.id_usuarioNavigation) // IMPORTANTE!
                .Where(c => c.id_filme == movieId)
                .OrderByDescending(c => c.data_post)
                .Select(c => new ComentarioViewModel
                {
                    nome_usuario = c.id_usuarioNavigation.nick_name,
                    descricao = c.descricao,
                    data_post = c.data_post,
                    usuario = c.id_usuarioNavigation,
                    foto_perfil = Convert.ToBase64String(c.id_usuarioNavigation.foto_perfil)
                    
                })
                .ToList();

            Console.WriteLine("Qtd comentários: " + comentarios.Count);

            return View("~/Views/Avaliacoes/ver_avaliacoes.cshtml", comentarios);
        }


        [HttpPost]
        public IActionResult CriarComentario(string tipo_comentario, int id_usuario, int id_filme, string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            // string.IsNullOrWhiteSpace(id_filme.ToString()) || 
            // string.IsNullOrWhiteSpace(id_usuario.ToString()) || 
            // string.IsNullOrWhiteSpace(tipo_comentario))
            {
                return View("Index", "Home");
            }

            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            int? movieId = HttpContext.Session.GetInt32("MovieId");
            string? movieTitle = HttpContext.Session.GetString("MovieTitle");
            string? moviePoster = HttpContext.Session.GetString("MovieImg");


            var comentario = new Comentario
            {
                tipo_comentario = tipo_comentario,
                id_usuario = usuarioId,
                id_filme = movieId,
                descricao = descricao,
                data_post = DateTime.Now,
                nome_filme = movieTitle,
                img_path = moviePoster
            };

            _appDbContext.Comentarios.Add(comentario);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
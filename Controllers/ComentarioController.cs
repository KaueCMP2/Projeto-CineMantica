using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
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

        public IActionResult Index(int idFilme)
        {
            var comentarios = _appDbContext.Comentarios
                .Where(c => c.id_filme == idFilme)
                .OrderByDescending(c => c.data_post)
                .Select(c => new ComentarioViewModel
                {
                    nome_usuario = c.usuario.nick_name,
                    descricao = c.descricao,
                    data_post = c.data_post
                })
                .ToList();

            return View(comentarios);
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


            var comentario = new Comentario
            {
                tipo_comentario = tipo_comentario,
                id_usuario = usuarioId,
                id_filme = movieId,
                descricao = descricao,
                data_post = DateTime.Now
            };

            _appDbContext.Comentarios.Add(comentario);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
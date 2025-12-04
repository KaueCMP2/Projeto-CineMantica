using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ProjetoCinemanticaMVC.Data;
using ProjetoCinemanticaMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ProjetoCinemanticaMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                ViewBag.Erro = "Email ou senha incorretos";
                return View("Index");
            }

            // hash das senha digitada
            byte[] senhaDIgitadaHash = HashService.GerarHashBytes(senha);

            // Percorre a lista de usuarios do banco e verifica se existe algum com aquele email
            var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.email == email);

            if(usuario == null)
            {
                ViewBag.Erro = "E-mail ou senha incorretos.";
                return View("Index");
            }

            // comparar byte a byte da senha
            // SequenceEqual ->retorna false se qualquer byte estiver diferente
            if (!usuario.senha.SequenceEqual(senhaDIgitadaHash))
            {
                ViewBag.Erro = "E-mail ou senha incorretos.";
                return View("Index");
            }

            HttpContext.Session.SetString("UsuarioNome", usuario.email);
            HttpContext.Session.SetInt32("UsuarioId", usuario.id_usuario);

            return RedirectToAction("Index", "Home");
        }   

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
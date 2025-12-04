using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;
using ProjetoCinemanticaMVC.Data;
using ProjetoCinemanticaMVC.Models;
using ProjetoCinemanticaMVC.Services;

namespace ProjetoCinemanticaMVC.Controllers
{
    public class CadastroController : Controller
    {
        private readonly AppDbContext _context;

        public CadastroController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Criar(string email, string senha, string confirmarSenha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha) || string.IsNullOrWhiteSpace(confirmarSenha))
            {
                ViewBag.Erro = "Preencha todos os campos!";
                return View("Index");
            }

            if (senha != confirmarSenha)
            {
                ViewBag.Erro = "As senhas não conferem";
                return View("Index");
            }

            //* Verificando se já existe email cadastrado no sistema
            if (_context.Usuarios.Any(usuario => usuario.email == email))
            {
                ViewBag.Erro = "E-mail já cadastrado";
                return View("Index");
            }

            byte[] hash = HashService.GerarHashBytes(senha);

            Usuario usuario = new Usuario
            {
                nome = "Usuário",
                email = email,
                senha = hash,
                nick_name = "Usuário",
                desc_perfil = null,
                foto_perfil = null,
                RegraId = 1
            };

            //* Salvar no banco
            _context.Usuarios.Add(usuario); 
            _context.SaveChanges();

            return RedirectToAction("Index", "Login");
        }
    }
}

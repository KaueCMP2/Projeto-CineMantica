using System.Security.Policy;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<IActionResult> Criar(string nome, string nickname, string dataNascimento, string email, string senha, string confirmarSenha)
        {
            if( string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(nickname) ||
                string.IsNullOrWhiteSpace(dataNascimento) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(senha) ||
                string.IsNullOrWhiteSpace(confirmarSenha))
            {
                ViewBag.Erro = "Preencha todos os campos";
                return View("Index");
            }

            if(senha != confirmarSenha)
            {
                ViewBag.Erro = "As senhas não conferem.";
                return View("Index");
            }
            if(_context.Usuarios.Any(usuario => usuario.email == email))
            {
                
                ViewBag.Erro = "E-mail já cadastrado";
                return View("Index");
            }

            if(_context.Usuarios.Any(usuario => usuario.nick_name == nickname))
            {
                ViewBag.Erro = "Nome de usuário já cadastrado";
                return View("Index");
            }

            byte[] hash = HashService.GerarHashBytes(senha);

            if (!DateOnly.TryParse(dataNascimento, out var dataNasc))
            {
                ViewBag.Erro = "Data de nascimento inválida";
                return View("Index");
            }

            Usuario usuario = new Usuario
            {
                nome = nome,
                email = email,
                senha = hash,
                nick_name = nickname,
                data_nascimento = dataNasc
                
            };
            
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            // redireciona para o login
            return RedirectToAction("Index", "Home");


        }

    }
}


  
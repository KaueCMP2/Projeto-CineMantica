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
        public async Task<IActionResult> Criar(string nome, string nick_name, string data_nascimento, string email, string senha, string confirmar)
        {
            if( string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(nick_name) ||
                string.IsNullOrWhiteSpace(data_nascimento) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(senha) ||
                string.IsNullOrWhiteSpace(confirmar))
            {
                ViewBag.Erro = "Preencha todos os campos";
                return View("Index");
            }

            if(senha != confirmar)
            {
                ViewBag.Erro = "As senhas não conferem.";
                return View("Index");
            }
            if(_context.Usuarios.Any(usuario => usuario.email == email))
            {
                
                ViewBag.Erro = "E-mail já cadastrado";
                return View("Index");
            }

            if(_context.Usuarios.Any(usuario => usuario.nick_name == nick_name))
            {
                ViewBag.Erro = "Nome de usuário já cadastrado";
                return View("Index");
            }

            byte[] hash = HashService.GerarHashBytes(senha);

            if (!DateOnly.TryParse(data_nascimento, out var dataNasc))
            {
                ViewBag.Erro = "Data de nascimento inválida";
                return View("Index");
            }

            Usuario usuario = new Usuario
            {
                nome = nome,
                email = email,
                senha = hash,
                nick_name = nick_name,
                data_nascimento = dataNasc
                
            };

            Console.WriteLine(usuario.nome, usuario.email, usuario.nick_name, usuario.data_nascimento);
            
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            Console.WriteLine("Usuário cadastrado com sucesso!");
            // redireciona para o login
            return RedirectToAction("Index", "Home");


        }

    }
}


  
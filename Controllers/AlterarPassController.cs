using ProjetoCinemanticaMVC.Data;
using Microsoft.AspNetCore.Mvc;
using ProjetoCinemanticaMVC.Services;

namespace EsqueceuSenha.controllers
{
    public class AlterarPassController : Controller
    {
        private readonly AppDbContext ctx;
        public AlterarPassController(AppDbContext context)
        {
            ctx = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult alterarSenha(string senha, string Confsenha)
        {
            var usas = ctx.Usuarios.FirstOrDefault(u => u.id_usuario == HttpContext.Session.GetInt32("idUser"));
            if(usas == null)
            {
                return RedirectToAction("Index", "envCodigo");
            }
                        
            if(string.IsNullOrWhiteSpace(senha) || string.IsNullOrWhiteSpace(Confsenha))
            {
                ViewBag.Erro1 = "Preencha todos os campos";
                return View("Index");
            }
            if(senha != Confsenha)
            {
                ViewBag.Erro1 = "Senhas não coincidem";
                return View("Index");
            }

            usas.senha = HashService.GerarHashBytes(senha);
            ctx.SaveChanges();    
            return RedirectToAction("Index", "Login");        
        }
    }
}
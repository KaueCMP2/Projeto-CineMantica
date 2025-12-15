using ProjetoCinemanticaMVC.Data;
using ProjetoCinemanticaMVC.Models;
using ProjetoCinemanticaMVC.Services;
using Microsoft.AspNetCore.Mvc;
using EsqueceuSenha.Services;

namespace EsqueceuSenha.controllers
{
    public class EnvCodigoController : Controller
    {
        private readonly AppDbContext _context;

        public EnvCodigoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EnviarCodigoUsu(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.erro = "Prencha todos os campos";
                return View("Index");
            }

            ViewBag.Modal = true;
            EmailService emailService = new EmailService(email);
            await emailService.EnviarCodigo();

            var codigo = emailService.CodigoGerado;

            var usuario = _context.Usuarios.FirstOrDefault(u => u.email == email);
            if (usuario == null)
            {
                ViewBag.erro = "Email não encontrado";
                return View("Index");
            }

            codigoUsuarioSenha codigoSenha = new codigoUsuarioSenha();
            codigoSenha.id_usuario = usuario.id_usuario;
            codigoSenha.codigo = codigo;

            HttpContext.Session.SetInt32("idUser", usuario.id_usuario);

            ViewBag.Modal = true;
            _context.codigoUsuarioSenhas.Add(codigoSenha);
            _context.SaveChanges();
            return View("Index");
        }

        [HttpPost]
        public IActionResult validarCodigo(string email, int codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo.ToString()))
            {
                ViewBag.Modal = true;
                ViewBag.erro1 = "Preencha o de codigo campos";
                return View("Index");
            }

            var usasCod = _context.codigoUsuarioSenhas.Where(c => c.id_usuario == HttpContext.Session.GetInt32("idUser")).OrderByDescending(d => d.dataEnvio).FirstOrDefault();
            if (usasCod == null)
            {
                ViewBag.Modal = true;
                ViewBag.erro1 = "Dados invalidos";
                return View("Index");
            }

            if (usasCod.codigo != codigo)
            {
                ViewBag.Modal = true;
                ViewBag.erro1 = "Dados invalidos";
                return View("Index");
            }
            return RedirectToAction("Index", "AlterarPass");
        }
    }
}

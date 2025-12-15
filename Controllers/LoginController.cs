using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ProjetoCinemanticaMVC.Data;
using ProjetoCinemanticaMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

using ProjetoCinemanticaMVC.Models;


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
            if (HttpContext.Session.GetInt32("UsuarioId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
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



             // LOGIN VIA GOOGLE
        public IActionResult LoginGoogle()
        {
            var propriedades = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleCallback")
            };

            // Abre a tela de login do Google
            return Challenge(propriedades, GoogleDefaults.AuthenticationScheme);
        }

        // RETORNO DO GOOGLE
        public async Task<IActionResult> GoogleCallback()
        {
            // Essa autenticação precisa usar o esquema do Google
            var resultado = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!resultado.Succeeded)
                return RedirectToAction("Index");

            var claims = resultado.Principal.Identities.First().Claims.ToList();

            string email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
             string fotoClaim = claims.FirstOrDefault(c =>
                c.Type == "picture" ||
                c.Type == "urn:google:picture" ||
                c.Type == ClaimTypes.Uri)?.Value;

            // converte a claim para byte[] (suporta base64 puro ou data:[...];base64,...)
            byte[] foto_perfil_bytes = null;
            if (!string.IsNullOrEmpty(fotoClaim))
            {
                try
                {
                    // remove possível prefixo data:<tipo>;base64,
                    var commaIndex = fotoClaim.IndexOf("base64,", StringComparison.OrdinalIgnoreCase);
                    if (commaIndex >= 0)
                        fotoClaim = fotoClaim.Substring(commaIndex + 7);

                    foto_perfil_bytes = Convert.FromBase64String(fotoClaim);
                }
                catch
                {
                    // se não for base64, mantém null (pode optar por logar ou tentar baixar se for URL)
                    foto_perfil_bytes = null;
                }
            }
            string nome = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "Usuário Google";

            if (email == null)
                return RedirectToAction("Index");

            // Se o usuário ainda não existe no banco ele cria soq com a senha vazia por conta que essa API usa o cookies
            // do negocio pra logar
            var usuario = _context.Usuarios.FirstOrDefault(u => u.email == email);

            if (usuario == null)
            {
                usuario = new Usuario
                {
                    email = email,
                    nome = nome,
                    foto_perfil = foto_perfil_bytes
                     // sem nada pq o google vai usar os cookies(nao e os de comer)
                };

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
            }

            // Essa identity pega todas as claims vindas da conta Google
            var identity = new ClaimsIdentity(resultado.Principal.Identity, resultado.Principal.Claims);

            // Cria cookie de login local, que mantém o usuário autenticado no site
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );

            // Armazena sessão usada no site
            HttpContext.Session.SetString("UsuarioNome", usuario.nome);
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
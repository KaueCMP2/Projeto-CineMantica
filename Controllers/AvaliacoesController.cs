using Microsoft.AspNetCore.Mvc;

namespace ProjetoCinemanticaMVC.Controllers // Verifique se o namespace é o mesmo do seu projeto
{
    public class AvaliacoesController : Controller
    {
        // GET: Avaliacoes/Feed
        // Carrega o arquivo: Views/Avaliacoes/feed_avaliacoes.cshtml
        public ActionResult Feed()
        {
            return View("feed_avaliacoes");
        }

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
    }
}
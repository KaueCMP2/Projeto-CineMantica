using Microsoft.AspNetCore.Mvc;

namespace ProjetoCinemanticaMVC.Controllers // Troque pelo namespace do seu projeto
{
    public class DestaquesController : Controller
    {
        public IActionResult Index()
        {
            // Isso vai procurar o arquivo em Views/Destaques/Index.cshtml
            return View(); 
        }
    }
}
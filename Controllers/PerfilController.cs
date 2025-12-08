using Microsoft.AspNetCore.Mvc;

namespace ProjetoCinemanticaMVC.Controllers
{
    public class PerfilController : Controller
    {
        // GET: /Perfil/Index?user=...
        public IActionResult Index()
        {
            return View();
        }
    }
}

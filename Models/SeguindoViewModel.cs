namespace ProjetoCinemanticaMVC.Models
{
    public class SeguindoViewModel
    {
        public List<Seguindo> Seguindo { get; set; } = new List<Seguindo>();
        public List<Comentario> Feed { get; set; }
    }
}
namespace ProjetoCinemanticaMVC.Models
{
    public class SeguindoViewModel
    {
        public string ?NomeUsuario { get; set; }
        public byte[] ?FotoUsuario { get; set; }
        public List<Seguindo> ?Seguindo { get; set; } 
        public List<Comentario> ?Feed { get; set; }
    }
}
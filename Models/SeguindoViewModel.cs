namespace ProjetoCinemanticaMVC.Models
{
    public class SeguindoViewModel
    {
        public int FilmeId { get; set; }
        public string ?NomeUsuario { get; set; }
        public string ?FotoUsuario { get; set; }
        public List<Seguindo> ?Seguindo { get; set; } 
        public List<Comentario> ?Feed { get; set; }
        public string ?Titulo { get; set; }
        public string ?Poster { get; set; }
        public string FotoPerfilTopo { get; set; }


    }
}
namespace ProjetoCinemanticaMVC.Models
{
    public class AvaliacoesViewModel
    {
        public string nome_usuario { get; set; }
        public int? id_usuario { get; set; }
        public string descricao { get; set; }
        public DateTime data_post { get; set; }
        public Usuario usuario { get; set; }
        public string foto_perfil { get; set; }

        public int FilmeId { get; set; }
        public string TituloFilme { get; set; }
        public string PosterFilme { get; set; }
        public string tipo_comentario { get; set; }
    }
}
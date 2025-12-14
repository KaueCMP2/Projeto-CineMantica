namespace ProjetoCinemanticaMVC.Models
{
    public class AvaliacoesViewModel 
    {
        public string nome_usuario { get; set; }
        public string descricao { get; set; }
        public DateTime data_post { get; set; }
        public Usuario usuario { get; set; }
        public string foto_perfil { get; set; }
    }
}
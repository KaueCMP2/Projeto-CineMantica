using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoCinemanticaMVC.Models
{
    public class ComentarioViewModel
    {
        public string? tipo_comentario { get; set; }
        public int? id_usuario { get; set; }
        public int? id_filme { get; set; }
        public string descricao { get; set; } = null!;
        public DateTime data_post { get; set; }
        public string? nome_usuario { get; set; }
        public string? titulo_filme { get; set; }
        // public List<Comentario> listaComentario {get; set;}
    }
}
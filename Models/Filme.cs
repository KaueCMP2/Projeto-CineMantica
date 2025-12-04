using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetoCinemanticaMVC.Models;

[Table("Filme")]
public partial class Filme
{
    [Key]
    public int id_filme { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string nome { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string descricao_filme { get; set; } = null!;

    public DateOnly data_postagem { get; set; }

    public int? id_diretor { get; set; }

    public int? id_genero { get; set; }

    [InverseProperty("id_filmeNavigation")]
    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    [ForeignKey("id_diretor")]
    [InverseProperty("Filmes")]
    public virtual diretorFilme? id_diretorNavigation { get; set; }

    [ForeignKey("id_genero")]
    [InverseProperty("Filmes")]
    public virtual generoFilme? id_generoNavigation { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetoCinemanticaMVC.Models;

[Table("Comentario")]
public partial class Comentario
{
    [Key]
    public int id_comentario { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? tipo_comentario { get; set; }

    public int? id_usuario { get; set; }

    public int? id_filme { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string descricao { get; set; } = null!;

    [Precision(0)]
    public DateTime data_post { get; set; }

    [ForeignKey("id_filme")]
    [InverseProperty("Comentarios")]
    public virtual Filme? id_filmeNavigation { get; set; }

    [ForeignKey("id_usuario")]
    [InverseProperty("Comentarios")]
    public virtual Usuario? id_usuarioNavigation { get; set; }
}

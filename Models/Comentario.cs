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

    [StringLength(200)]
    [Unicode(false)]
    public string descricao { get; set; } = null!;

    [Precision(0)]
    public DateTime data_post { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string img_path { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string nome_filme { get; set; } = null!;

    [ForeignKey("id_usuario")]
    [InverseProperty("Comentarios")]
    public virtual Usuario? id_usuarioNavigation { get; set; }
}

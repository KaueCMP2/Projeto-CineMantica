using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetoCinemanticaMVC.Models;

[Table("Usuario")]
[Index("email", Name = "UQ__Usuario__AB6E61646419725F", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public int id_usuario { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string nome { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string email { get; set; } = null!;

    [MaxLength(32)]
    public byte[] senha { get; set; } = null!;

    [StringLength(12)]
    [Unicode(false)]
    public string nick_name { get; set; } = null!;

    public DateOnly data_nascimento { get; set; }

    [StringLength(100)]
    public string? desc_perfil { get; set; }

    public byte[]? foto_perfil { get; set; }

    public int RegraId { get; set; }

    [InverseProperty("id_usuarioNavigation")]
    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    [ForeignKey("RegraId")]
    [InverseProperty("Usuarios")]
    public virtual RegraPerfil Regra { get; set; } = null!;
}

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

    [StringLength(255)]
    [Unicode(false)]
    public string nome { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string email { get; set; } = null!;

    public byte[]? senha { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? nick_name { get; set; }

    public DateOnly data_nascimento { get; set; }

    [StringLength(100)]
    public string? desc_perfil { get; set; }

    public byte[]? foto_perfil { get; set; }

    public byte[]? Banner { get; set; }

    [InverseProperty("id_usuarioNavigation")]
    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    [InverseProperty("seguidor")]
    public virtual ICollection<Seguindo> Seguindoseguidors { get; set; } = new List<Seguindo>();

    [InverseProperty("seguindo")]
    public virtual ICollection<Seguindo> Seguindoseguindos { get; set; } = new List<Seguindo>();

    [InverseProperty("id_usuarioNavigation")]
    public virtual ICollection<codigoUsuarioSenha> codigoUsuarioSenhas { get; set; } = new List<codigoUsuarioSenha>();
}

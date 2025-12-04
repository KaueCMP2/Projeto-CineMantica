using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetoCinemanticaMVC.Models;

[Table("RegraPerfil")]
[Index("Nome", Name = "UQ__RegraPer__7D8FE3B2E937F565", IsUnique = true)]
public partial class RegraPerfil
{
    [Key]
    public int IdRegra { get; set; }

    [StringLength(40)]
    public string Nome { get; set; } = null!;

    [InverseProperty("Regra")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

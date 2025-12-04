using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetoCinemanticaMVC.Models;

[Table("diretorFilme")]
public partial class diretorFilme
{
    [Key]
    public int id_diretor { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string nome { get; set; } = null!;

    [InverseProperty("id_diretorNavigation")]
    public virtual ICollection<Filme> Filmes { get; set; } = new List<Filme>();
}

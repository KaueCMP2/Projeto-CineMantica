using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetoCinemanticaMVC.Models;

[Table("generoFilme")]
public partial class generoFilme
{
    [Key]
    public int id_genero { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string nome_genero { get; set; } = null!;

    [InverseProperty("id_generoNavigation")]
    public virtual ICollection<Filme> Filmes { get; set; } = new List<Filme>();
}

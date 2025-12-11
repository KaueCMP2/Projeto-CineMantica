using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetoCinemanticaMVC.Models;

[Table("Seguindo")]
[Index("seguidor_id", "seguindo_id", Name = "UQ_Seguindo", IsUnique = true)]
public partial class Seguindo
{
    [Key]
    public int id_seguindo { get; set; }

    public int seguidor_id { get; set; }

    public int seguindo_id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? data_seguindo { get; set; }

    [ForeignKey("seguidor_id")]
    [InverseProperty("Seguindoseguidors")]
    public virtual Usuario seguidor { get; set; } = null!;

    [ForeignKey("seguindo_id")]
    [InverseProperty("Seguindoseguindos")]
    public virtual Usuario seguindo { get; set; } = null!;
}

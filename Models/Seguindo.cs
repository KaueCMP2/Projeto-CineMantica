using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetoCinemanticaMVC.Models;

[Table("Seguindo")]
[Index("seguidor_id", "seguido_id", Name = "UQ_Seguindo", IsUnique = true)]
public partial class Seguindo
{
    [Key]
    public int id_seguindo { get; set; }

    public int seguidor_id { get; set; }

    public int seguido_id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? data_seguindo { get; set; }

    [ForeignKey("seguido_id")]
    [InverseProperty("Seguindoseguidos")]
    public virtual Usuario seguido { get; set; } = null!;

    [ForeignKey("seguidor_id")]
    [InverseProperty("Seguindoseguidors")]
    public virtual Usuario seguidor { get; set; } = null!;
}

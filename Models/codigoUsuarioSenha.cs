using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetoCinemanticaMVC.Models;

[Table("codigoUsuarioSenha")]
public partial class codigoUsuarioSenha
{
    [Key]
    public int id { get; set; }

    public int? id_usuario { get; set; }

    public int? codigo { get; set; }

    [ForeignKey("id_usuario")]
    [InverseProperty("codigoUsuarioSenhas")]
    public virtual Usuario? id_usuarioNavigation { get; set; }
}

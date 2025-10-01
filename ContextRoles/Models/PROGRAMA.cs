using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutenticacionNetCore.Models;

[Table("PROGRAMAS")]
public partial class PROGRAMA
{
    [Key]
    public Guid ID { get; set; }

    [Column("PROGRAMA")]
    [StringLength(60)]
    [Unicode(false)]
    public string? PROGRAMA1 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? OWNER { get; set; }

    public bool ACTIVO { get; set; }

    [InverseProperty("ID_PROGRAMANavigation")]
    public virtual ICollection<GRUPO> GRUPOs { get; set; } = new List<GRUPO>();

    [InverseProperty("ID_PROGRAMANavigation")]
    public virtual ICollection<REL_ROL_GRUPO> REL_ROL_GRUPOs { get; set; } = new List<REL_ROL_GRUPO>();

    [InverseProperty("ID_PROGRAMANavigation")]
    public virtual ICollection<ROLE> ROLEs { get; set; } = new List<ROLE>();
}

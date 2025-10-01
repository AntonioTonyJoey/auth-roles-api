using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutenticacionNetCore.Models;

[Table("GRUPOS")]
public partial class GRUPO
{
    [Key]
    public Guid ID { get; set; }

    public Guid ID_PROGRAMA { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string NOMBRE { get; set; } = null!;

    public bool ACTIVO { get; set; }

    [ForeignKey("ID_PROGRAMA")]
    [InverseProperty("GRUPOs")]
    public virtual PROGRAMA ID_PROGRAMANavigation { get; set; } = null!;

    [InverseProperty("ID_GRUPONavigation")]
    public virtual ICollection<REL_ROL_GRUPO> REL_ROL_GRUPOs { get; set; } = new List<REL_ROL_GRUPO>();
}

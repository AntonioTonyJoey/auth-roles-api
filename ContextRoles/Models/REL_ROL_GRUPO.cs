using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutenticacionNetCore.Models;

[Table("REL_ROL_GRUPO")]
public partial class REL_ROL_GRUPO
{
    [Key]
    public Guid ID { get; set; }

    public Guid? ID_GRUPO { get; set; }

    public Guid? ID_ROL { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string WIW { get; set; } = null!;

    public bool ACTIVO { get; set; }

    public Guid? ID_PROGRAMA { get; set; }

    public int? TEAM { get; set; }

    public int? POOL { get; set; }

    [ForeignKey("ID_GRUPO")]
    [InverseProperty("REL_ROL_GRUPOs")]
    public virtual GRUPO? ID_GRUPONavigation { get; set; }

    [ForeignKey("ID_PROGRAMA")]
    [InverseProperty("REL_ROL_GRUPOs")]
    public virtual PROGRAMA? ID_PROGRAMANavigation { get; set; }

    [ForeignKey("ID_ROL")]
    [InverseProperty("REL_ROL_GRUPOs")]
    public virtual ROLE? ID_ROLNavigation { get; set; }
}

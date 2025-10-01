using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutenticacionNetCore.Models;

[Table("ROLES")]
public partial class ROLE
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string NOMBRE { get; set; } = null!;

    public Guid? ID_PROGRAMA { get; set; }

    public bool ACTIVO { get; set; }

    [ForeignKey("ID_PROGRAMA")]
    [InverseProperty("ROLEs")]
    public virtual PROGRAMA? ID_PROGRAMANavigation { get; set; }

    [InverseProperty("ID_ROLNavigation")]
    public virtual ICollection<REL_ROL_GRUPO> REL_ROL_GRUPOs { get; set; } = new List<REL_ROL_GRUPO>();
}

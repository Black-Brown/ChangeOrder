using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeOrder.Domain.Abstractions;

/// <summary>Contrato de borrado lógico — nunca borrar físicamente </summary>
public interface ISoftDeletable
{
    /// <summary>Flag de borrado lógico. DEFAULT false.</summary>
    bool IsDeleted { get; set; }

    /// <summary>Timestamp del borrado lógico (UTC). Set automáticamente por AuditInterceptor.</summary>
    DateTime? DeletedAt { get; set; }

}

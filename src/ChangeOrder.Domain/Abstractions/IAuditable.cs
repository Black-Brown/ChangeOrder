using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeOrder.Domain.Abstractions;

/// <summary>Contrato de auditoría automática.</summary>
public interface IAuditable
{
    /// <summary>Timestamp de creación (UTC). Solo se asigna una vez.</summary>
    DateTime CreatedAt { get; }

    /// <summary>Timestamp de última modificación (UTC). Actualizado por AuditInterceptor.</summary>
    DateTime? UpdatedAt { get; set; }
}

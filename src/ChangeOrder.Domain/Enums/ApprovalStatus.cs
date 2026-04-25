using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeOrder.Domain.Enums;

/// <summary>
/// Indica el estado de aprobación de una orden
/// </summary>
public enum ApprovalStatus
{
    /// <summary>
    /// Esperando decision — estado inicial de toda aprobacion
    /// </summary>
    Pending,

    /// <summary>
    /// Aprobado por el responsable de ese nivel
    /// </summary>
    Approved,

    /// <summary>
    /// Rechazado — la orden puede ser cancelada o revisada
    /// </summary>
    Rejected
}
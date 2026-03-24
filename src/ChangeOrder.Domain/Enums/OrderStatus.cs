using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeOrder.Domain.Enums;

/// <summary>
/// Representa el estado del ciclo de vida completo de la orden de cambio. Almacenado como string en BD
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Borrador — recien creada, no enviada a aprobacion
    /// </summary>
    Draft,

    /// <summary>
    /// Pendiente de aprobacion por la cadena de 4 niveles
    /// </summary>
    PendingApproval,

    /// <summary>
    /// Aprobada completamente — lista para iniciar el trabajo
    /// </summary>
    Approved,

    /// <summary>
    /// El programador esta trabajando activamente en el cambio
    /// </summary>
    InProgress,

    /// <summary>
    /// Cambio desplegado exitosamente en produccion
    /// </summary>
    Deployed,

    /// <summary>
    /// Cancelada — puede ser por rechazo o decision del negocio
    /// </summary>
    Canceled

}
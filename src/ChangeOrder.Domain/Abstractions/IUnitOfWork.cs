using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeOrder.Domain.Abstractions;

/// <summary>Patrón Unit of Work — gestiona transacciones atómicas.</summary>

public interface IUnitOfWork
{
    /// <summary> Persiste todos los cambios pendientes en la base de datos </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
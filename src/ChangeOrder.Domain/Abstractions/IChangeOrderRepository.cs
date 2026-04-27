using System;
using System.Collections.Generic;
using System.Text;
using ChangeOrder.Domain.Entities;
using ChangeOrder.Domain.Models;

namespace ChangeOrder.Domain.Abstractions;

/// <summary>Contrato de acceso a datos para órdenes de cambio.</summary>
public interface IChangeOrderRepository
{
    /// <summary>Obtiene una orden por Id. Retorna null si no existe o está soft-deleted.</summary>
    Task<ChangeOrderEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    /// <summary>Lista paginada de órdenes. Excluye soft-deleted automáticamente.</summary>
    Task<PagedResponse<ChangeOrderEntity>> GetAllAsync(
        PagedRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Órdenes de una fecha específica.</summary>
    Task<IReadOnlyList<ChangeOrderEntity>> GetByDateAsync(
        DateTime date,
        CancellationToken cancellationToken = default);

    /// <summary>Próximo número secuencial para generar OrderNumber — thread-safe.</summary>
    Task<int> GetNextSequenceForDateAsync(
        DateTime date,
        CancellationToken cancellationToken = default);

    /// <summary>Agrega nueva orden al contexto. SaveChanges lo maneja UnitOfWork.</summary>
    Task AddAsync(
        ChangeOrderEntity order,
        CancellationToken cancellationToken = default);

    /// <summary>Marca la entidad como Modified en EF ChangeTracker.</summary>
    void Update(ChangeOrderEntity order);

    /// <summary>Marca para borrado — AuditInterceptor convierte en soft delete.</summary>
    void Delete(ChangeOrderEntity order);
}
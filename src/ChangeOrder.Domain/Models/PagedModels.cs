using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeOrder.Domain.Models;

/// <summary>Parámetros de paginación para consultas de listas.</summary>
public sealed record PagedRequest(int Page = 1, int PageSize = 10)
{
    /// <summary>Número de página (mínimo 1).</summary>
    public int Page { get; init; } = Page < 1 ? 1 : Page;

    /// <summary>Tamaño de página (entre 1 y 50).</summary>
    public int PageSize { get; init; } = PageSize > 50 ? 50 : PageSize < 1 ? 10 : PageSize;
}

/// <summary>Respuesta paginada genérica.</summary>
public sealed record PagedResponse<T>(
    IReadOnlyList<T> Items,
    int TotalCount,
    int Page,
    int PageSize)
{
    /// <summary>Total de páginas.</summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    /// <summary>Indica si existe una página siguiente.</summary>
    public bool HasNextPage => Page < TotalPages;

    /// <summary>Indica si existe una página anterior.</summary>
    public bool HasPreviousPage => Page > 1;
}
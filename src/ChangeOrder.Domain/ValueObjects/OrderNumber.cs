using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeOrder.Domain.ValueObjects;

/// <summary>
/// Número de orden con formato yyyyMMdd-## (ej: 20260224-01).
/// Máximo 13 caracteres. Único en base de datos.
/// </summary>
public sealed class OrderNumber
{
    /// <summary> Valor formateado del número de orden </summary>
    public string Value { get; }

    private OrderNumber(string value) => Value = value;

    /// <summary> Crea un número de orden a partir de una fecha y número de secuencia </summary>
    public static OrderNumber Create(DateTime date, int sequence) =>
        new($"{date:yyyyMMdd}-{sequence:D2}");

    /// <summary> Restaura un número de orden existente desde persistencia </summary>
    public static OrderNumber Restore(string value) => new(value);

    /// <inheritdoc/>
    public override string ToString() => Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) =>
        obj is OrderNumber other && Value == other.Value;

    /// <inheritdoc/>
    public override int GetHashCode() => Value.GetHashCode();
}
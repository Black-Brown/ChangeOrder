using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeOrder.Domain.Errors;
/// <summary> Errores de dominio del sistema de ordenes de cambio </summary>

public static class DomainErrors
{
    /// <summary> Errores relacionados con ordenes de cambios </summary>
    public static class Order
    {
        /// <summary> La order solicitada no fue encontrada </summary>
        public static Error NotFound(Guid id) =>
            new("Order.NotFound", $"Order con Id {id} no fue encontrado");

        /// <summary> Ya existe una orden con ese numero </summary>
        public static readonly Error DuplicateNumber =
            new("Order.DuplicateNumber", "Ya existe una order con ese numero");

        /// <summary> El rango de fecha proporcionado no es valido </summary>
        public static readonly Error InvalidDateRange =
            new("Order.InvalidDateRange", "El rango de fecha es invalido");

        /// <summary> Ya existe una orden con esta clave de idempotencia </summary>
        public static readonly Error DuplicateIdempotencyKey =
            new("Order.DuplicateIdempotencyKey", "Ya existe una orden con esta clave de idempotencia");

        /// <summary> No se puede modifica una orden con ese estado </summary>
        public static Error InvalidStatusTransition(string currentStatus) =>
            new("Order.InvalidStatusTransition", $"No se puede modificar una orden en estado '{currentStatus}'");
    }

}

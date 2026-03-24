using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChangeOrder.Domain.Errors;

/// <summary>Representa un error de dominio con código y descripción.</summary>
public sealed record Error(string Code, string Description)
{
    /// <summary> Error vacío — representa ausencia de error </summary>
    public static readonly Error None = new(string.Empty, string.Empty);
}

/// <summary>
/// Contenedor de resultado que encapsula éxito o fallo sin usar excepciones.
/// </summary>
public sealed class Result<TValue, TError>
{
    private readonly TValue? _value;
    private readonly TError? _error;

    /// <summary> Indica si la operación fue exitosa </summary>
    public bool IsSuccess { get; }

    /// <summary> Indica si la operación falló </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary> Valor del resultado exitoso </summary>
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("No hay valor en un resultado fallido");

    /// <summary> Error del resultado fallido </summary>
    public TError Error => IsFailure
        ? _error!
        : throw new InvalidOperationException("No hay error en un resultado fallido");

    private Result(TValue? value)
    {
        _value = value;
        IsSuccess = true;
    }

    private Result(TError? error)
    {
        _error = error;
        IsSuccess = false;
    }

    /// <summary>Crea un resultado exitoso.</summary>
    public static Result<TValue, TError> Success(TValue value) => new(value);

    /// <summary>Crea un resultado fallido.</summary>
    public static Result<TValue, TError> Failure(TError error) => new(error);
}

/// <summary> Tipo Unit para comandos sin valor de retorno </summary>
public sealed record Unit
{
    /// <summary> Instancia única de Unit </summary>
    public static readonly Unit Value = new();

    private Unit() { }
}
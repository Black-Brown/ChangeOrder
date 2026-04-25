using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeOrder.Domain.ValueObjects;

/// <summary> 
/// Información del solicitante del cambio.
/// Se almacena como columnas propias en la tabla con prefijo Requester_.
/// </summary>
public sealed class RequesterInfo
{
    /// <summary> Nombre completo del solicitante. </summary>
    public string Name { get; }

    /// <summary> Cargo del solicitante </summary>
    public string Position { get; }

    /// <summary> Departamento del solicitante </summary>
    public string Department { get; }

    /// <summary> Correo electrónico del solicitante </summary>
    public string Email { get; }

    private RequesterInfo(string name, string position, string department, string email)
    {
        Name = name;
        Position = position;
        Department = department;
        Email = email;
    }

    /// <summary> Crea una instancia de <see cref="RequesterInfo"/> </summary>
    public static RequesterInfo Create(
        string name,
        string position,
        string deparment,
        string email) => new(name, position, deparment, email);

    /// <inheritdoc/>
    public override bool Equals(object? obj) =>
        obj is RequesterInfo other &&
        Name == other.Name &&
        Position == other.Position &&
        Department == other.Department &&
        Email == other.Email;

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(Name, Position, Department, Email);

}
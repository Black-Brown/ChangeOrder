using ChangeOrder.Domain.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace ChangeOrder.Domain.Tests.ValueObjects;
/// <summary>
/// Pruebas unitarias para el Value Object OrderNumber.
/// </summary>
public sealed class OrderNumberTests
{
    /// <summary>
    /// Verifica que al crear un OrderNumber con secuencia menor a 10
    /// el formato sea correcto con relleno de cero.
    /// </summary>
    [Fact]
    public void Create_ValidDateAndSequence_ReturnsCorrectFormat()
    {
        DateTime date = new(2026, 2, 24);
        int sequence = 1;

        OrderNumber result = OrderNumber.Create(date, sequence);

        result.Value.Should().Be("20260224-01");
    }

    /// <summary>
    /// Verifica que al crear un OrderNumber con secuencia mayor a 9
    /// el formato sea correcto sin relleno de cero.
    /// </summary>
    [Fact]
    public void Create_SequenceGreaterThanNine_ReturnsCorrectFormat()
    {
        DateTime date = new(2026, 2, 24);
        int sequence = 10;

        OrderNumber result = OrderNumber.Create(date, sequence);

        result.Value.Should().Be("20260224-10");
    }

    /// <summary>
    /// Verifica que al restaurar un OrderNumber desde su representación
    /// en cadena el valor coincida exactamente.
    /// </summary>
    [Fact]
    public void Restore_ValidValue_ReturnsOrderNumber()
    {
        string value = "20260224-01";

        OrderNumber result = OrderNumber.Restore(value);

        result.Value.Should().Be(value);
    }

    /// <summary>
    /// Verifica que dos OrderNumber con el mismo valor sean iguales.
    /// </summary>
    [Fact]
    public void Equals_SameValue_ReturnsTrue()
    {
        OrderNumber first = OrderNumber.Create(new DateTime(2026, 2, 24), 1);
        OrderNumber second = OrderNumber.Restore("20260224-01");

        bool result = first.Equals(second);

        result.Should().BeTrue();
    }

    /// <summary>
    /// Verifica que dos OrderNumber con valores distintos no sean iguales.
    /// </summary>
    [Fact]
    public void Equals_DifferentValue_ReturnsFalse()
    {
        OrderNumber first = OrderNumber.Create(new DateTime(2026, 2, 24), 1);
        OrderNumber second = OrderNumber.Create(new DateTime(2026, 2, 24), 2);

        bool result = first.Equals(second);

        result.Should().BeFalse();
    }
}



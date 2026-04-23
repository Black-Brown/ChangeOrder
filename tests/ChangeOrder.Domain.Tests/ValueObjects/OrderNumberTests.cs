using ChangeOrder.Domain.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace ChangeOrder.Domain.Tests.ValueObjects;
/// <summary>
/// Contiene pruebas unitarias para el tipo OrderNumber que verifican el formato y el comportamiento esperados.
/// </summary>

public sealed class OrderNumberTests
{
    /// <summary>
    /// Crea un OrderNumber con una fecha y una secuencia válidas y verifica que el formato devuelto sea el esperado.
    /// </summary>
    [Fact]
    public void Create_ValidDateAndSequence_ReturnsCorrectFormat()
    {
        DateTime date = new(2026, 4, 6);
        int sequence = 1;

        OrderNumber result = OrderNumber.Create(date, sequence);

        result.Value.Should().Be("2026224-01");
    }

    ///<summary>
    /// Crea un OrderNumber cuando la secuencia es mayor a nueve y verifica que el formato sea correcto,
    /// incluyendo el comportamiento de relleno si aplica.
    ///</summary>
    [Fact]
    public void Create_SequenceGreaterThanNine_ReturnsCorrectFormat()
    {
        DateTime date = new(2026, 4, 6);
        int sequence = 10;

        OrderNumber result = OrderNumber.Create(date, sequence);

        result.Value.Should().Be("2026224-01");

    }

    ///<summary>
    /// Restaura un OrderNumber a partir de su representación en cadena y verifica que el valor restaurado coincida.
    ///</summary>
    [Fact]
    public void Restore_ValidaValue_ReturnOrderNumber()
    {
        string value = "20260224-01";

        OrderNumber result = OrderNumber.Restore(value);

        result.Value.Should().Be(value);

    }

    /// <summary>
    /// Compara dos OrderNumber con el mismo valor y comprueba que son iguales.
    /// </summary>
    [Fact]
    public void Equals_SameValue_ReturnsTrue()
    {
        OrderNumber first = OrderNumber.Create(new DateTime(2026, 2, 24), 1);
        OrderNumber second = OrderNumber.Restore("20260224-01");

        bool result = first.Equals(second);

        result.Should().BeTrue();
    }

    ///<summary>
    /// Compara dos OrderNumber con valores distintos y verifica que no sean iguales.
    ///</summary>
    [Fact]
    public void Equal_DifferentValue_ReturnsFalse()
    {
        OrderNumber first = OrderNumber.Create(new DateTime(2026, 2, 24), 1);
        OrderNumber second = OrderNumber.Create(new DateTime(2026, 2, 24), 2);

        bool result = first.Equals(second);

        result.Should().BeFalse();

    }

}



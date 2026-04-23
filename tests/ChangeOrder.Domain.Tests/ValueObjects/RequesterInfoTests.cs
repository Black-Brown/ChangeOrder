using System;
using System.Collections.Generic;
using System.Text;
using ChangeOrder.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace ChangeOrder.Domain.Tests.ValueObjects;

/// <summary>
/// Pruebas unitarias para el objeto de valor RequesterInfo.
/// </summary>
public sealed class RequesterInfoTests
{
    /// <summary>
    /// Verifica que al crear un RequesterInfo con datos válidos se obtenga una instancia
    /// con las propiedades Name, Position, Department y Email establecidas correctamente.
    /// </summary>
    [Fact]
    public void Create_ValidData_ReturnRequesterInfo()
    {
        string name = "Benito Diaz";
        string position = "Analista";
        string department = "TI";
        string email = "benito@empresa.com";

        RequesterInfo result = RequesterInfo.Create(name, position, department, email);

        result.Name.Should().Be(name);
        result.Position.Should().Be(position);
        result.Department.Should().Be(department);
        result.Email.Should().Be(email);

    }

    /// <summary>
    /// Verifica que dos instancias creadas con los mismos datos sean consideradas iguales.
    /// </summary>
    [Fact]
    public void Equals_SameData_ReturnsTrue()
    {
        RequesterInfo first = RequesterInfo.Create("Benito Diaz", "Analista", "TI", "benito@empresa.com");
        RequesterInfo second = RequesterInfo.Create("Benito Diaz", "Analista", "TI", "benito@empresa.com");

        bool result = first.Equals(second);
        result.Should().BeTrue();
    }

    /// <summary>
    /// Verifica que dos instancias con datos diferentes no sean consideradas iguales.
    /// </summary>
    [Fact]
    public void Equals_DifferentData_ReturnsFalse()
    {
        RequesterInfo first = RequesterInfo.Create("Benito Diaz", "Analista", "TI", "benito@empresa.com");
        RequesterInfo second = RequesterInfo.Create("Maria Lopez", "Gerente", "Finanzas", "benito@empresa.com");

        bool result = first.Equals(second);

        result.Should().BeFalse();
    }
}
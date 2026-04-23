using System;
using System.Collections.Generic;
using System.Text;
using ChangeOrder.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace ChangeOrder.Domain.Tests.ValueObjects;

/// <summary>
/// 
/// </summary>
public sealed class RequesterInfoTests
{
    ///<summary>
    ///
    ///</summary>
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
    ///<summary>
    ///
    ///</summary>
    public void Equals_SameData_ReturnsTrue()
    {
        RequesterInfo first = RequesterInfo.Create("Benito Diaz", "Analista", "TI", "

}

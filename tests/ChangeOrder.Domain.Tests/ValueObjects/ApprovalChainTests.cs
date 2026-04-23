using ChangeOrder.Domain.Enums;
using ChangeOrder.Domain.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Xunit;

namespace ChangeOrder.Domain.Tests.ValueObjects;

/// <summary>
/// Pruebas unitarias para el objeto de valor ApprovalChain, verificando la creación y las consultas de estado.
/// </summary>
public sealed class ApprovalChainTests
{
    /// <summary>
    /// Verifica que CreatePending inicializa todos los niveles de aprobación en Pending.
    /// </summary>
    [Fact]
    public void CreatePending_AllLevels_ArePeding()
    {
        ApprovalChain result = ApprovalChain.CreatePending();

        result.RequesterApproval.Should().Be(ApprovalStatus.Pending);
        result.DepartmentHeadApproval.Should().Be(ApprovalStatus.Pending);
        result.ItHeadApproval.Should().Be(ApprovalStatus.Pending);
        result.ProgrammingDivisionApproval.Should().Be(ApprovalStatus.Pending);
    }

    /// <summary>
    /// Verifica que IsFullyApproved devuelve true cuando todos los niveles de aprobación son Approved.
    /// </summary>
    [Fact]
    public void IsFullyApproved_AllAproved_ReturnsTrue()
    {
        ApprovalChain result = ApprovalChain.Restore(
            ApprovalStatus.Approved,
            ApprovalStatus.Approved,
            ApprovalStatus.Approved,
            ApprovalStatus.Approved);

        result.IsFullyApproved.Should().BeTrue();
    }

    /// <summary>
    /// Verifica que IsFullyApproved devuelve false si algún nivel de aprobación aún está Pending.
    /// </summary>
    [Fact]
    public void IsFullyApproved_OnePending_ReturnsFalse()
    {
        ApprovalChain result = ApprovalChain.Restore(
            ApprovalStatus.Approved,
            ApprovalStatus.Approved,
            ApprovalStatus.Pending,
            ApprovalStatus.Approved);
        result.IsFullyApproved.Should().BeFalse();
    }

    /// <summary>
    /// Crea una cadena de aprobación con un nivel Rejected y verifica que IsFullyApproved es false.
    /// </summary>
    [Fact]
    public void HasRejection_OneRejected_ReturnsFalse()
    {
        ApprovalChain result = ApprovalChain.Restore(
            ApprovalStatus.Approved,
            ApprovalStatus.Approved,
            ApprovalStatus.Rejected,
            ApprovalStatus.Approved);
        result.IsFullyApproved.Should().BeFalse();
    }

    /// <summary>
    /// Verifica que HasRejection es false cuando ningún nivel de aprobación está Rejected (todos están Pending).
    /// </summary>
    [Fact]
    public void HasRejection_NoneRejected_ReturnsFalse()
    {
        ApprovalChain result = ApprovalChain.CreatePending();

        result.HasRejection.Should().BeFalse();
    }

}

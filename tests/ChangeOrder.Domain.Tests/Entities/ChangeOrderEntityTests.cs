using ChangeOrder.Domain.Entities;
using ChangeOrder.Domain.Enums;
using ChangeOrder.Domain.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ChangeOrder.Domain.Tests.Entities;

/// <summary>
/// Pruebas unitarias para la entidad ChangeOrderEntity.
/// </summary>
public sealed class ChangeOrderEntityTests
{
    /// <summary>
    /// Crea y devuelve una instancia válida de ChangeOrderEntity utilizada en los tests.
    /// </summary>
    private static ChangeOrderEntity CreateValidOrder()
    {
        OrderNumber number = OrderNumber.Create(new DateTime(2026, 2, 24), 1);
        RequesterInfo requester = RequesterInfo.Create(
            "Benito Diaz", "Analista", "TI", "benito@empresa.com");

        return ChangeOrderEntity.Create(
            number,
            "Sistema de Nómina",
            "1.0.0",
            new DateTime(2026, 2, 24),
            "Corrección de cálculo",
            "El cálculo de horas extras falla",
            "Impacta el pago de empleados",
            "Revisar módulo de horas extras",
            requester);
    }

    /// <summary>
    /// Verifica que al crear una orden válida, el estado inicial sea Draft, no esté marcada como eliminada y tenga Id no vacío.
    /// </summary>
    [Fact]
    public void Create_ValidData_ReturnsEntityWithDraftStatus()
    {
        ChangeOrderEntity order = CreateValidOrder();

        order.Status.Should().Be(OrderStatus.Draft);
        order.IsDeleted.Should().BeFalse();
        order.Id.Should().NotBeEmpty();
    }

    /// <summary>
    /// Verifica que todas las aprobaciones iniciales estén en estado Pending.
    /// </summary>
    [Fact]
    public void Create_ValidData_ApprovalsAllPending()
    {
        ChangeOrderEntity order = CreateValidOrder();

        order.Approvals.RequesterApproval.Should().Be(ApprovalStatus.Pending);
        order.Approvals.DepartmentHeadApproval.Should().Be(ApprovalStatus.Pending);
        order.Approvals.ItHeadApproval.Should().Be(ApprovalStatus.Pending);
        order.Approvals.ProgrammingDivisionApproval.Should().Be(ApprovalStatus.Pending);
    }

    /// <summary>
    /// Verifica que la fecha CreatedAt esté en formato UTC.
    /// </summary>
    [Fact]
    public void Create_ValidData_CreatedAtIsUtc()
    {
        ChangeOrderEntity order = CreateValidOrder();

        order.CreatedAt.Kind.Should().Be(DateTimeKind.Utc);
    }

    /// <summary>
    /// Verifica que SetStatus actualice correctamente el estado de la orden.
    /// </summary>
    [Fact]
    public void SetStatus_ValidStatus_UpdatesStatus()
    {
        ChangeOrderEntity order = CreateValidOrder();

        order.SetStatus(OrderStatus.InProgress);

        order.Status.Should().Be(OrderStatus.InProgress);
    }

    /// <summary>
    /// Verifica que al registrar un despliegue se actualice el estado a Deployed y se establezcan la fecha y la ruta de la captura posterior.
    /// </summary>
    [Fact]
    public void RegisterDeployment_ValidData_SetsDeployedStatus()
    {
        ChangeOrderEntity order = CreateValidOrder();
        DateTime deployDate = new(2026, 3, 1);

        order.RegisterDeployment(deployDate, "screenshots/post.png");

        order.Status.Should().Be(OrderStatus.Deployed);
        order.ProductionDeployDate.Should().Be(deployDate);
        order.PostDeployScreenshotPath.Should().Be("screenshots/post.png");
    }
}
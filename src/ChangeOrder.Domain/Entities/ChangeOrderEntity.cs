using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ChangeOrder.Domain.Abstractions;
using ChangeOrder.Domain.Enums;
using ChangeOrder.Domain.ValueObjects;

namespace ChangeOrder.Domain.Entities;

/// <summary>
/// Aggregate Root - representa una solicitud de cambio completa.
/// Tabla: dbo.ChangeOrders
/// </summary>
public sealed class ChangeOrderEntity : IAuditable, ISoftDeletable
{
    /// <summary>Identificador único de la orden.</summary>
    public Guid Id { get; private set; }

    /// <summary>Número de orden con formato yyyyMMdd-## — único en BD.</summary>
    public OrderNumber Number { get; private set; } = null!;

    /// <summary>Nombre del programa o módulo a cambiar.</summary>
    public string ProgramName { get; private set; } = string.Empty;

    /// <summary>Versión en producción antes del cambio.</summary>
    public string ProductionVersion { get; private set; } = string.Empty;

    /// <summary>Ruta al screenshot pre-cambio (opcional).</summary>
    public string? VersionScreenshotPath { get; private set; }

    /// <summary>Fecha en que se solicita el cambio.</summary>
    public DateTime RequestDate { get; private set; }

    /// <summary>Descripción breve del trabajo solicitado.</summary>
    public string WorkDescription { get; private set; } = string.Empty;

    /// <summary>Detalle completo de la solicitud.</summary>
    public string RequestDetails { get; private set; } = string.Empty;

    /// <summary>Justificación del negocio para el cambio.</summary>
    public string Justification { get; private set; } = string.Empty;

    /// <summary>Acción requerida al programador.</summary>
    public string RequiredAction { get; private set; } = string.Empty;

    /// <summary>Información del solicitante.</summary>
    public RequesterInfo Requester { get; private set; } = null!;

    /// <summary>Cadena de aprobación en 4 niveles.</summary>
    public ApprovalChain Approvals { get; private set; } = null!;

    /// <summary>Estado del ciclo de vida de la orden.</summary>
    public OrderStatus Status { get; private set; }

    /// <summary>Fecha estimada de entrega (opcional).</summary>
    public DateTime? DeliveryDate { get; private set; }

    /// <summary>Fecha de primera evaluación técnica (opcional).</summary>
    public DateTime? InitialEvaluationDate { get; private set; }

    /// <summary>Fecha real de despliegue en producción (opcional).</summary>
    public DateTime? ProductionDeployDate { get; private set; }

    /// <summary>Ruta al screenshot post-cambio (opcional).</summary>
    public string? PostDeployScreenshotPath { get; private set; }

    /// <inheritdoc/>
    public DateTime CreatedAt { get; private set; }

    /// <inheritdoc/>
    public DateTime? UpdatedAt { get; set; }

    /// <inheritdoc/>
    public bool IsDeleted { get; set; }

    /// <inheritdoc/>
    public DateTime? DeletedAt { get; set; }

    private ChangeOrderEntity() { }

    /// <summary>Crea una nueva orden de cambio.</summary>
    public static ChangeOrderEntity Create(
        OrderNumber number,
        string programName,
        string productionVersion,
        DateTime requestDate,
        string workDescription,
        string requestDetails,
        string justification,
        string requiredAction,
        RequesterInfo requester)
    {
        return new ChangeOrderEntity
        {
            Id = Guid.NewGuid(),
            Number = number,
            ProgramName = programName,
            ProductionVersion = productionVersion,
            RequestDate = requestDate,
            WorkDescription = workDescription,
            RequestDetails = requestDetails,
            Justification = justification,
            RequiredAction = requiredAction,
            Requester = requester,
            Approvals = ApprovalChain.CreatePending(),
            Status = OrderStatus.Draft,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
    }

    /// <summary>Actualiza los campos editables de la orden.</summary>
    public void Update(
        string programName,
        string productionVersion,
        string workDescription,
        string requestDetails,
        string justification,
        string requiredAction,
        RequesterInfo requester,
        DateTime? deliveryDate,
        DateTime? initialEvaluationDate,
        string? versionScreenshotPath)
    {
        ProgramName = programName;
        ProductionVersion = productionVersion;
        WorkDescription = workDescription;
        RequestDetails = requestDetails;
        Justification = justification;
        RequiredAction = requiredAction;
        Requester = requester;
        DeliveryDate = deliveryDate;
        InitialEvaluationDate = initialEvaluationDate;
        VersionScreenshotPath = versionScreenshotPath;
    }

    /// <summary>Actualiza el estado del ciclo de vida.</summary>
    public void SetStatus(OrderStatus status) => Status = status;

    /// <summary>Registra el despliegue en producción.</summary>
    public void RegisterDeployment(DateTime deployDate, string? screenshotPath)
    {
        ProductionDeployDate = deployDate;
        PostDeployScreenshotPath = screenshotPath;
        Status = OrderStatus.Deployed;
    }
}
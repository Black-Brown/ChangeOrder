using ChangeOrder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeOrder.Domain.ValueObjects;

/// <summary>
/// Cadena de aprobación en 4 niveles jerárquicos.
/// Cada nivel tiene su propio ApprovalStatus independiente.
/// </summary>
public sealed class ApprovalChain
{
    /// <summary> nivel 1 - aprobacion del propio solicitante </summary>
    public ApprovalStatus RequesterApproval {  get; private set; }

    /// <summary> nivel 2 — aprobación del jefe de departamento </summary>
    public ApprovalStatus DepartmentHeadApproval { get; private set; }

    /// <summary> nivel 3 — aprobación del jefe de TI </summary>
    public ApprovalStatus ItHeadApproval { get; private set; }

    /// <summary> nivel 4 — aprobación de la división de programación </summary>
    public ApprovalStatus ProgrammingDivisionApproval { get; private set; }

    private ApprovalChain(
        ApprovalStatus requester,
        ApprovalStatus departmentHead,
        ApprovalStatus itHead,
        ApprovalStatus programmingDivision)

    {
        RequesterApproval = requester;
        DepartmentHeadApproval = departmentHead;
        ItHeadApproval = itHead;
        ProgrammingDivisionApproval = programmingDivision;
    }

    /// <summary> Crea una cadena de aprobación con todos los niveles en Pending </summary>
    public static ApprovalChain CreatePending() =>
        new(ApprovalStatus.Pending, ApprovalStatus.Pending, ApprovalStatus.Pending, ApprovalStatus.Pending);

    /// <summary> Restaura una cadena de aprobación existente desde persistencia </summary>
    public static ApprovalChain Restore(
        ApprovalStatus requester,
        ApprovalStatus departmentHead,
        ApprovalStatus itHead,
        ApprovalStatus programmingDivision) =>
        new(requester, departmentHead, itHead, programmingDivision);

    /// <summary>Indica si todos los niveles han aprobado.</summary>
    public bool IsFullyApproved =>
        RequesterApproval == ApprovalStatus.Approved &&
        DepartmentHeadApproval == ApprovalStatus.Approved &&
        ItHeadApproval == ApprovalStatus.Approved &&
        ProgrammingDivisionApproval == ApprovalStatus.Approved;

    /// <summary>Indica si algún nivel ha rechazado.</summary>
    public bool HasRejection =>
        RequesterApproval == ApprovalStatus.Rejected ||
        DepartmentHeadApproval == ApprovalStatus.Rejected ||
        ItHeadApproval == ApprovalStatus.Rejected ||
        ProgrammingDivisionApproval == ApprovalStatus.Rejected;

}

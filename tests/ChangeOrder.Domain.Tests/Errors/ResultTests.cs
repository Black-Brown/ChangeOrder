using ChangeOrder.Domain.Errors;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ChangeOrder.Domain.Tests.Errors;

///<summary>
/// Pruebas unitarias para la clase Result TValue, TError, que representa
/// el resultado de una operación que puede ser exitosa o fallida.
///</summary>
public sealed class ResultTests
{
    ///<summary>
    /// Verifica que al crear un Result exitoso con un valor:
    /// - IsSuccess sea true
    /// - IsFailure sea false
    /// - Value devuelva el valor proporcionado
    ///</summary>
    [Fact]
    public void Success_WithValue_IsSuccessTrue()
    {
        Result<string, Error> result = Result<string, Error>.Success("Ok");

        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Value.Should().Be("Ok");
    }

    /// <summary>
    /// Verifica que al crear un Result fallido con un Error:
    /// - IsSuccess sea false
    /// - IsFailure sea true
    /// - Error devuelva la instancia de Error proporcionada
    /// </summary>
    [Fact]
    public void Failure_WithError_IsFailureTrue()
    {
        Error error = new("Test.Error", "Descripcion del error.");

        Result<string, Error> result = Result<string, Error>.Failure(error);

        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }

    ///<summary>
    /// Verifica que intentar acceder a la propiedad Error en un Result exitoso
    /// lance InvalidOperationException, ya que no existe un error en ese estado.
    ///</summary>
    [Fact]
    public void Success_AccessError_ThrowsInvalidOperationException()
    {
        Result<string, Error> result = Result<string, Error>.Success("Ok");

        Action act = () => { var error = result.Error; };

        act.Should().Throw<InvalidOperationException>();
    }

    ///<summary>
    /// Verifica que intentar acceder a la propiedad Value en un Result fallido
    /// lance InvalidOperationException, ya que no existe un valor en ese estado.
    ///</summary>
    [Fact]
    public void Failure_AccessValue_ThrowsInvalidOperationException()
    {
        Result<string, Error> result = Result<string, Error>.Failure(
            new Error("Test.Error", "Descripcion del error."));

        Action act = () => _ = result.Value;

        act.Should().Throw<InvalidOperationException>();
    }

}

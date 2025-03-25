using Castle.Core.Logging;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using Xunit;

namespace PrintingServer.API.MiddleWares.Tests;

public class ErrorHandlingMiddleWareTests
{
    [Fact()]
    public async void MiddleWate_NoException_CallNext()
    {
        var loggerMock = new Mock<ITQLogger<ErrorHandlingMiddleWare>>();
        var middleware = new ErrorHandlingMiddleWare(loggerMock.Object);
        var context = new DefaultHttpContext();
        var nextDelegateMock = new Mock<RequestDelegate>();

        await middleware.InvokeAsync(context, nextDelegateMock.Object);

        nextDelegateMock.Verify(next => next.Invoke(context), Times.Once);

    }

    [Fact()]
    public async void MiddleWate_Exception_404()
    {
        var loggerMock = new Mock<ITQLogger<ErrorHandlingMiddleWare>>();
        var middleware = new ErrorHandlingMiddleWare(loggerMock.Object);
        var context = new DefaultHttpContext();
        var exception = new NotFoundException(nameof(Client), "404 NF");

        await middleware.InvokeAsync(context, _ => throw exception);

        context.Response.StatusCode.Should().Be(404);

    }

    [Fact()]
    public async void MiddleWate_Exception_302()
    {
        var loggerMock = new Mock<ITQLogger<ErrorHandlingMiddleWare>>();
        var middleware = new ErrorHandlingMiddleWare(loggerMock.Object);
        var context = new DefaultHttpContext();
        var exception = new AlreadyFoundException(nameof(Client), "302");

        await middleware.InvokeAsync(context, _ => throw exception);

        context.Response.StatusCode.Should().Be(302);

    }

    [Fact()]
    public async void MiddleWate_Exception_400()
    {
        var loggerMock = new Mock<ITQLogger<ErrorHandlingMiddleWare>>();
        var middleware = new ErrorHandlingMiddleWare(loggerMock.Object);
        var context = new DefaultHttpContext();
        var validationResult = new ValidationResult();
        validationResult.Errors = new List<ValidationFailure>()
        {
            new ValidationFailure("Key","0")
            {
                    ErrorCode="400",
                    ErrorMessage="Fluent"
            }
        };
        var exception = new FluentValidationException(validationResult);
        

        await middleware.InvokeAsync(context, _ => throw exception);

        context.Response.StatusCode.Should().Be(400);

    }

    [Fact()]
    public async void MiddleWate_Exception_403()
    {
        var loggerMock = new Mock<ITQLogger<ErrorHandlingMiddleWare>>();
        var middleware = new ErrorHandlingMiddleWare(loggerMock.Object);
        var context = new DefaultHttpContext();
        var exception = new ForbidException();

        await middleware.InvokeAsync(context, _ => throw exception);

        context.Response.StatusCode.Should().Be(403);

    }

    [Fact()]
    public async void MiddleWate_Exception_500()
    {
        var loggerMock = new Mock<ITQLogger<ErrorHandlingMiddleWare>>();
        var middleware = new ErrorHandlingMiddleWare(loggerMock.Object);
        var context = new DefaultHttpContext();
        var exception = new Exception();

        await middleware.InvokeAsync(context, _ => throw exception);

        context.Response.StatusCode.Should().Be(500);

    }
}
namespace WebApi.Controllers.Exceptions;

internal class ControllerNotFoundException(string message) : Exception(message)
{
}

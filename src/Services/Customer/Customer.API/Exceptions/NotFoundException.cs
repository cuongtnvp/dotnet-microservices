namespace Customer.API.Exceptions;

public sealed class NotFoundException(string message) : Exception($"{message} not found");
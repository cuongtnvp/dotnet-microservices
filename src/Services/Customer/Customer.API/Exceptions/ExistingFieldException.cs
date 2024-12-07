namespace Customer.API.Exceptions;

public sealed class ExistingFieldException(string? message, string? field) : Exception($"{field}: {message} already exists.");
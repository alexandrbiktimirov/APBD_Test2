namespace Test2.Exceptions;

public class TrackDoesNotExistException(string? message) : Exception(message);
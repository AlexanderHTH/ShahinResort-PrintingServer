namespace PrintingServer.Domain.Exceptions;

public class NotMatchedException(string resourceType, string resourceIdentifier, string compareIdentifier) : Exception($"{resourceType} ID's don't match ({resourceIdentifier},{compareIdentifier}).")
{
}

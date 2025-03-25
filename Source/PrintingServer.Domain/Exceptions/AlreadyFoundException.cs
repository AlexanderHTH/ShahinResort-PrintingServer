namespace PrintingServer.Domain.Exceptions;
public class AlreadyFoundException(string resourceType, string resourceIdentifier) : Exception($"{resourceType} ({resourceIdentifier}) already exist.")
{
}

namespace PrintingServer.Domain.Exceptions;

public class HandlingDataException(string resourceType, string resourceIdentifier) : Exception($"{resourceType} with ID ({resourceIdentifier}) error while hanling data.")
{
}

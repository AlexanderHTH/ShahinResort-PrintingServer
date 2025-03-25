namespace PrintingServer.Domain.Storage;

public interface IBlobStorageService
{
    Task<string> UploadLogoToBlobAsync(Stream fileData, string fileName);
    Task<string> UploadPDFToBlobAsync(Stream fileData, string fileName);
}

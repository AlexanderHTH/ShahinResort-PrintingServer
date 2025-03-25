using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using PrintingServer.Domain.Storage;

namespace PrintingServer.Infrastructure.Storage;

internal class BlobStorageService(IOptions<BlobStorageSettings> options) : IBlobStorageService
{
    private readonly BlobServiceClient blobServiceClient = new BlobServiceClient(options.Value.ConnectionString);

    public async Task<string> UploadLogoToBlobAsync(Stream fileData, string fileName)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(options.Value.LogosContainer);
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileData);
        return blobClient.Uri.ToString();
    }

    public async Task<string> UploadPDFToBlobAsync(Stream fileData, string fileName)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(options.Value.PDFContainer);
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileData);
        return blobClient.Uri.ToString();
    }

}

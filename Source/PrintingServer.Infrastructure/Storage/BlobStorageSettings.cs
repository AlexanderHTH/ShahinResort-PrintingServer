namespace PrintingServer.Infrastructure.Storage
{
    public class BlobStorageSettings
    {
        public string ConnectionString { get; set; } = default!;
        public string LogosContainer { get; set; } = default!;
        public string PDFContainer { get; set; } = default!;
   }
}

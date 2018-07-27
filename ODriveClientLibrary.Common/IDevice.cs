namespace ODriveClientLibrary.Common
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDevice
    {
        Task<T> GetProperty<T>(IReadablePropertyMember<T> readablePropertyMember);
        Task SetProperty<T>(IWriteablePropertyMember<T> writeablePropertyMember, T newValue);
        T GetExecutionDelegate<T>(IExecutableMember<T> executableMember);
        Task<bool> Connect(bool skipChecksumValidation = false);
        bool Disconnect();
        Task<string> DownloadSchema(CancellationToken cancellationToken = default(CancellationToken), bool setSchemaChecksum = true);
        Task InvokeEndpoint(ushort endpointID, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> RequestValue<T>(ushort endpointID, CancellationToken cancellationToken = default(CancellationToken)) where T : struct;
        Task<T> PushValue<T>(ushort endpointID, T newValue, CancellationToken cancellationToken = default(CancellationToken)) where T : struct;
        void Dispose();
    }
}

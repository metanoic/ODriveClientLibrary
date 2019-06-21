namespace ODriveClientLibrary.Common
{
    using System.Threading.Tasks;

    public interface IWriteablePropertyMember<T>
    {
        Task SetProperty(IDevice oDrive, T newValue);
    }
}

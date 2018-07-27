namespace ODriveClientLibrary.Common
{
    using System.Threading.Tasks;

    public interface IReadablePropertyMember<T>
    {
        Task<T> GetProperty(IDevice oDrive);
    }
}

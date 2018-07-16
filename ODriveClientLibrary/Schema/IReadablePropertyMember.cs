namespace ODrive.Schema
{
    using System.Threading.Tasks;

    public interface IReadablePropertyMember<T>
    {
        Task<T> GetProperty(Device oDrive);
    }
}

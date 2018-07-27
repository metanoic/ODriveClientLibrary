namespace ODrive.Schema
{
    using System.Threading.Tasks;

    public interface IWriteablePropertyMember<T>
    {
        Task SetProperty(Device oDrive, T newValue);
    }
}

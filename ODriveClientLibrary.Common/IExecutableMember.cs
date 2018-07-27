namespace ODriveClientLibrary.Common
{
    public interface IExecutableMember<TDelegate>
    {
        TDelegate GetExecutor(IDevice oDrive);
    }
}

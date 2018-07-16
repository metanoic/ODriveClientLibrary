namespace ODrive.Schema
{
    public interface IExecutableMember<TDelegate>
    {
        TDelegate GetExecutor(Device oDrive);
    }
}

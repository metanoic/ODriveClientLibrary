namespace ODriveClientLibrary.DeviceGenerator
{
    public class GenerationResult
    {
        public string Code { get; private set; }
        public GenerationResult(string code)
        {
            Code = code;
        }
    }
}

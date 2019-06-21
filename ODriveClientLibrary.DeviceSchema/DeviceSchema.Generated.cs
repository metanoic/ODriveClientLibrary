namespace ODriveClientLibrary.DeviceSchema
{
    using System;
    using System.Threading.Tasks;
    using ODriveClientLibrary.Common;
    using ODriveClientLibrary.DeviceSchema;

    public partial class DeviceSchema : IDeviceSchema
    {
        public static ushort SchemaChecksum = 56568;
        public GetOscilloscopeValFunction GetOscilloscopeVal = new GetOscilloscopeValFunction();
        public GetAdcVoltageFunction GetAdcVoltage = new GetAdcVoltageFunction();
        public SaveConfigurationMethod SaveConfiguration = new SaveConfigurationMethod();
        public EraseConfigurationMethod EraseConfiguration = new EraseConfigurationMethod();
        public RebootMethod Reboot = new RebootMethod();
        public EnterDfuModeMethod EnterDfuMode = new EnterDfuModeMethod();
        public DeviceSchemaSystemStats SystemStats = new DeviceSchemaSystemStats();
        public DeviceSchemaConfig Config = new DeviceSchemaConfig();
        public DeviceSchemaAxis0 Axis0 = new DeviceSchemaAxis0();
        public DeviceSchemaAxis1 Axis1 = new DeviceSchemaAxis1();
        public DeviceSchemaCan Can = new DeviceSchemaCan();
        public JsonCrcProperty JsonCrc = new JsonCrcProperty();
        public HwVersionMajorProperty HwVersionMajor = new HwVersionMajorProperty();
        public HwVersionMinorProperty HwVersionMinor = new HwVersionMinorProperty();
        public HwVersionVariantProperty HwVersionVariant = new HwVersionVariantProperty();
        public FwVersionMajorProperty FwVersionMajor = new FwVersionMajorProperty();
        public FwVersionMinorProperty FwVersionMinor = new FwVersionMinorProperty();
        public FwVersionRevisionProperty FwVersionRevision = new FwVersionRevisionProperty();
        public FwVersionUnreleasedProperty FwVersionUnreleased = new FwVersionUnreleasedProperty();
        public VbusVoltageProperty VbusVoltage = new VbusVoltageProperty();
        public SerialNumberProperty SerialNumber = new SerialNumberProperty();
        public UserConfigLoadedProperty UserConfigLoaded = new UserConfigLoadedProperty();
        public BrakeResistorArmedProperty BrakeResistorArmed = new BrakeResistorArmedProperty();
        public partial class GetOscilloscopeValFunction : IExecutableMember<GetOscilloscopeValFunction.ExecutionDelegate>
        {
            public delegate Task<float> ExecutionDelegate(uint index);
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async (uint index) =>
                {
                    await oDrive.PushValue<uint>(273, index);
                    return await oDrive.RequestValue<float>(272);
                }

                ;
            }
        }

        public partial class GetAdcVoltageFunction : IExecutableMember<GetAdcVoltageFunction.ExecutionDelegate>
        {
            public delegate Task<float> ExecutionDelegate(uint gpio);
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async (uint gpio) =>
                {
                    await oDrive.PushValue<uint>(276, gpio);
                    return await oDrive.RequestValue<float>(275);
                }

                ;
            }
        }

        public partial class SaveConfigurationMethod : IExecutableMember<SaveConfigurationMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate();
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async () =>
                {
                    await oDrive.InvokeEndpoint(278);
                }

                ;
            }
        }

        public partial class EraseConfigurationMethod : IExecutableMember<EraseConfigurationMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate();
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async () =>
                {
                    await oDrive.InvokeEndpoint(279);
                }

                ;
            }
        }

        public partial class RebootMethod : IExecutableMember<RebootMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate();
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async () =>
                {
                    await oDrive.InvokeEndpoint(280);
                }

                ;
            }
        }

        public partial class EnterDfuModeMethod : IExecutableMember<EnterDfuModeMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate();
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async () =>
                {
                    await oDrive.InvokeEndpoint(281);
                }

                ;
            }
        }

        public partial class JsonCrcProperty : IReadablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(1);
            }
        }

        public partial class HwVersionMajorProperty : IReadablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(2);
            }
        }

        public partial class HwVersionMinorProperty : IReadablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(3);
            }
        }

        public partial class HwVersionVariantProperty : IReadablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(4);
            }
        }

        public partial class FwVersionMajorProperty : IReadablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(5);
            }
        }

        public partial class FwVersionMinorProperty : IReadablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(6);
            }
        }

        public partial class FwVersionRevisionProperty : IReadablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(7);
            }
        }

        public partial class FwVersionUnreleasedProperty : IReadablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(8);
            }
        }

        public partial class VbusVoltageProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(9);
            }
        }

        public partial class SerialNumberProperty : IReadablePropertyMember<ulong>
        {
            public async Task<ulong> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ulong>(10);
            }
        }

        public partial class UserConfigLoadedProperty : IReadablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(11);
            }
        }

        public partial class BrakeResistorArmedProperty : IReadablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(12);
            }
        }
    }

    public partial class DeviceSchemaSystemStats : IDeviceSchema
    {
        public SystemStatsUsb Usb = new SystemStatsUsb();
        public SystemStatsI2c I2c = new SystemStatsI2c();
        public UptimeProperty Uptime = new UptimeProperty();
        public MinHeapSpaceProperty MinHeapSpace = new MinHeapSpaceProperty();
        public MinStackSpaceAxis0Property MinStackSpaceAxis0 = new MinStackSpaceAxis0Property();
        public MinStackSpaceAxis1Property MinStackSpaceAxis1 = new MinStackSpaceAxis1Property();
        public MinStackSpaceCommsProperty MinStackSpaceComms = new MinStackSpaceCommsProperty();
        public MinStackSpaceUsbProperty MinStackSpaceUsb = new MinStackSpaceUsbProperty();
        public MinStackSpaceUartProperty MinStackSpaceUart = new MinStackSpaceUartProperty();
        public MinStackSpaceUsbIrqProperty MinStackSpaceUsbIrq = new MinStackSpaceUsbIrqProperty();
        public MinStackSpaceStartupProperty MinStackSpaceStartup = new MinStackSpaceStartupProperty();
        public partial class UptimeProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(13);
            }
        }

        public partial class MinHeapSpaceProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(14);
            }
        }

        public partial class MinStackSpaceAxis0Property : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(15);
            }
        }

        public partial class MinStackSpaceAxis1Property : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(16);
            }
        }

        public partial class MinStackSpaceCommsProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(17);
            }
        }

        public partial class MinStackSpaceUsbProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(18);
            }
        }

        public partial class MinStackSpaceUartProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(19);
            }
        }

        public partial class MinStackSpaceUsbIrqProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(20);
            }
        }

        public partial class MinStackSpaceStartupProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(21);
            }
        }
    }

    public partial class DeviceSchemaConfig : IDeviceSchema
    {
        public ConfigGpio1PwmMapping Gpio1PwmMapping = new ConfigGpio1PwmMapping();
        public ConfigGpio2PwmMapping Gpio2PwmMapping = new ConfigGpio2PwmMapping();
        public ConfigGpio3PwmMapping Gpio3PwmMapping = new ConfigGpio3PwmMapping();
        public ConfigGpio4PwmMapping Gpio4PwmMapping = new ConfigGpio4PwmMapping();
        public BrakeResistanceProperty BrakeResistance = new BrakeResistanceProperty();
        public EnableUartProperty EnableUart = new EnableUartProperty();
        public EnableI2cInsteadOfCanProperty EnableI2cInsteadOfCan = new EnableI2cInsteadOfCanProperty();
        public EnableAsciiProtocolOnUsbProperty EnableAsciiProtocolOnUsb = new EnableAsciiProtocolOnUsbProperty();
        public DcBusUndervoltageTripLevelProperty DcBusUndervoltageTripLevel = new DcBusUndervoltageTripLevelProperty();
        public DcBusOvervoltageTripLevelProperty DcBusOvervoltageTripLevel = new DcBusOvervoltageTripLevelProperty();
        public partial class BrakeResistanceProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(29);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(29, newValue);
            }
        }

        public partial class EnableUartProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(30);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(30, newValue);
            }
        }

        public partial class EnableI2cInsteadOfCanProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(31);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(31, newValue);
            }
        }

        public partial class EnableAsciiProtocolOnUsbProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(32);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(32, newValue);
            }
        }

        public partial class DcBusUndervoltageTripLevelProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(33);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(33, newValue);
            }
        }

        public partial class DcBusOvervoltageTripLevelProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(34);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(34, newValue);
            }
        }
    }

    public partial class DeviceSchemaAxis0 : IDeviceSchema
    {
        public GetTempFunction GetTemp = new GetTempFunction();
        public Axis0Config Config = new Axis0Config();
        public Axis0Motor Motor = new Axis0Motor();
        public Axis0Controller Controller = new Axis0Controller();
        public Axis0Encoder Encoder = new Axis0Encoder();
        public Axis0SensorlessEstimator SensorlessEstimator = new Axis0SensorlessEstimator();
        public ErrorProperty Error = new ErrorProperty();
        public EnableStepDirProperty EnableStepDir = new EnableStepDirProperty();
        public CurrentStateProperty CurrentState = new CurrentStateProperty();
        public RequestedStateProperty RequestedState = new RequestedStateProperty();
        public LoopCounterProperty LoopCounter = new LoopCounterProperty();
        public partial class GetTempFunction : IExecutableMember<GetTempFunction.ExecutionDelegate>
        {
            public delegate Task<float> ExecutionDelegate();
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async () =>
                {
                    return await oDrive.RequestValue<float>(64);
                }

                ;
            }
        }

        public partial class ErrorProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(47);
            }

            public async Task SetProperty(IDevice oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(47, newValue);
            }
        }

        public partial class EnableStepDirProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(48);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(48, newValue);
            }
        }

        public partial class CurrentStateProperty : IReadablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(49);
            }
        }

        public partial class RequestedStateProperty : IReadablePropertyMember<byte>, IWriteablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(50);
            }

            public async Task SetProperty(IDevice oDrive, byte newValue)
            {
                await oDrive.PushValue<byte>(50, newValue);
            }
        }

        public partial class LoopCounterProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(51);
            }
        }
    }

    public partial class DeviceSchemaAxis1 : IDeviceSchema
    {
        public GetTempFunction GetTemp = new GetTempFunction();
        public Axis1Config Config = new Axis1Config();
        public Axis1Motor Motor = new Axis1Motor();
        public Axis1Controller Controller = new Axis1Controller();
        public Axis1Encoder Encoder = new Axis1Encoder();
        public Axis1SensorlessEstimator SensorlessEstimator = new Axis1SensorlessEstimator();
        public ErrorProperty Error = new ErrorProperty();
        public EnableStepDirProperty EnableStepDir = new EnableStepDirProperty();
        public CurrentStateProperty CurrentState = new CurrentStateProperty();
        public RequestedStateProperty RequestedState = new RequestedStateProperty();
        public LoopCounterProperty LoopCounter = new LoopCounterProperty();
        public partial class GetTempFunction : IExecutableMember<GetTempFunction.ExecutionDelegate>
        {
            public delegate Task<float> ExecutionDelegate();
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async () =>
                {
                    return await oDrive.RequestValue<float>(171);
                }

                ;
            }
        }

        public partial class ErrorProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(154);
            }

            public async Task SetProperty(IDevice oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(154, newValue);
            }
        }

        public partial class EnableStepDirProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(155);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(155, newValue);
            }
        }

        public partial class CurrentStateProperty : IReadablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(156);
            }
        }

        public partial class RequestedStateProperty : IReadablePropertyMember<byte>, IWriteablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(157);
            }

            public async Task SetProperty(IDevice oDrive, byte newValue)
            {
                await oDrive.PushValue<byte>(157, newValue);
            }
        }

        public partial class LoopCounterProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(158);
            }
        }
    }

    public partial class DeviceSchemaCan : IDeviceSchema
    {
        public NodeIdProperty NodeId = new NodeIdProperty();
        public TxMailboxCompleteCallbackCntProperty TxMailboxCompleteCallbackCnt = new TxMailboxCompleteCallbackCntProperty();
        public TxMailboxAbortCallbackCntProperty TxMailboxAbortCallbackCnt = new TxMailboxAbortCallbackCntProperty();
        public ReceivedMsgCntProperty ReceivedMsgCnt = new ReceivedMsgCntProperty();
        public ReceivedAckProperty ReceivedAck = new ReceivedAckProperty();
        public UnexpectedErrorsProperty UnexpectedErrors = new UnexpectedErrorsProperty();
        public UnhandledMessagesProperty UnhandledMessages = new UnhandledMessagesProperty();
        public partial class NodeIdProperty : IReadablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(261);
            }
        }

        public partial class TxMailboxCompleteCallbackCntProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(262);
            }
        }

        public partial class TxMailboxAbortCallbackCntProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(263);
            }
        }

        public partial class ReceivedMsgCntProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(264);
            }
        }

        public partial class ReceivedAckProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(265);
            }
        }

        public partial class UnexpectedErrorsProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(266);
            }
        }

        public partial class UnhandledMessagesProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(267);
            }
        }
    }

    public partial class SystemStatsUsb : IDeviceSchema
    {
        public RxCntProperty RxCnt = new RxCntProperty();
        public TxCntProperty TxCnt = new TxCntProperty();
        public TxOverrunCntProperty TxOverrunCnt = new TxOverrunCntProperty();
        public partial class RxCntProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(22);
            }
        }

        public partial class TxCntProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(23);
            }
        }

        public partial class TxOverrunCntProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(24);
            }
        }
    }

    public partial class SystemStatsI2c : IDeviceSchema
    {
        public AddrProperty Addr = new AddrProperty();
        public AddrMatchCntProperty AddrMatchCnt = new AddrMatchCntProperty();
        public RxCntProperty RxCnt = new RxCntProperty();
        public ErrorCntProperty ErrorCnt = new ErrorCntProperty();
        public partial class AddrProperty : IReadablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(25);
            }
        }

        public partial class AddrMatchCntProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(26);
            }
        }

        public partial class RxCntProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(27);
            }
        }

        public partial class ErrorCntProperty : IReadablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<uint>(28);
            }
        }
    }

    public partial class ConfigGpio1PwmMapping : IDeviceSchema
    {
        public EndpointProperty Endpoint = new EndpointProperty();
        public MinProperty Min = new MinProperty();
        public MaxProperty Max = new MaxProperty();
        public partial class EndpointProperty : IReadablePropertyMember<Common.Types.EndpointReference>, IWriteablePropertyMember<Common.Types.EndpointReference>
        {
            public async Task<Common.Types.EndpointReference> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<Common.Types.EndpointReference>(35);
            }

            public async Task SetProperty(IDevice oDrive, Common.Types.EndpointReference newValue)
            {
                await oDrive.PushValue<Common.Types.EndpointReference>(35, newValue);
            }
        }

        public partial class MinProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(36);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(36, newValue);
            }
        }

        public partial class MaxProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(37);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(37, newValue);
            }
        }
    }

    public partial class ConfigGpio2PwmMapping : IDeviceSchema
    {
        public EndpointProperty Endpoint = new EndpointProperty();
        public MinProperty Min = new MinProperty();
        public MaxProperty Max = new MaxProperty();
        public partial class EndpointProperty : IReadablePropertyMember<Common.Types.EndpointReference>, IWriteablePropertyMember<Common.Types.EndpointReference>
        {
            public async Task<Common.Types.EndpointReference> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<Common.Types.EndpointReference>(38);
            }

            public async Task SetProperty(IDevice oDrive, Common.Types.EndpointReference newValue)
            {
                await oDrive.PushValue<Common.Types.EndpointReference>(38, newValue);
            }
        }

        public partial class MinProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(39);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(39, newValue);
            }
        }

        public partial class MaxProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(40);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(40, newValue);
            }
        }
    }

    public partial class ConfigGpio3PwmMapping : IDeviceSchema
    {
        public EndpointProperty Endpoint = new EndpointProperty();
        public MinProperty Min = new MinProperty();
        public MaxProperty Max = new MaxProperty();
        public partial class EndpointProperty : IReadablePropertyMember<Common.Types.EndpointReference>, IWriteablePropertyMember<Common.Types.EndpointReference>
        {
            public async Task<Common.Types.EndpointReference> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<Common.Types.EndpointReference>(41);
            }

            public async Task SetProperty(IDevice oDrive, Common.Types.EndpointReference newValue)
            {
                await oDrive.PushValue<Common.Types.EndpointReference>(41, newValue);
            }
        }

        public partial class MinProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(42);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(42, newValue);
            }
        }

        public partial class MaxProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(43);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(43, newValue);
            }
        }
    }

    public partial class ConfigGpio4PwmMapping : IDeviceSchema
    {
        public EndpointProperty Endpoint = new EndpointProperty();
        public MinProperty Min = new MinProperty();
        public MaxProperty Max = new MaxProperty();
        public partial class EndpointProperty : IReadablePropertyMember<Common.Types.EndpointReference>, IWriteablePropertyMember<Common.Types.EndpointReference>
        {
            public async Task<Common.Types.EndpointReference> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<Common.Types.EndpointReference>(44);
            }

            public async Task SetProperty(IDevice oDrive, Common.Types.EndpointReference newValue)
            {
                await oDrive.PushValue<Common.Types.EndpointReference>(44, newValue);
            }
        }

        public partial class MinProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(45);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(45, newValue);
            }
        }

        public partial class MaxProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(46);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(46, newValue);
            }
        }
    }

    public partial class Axis0Config : IDeviceSchema
    {
        public StartupMotorCalibrationProperty StartupMotorCalibration = new StartupMotorCalibrationProperty();
        public StartupEncoderIndexSearchProperty StartupEncoderIndexSearch = new StartupEncoderIndexSearchProperty();
        public StartupEncoderOffsetCalibrationProperty StartupEncoderOffsetCalibration = new StartupEncoderOffsetCalibrationProperty();
        public StartupClosedLoopControlProperty StartupClosedLoopControl = new StartupClosedLoopControlProperty();
        public StartupSensorlessControlProperty StartupSensorlessControl = new StartupSensorlessControlProperty();
        public EnableStepDirProperty EnableStepDir = new EnableStepDirProperty();
        public CountsPerStepProperty CountsPerStep = new CountsPerStepProperty();
        public RampUpTimeProperty RampUpTime = new RampUpTimeProperty();
        public RampUpDistanceProperty RampUpDistance = new RampUpDistanceProperty();
        public SpinUpCurrentProperty SpinUpCurrent = new SpinUpCurrentProperty();
        public SpinUpAccelerationProperty SpinUpAcceleration = new SpinUpAccelerationProperty();
        public SpinUpTargetVelProperty SpinUpTargetVel = new SpinUpTargetVelProperty();
        public partial class StartupMotorCalibrationProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(52);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(52, newValue);
            }
        }

        public partial class StartupEncoderIndexSearchProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(53);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(53, newValue);
            }
        }

        public partial class StartupEncoderOffsetCalibrationProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(54);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(54, newValue);
            }
        }

        public partial class StartupClosedLoopControlProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(55);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(55, newValue);
            }
        }

        public partial class StartupSensorlessControlProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(56);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(56, newValue);
            }
        }

        public partial class EnableStepDirProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(57);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(57, newValue);
            }
        }

        public partial class CountsPerStepProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(58);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(58, newValue);
            }
        }

        public partial class RampUpTimeProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(59);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(59, newValue);
            }
        }

        public partial class RampUpDistanceProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(60);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(60, newValue);
            }
        }

        public partial class SpinUpCurrentProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(61);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(61, newValue);
            }
        }

        public partial class SpinUpAccelerationProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(62);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(62, newValue);
            }
        }

        public partial class SpinUpTargetVelProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(63);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(63, newValue);
            }
        }
    }

    public partial class Axis0Motor : IDeviceSchema
    {
        public SetCurrentControlBandwidthMethod SetCurrentControlBandwidth = new SetCurrentControlBandwidthMethod();
        public MotorCurrentControl CurrentControl = new MotorCurrentControl();
        public MotorGateDriver GateDriver = new MotorGateDriver();
        public MotorTimingLog TimingLog = new MotorTimingLog();
        public MotorConfig Config = new MotorConfig();
        public ErrorProperty Error = new ErrorProperty();
        public ArmedStateProperty ArmedState = new ArmedStateProperty();
        public IsCalibratedProperty IsCalibrated = new IsCalibratedProperty();
        public CurrentMeasPhBProperty CurrentMeasPhB = new CurrentMeasPhBProperty();
        public CurrentMeasPhCProperty CurrentMeasPhC = new CurrentMeasPhCProperty();
        public DCCalibPhBProperty DCCalibPhB = new DCCalibPhBProperty();
        public DCCalibPhCProperty DCCalibPhC = new DCCalibPhCProperty();
        public PhaseCurrentRevGainProperty PhaseCurrentRevGain = new PhaseCurrentRevGainProperty();
        public partial class SetCurrentControlBandwidthMethod : IExecutableMember<SetCurrentControlBandwidthMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate(float current_control_bandwidth);
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async (float current_control_bandwidth) =>
                {
                    await oDrive.PushValue<float>(106, current_control_bandwidth);
                    await oDrive.InvokeEndpoint(105);
                }

                ;
            }
        }

        public partial class ErrorProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(66);
            }

            public async Task SetProperty(IDevice oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(66, newValue);
            }
        }

        public partial class ArmedStateProperty : IReadablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(67);
            }
        }

        public partial class IsCalibratedProperty : IReadablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(68);
            }
        }

        public partial class CurrentMeasPhBProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(69);
            }
        }

        public partial class CurrentMeasPhCProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(70);
            }
        }

        public partial class DCCalibPhBProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(71);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(71, newValue);
            }
        }

        public partial class DCCalibPhCProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(72);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(72, newValue);
            }
        }

        public partial class PhaseCurrentRevGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(73);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(73, newValue);
            }
        }
    }

    public partial class Axis0Controller : IDeviceSchema
    {
        public SetPosSetpointMethod SetPosSetpoint = new SetPosSetpointMethod();
        public SetVelSetpointMethod SetVelSetpoint = new SetVelSetpointMethod();
        public SetCurrentSetpointMethod SetCurrentSetpoint = new SetCurrentSetpointMethod();
        public StartAnticoggingCalibrationMethod StartAnticoggingCalibration = new StartAnticoggingCalibrationMethod();
        public ControllerConfig Config = new ControllerConfig();
        public PosSetpointProperty PosSetpoint = new PosSetpointProperty();
        public VelSetpointProperty VelSetpoint = new VelSetpointProperty();
        public VelIntegratorCurrentProperty VelIntegratorCurrent = new VelIntegratorCurrentProperty();
        public CurrentSetpointProperty CurrentSetpoint = new CurrentSetpointProperty();
        public partial class SetPosSetpointMethod : IExecutableMember<SetPosSetpointMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate(float pos_setpoint, float vel_feed_forward, float current_feed_forward);
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async (float pos_setpoint, float vel_feed_forward, float current_feed_forward) =>
                {
                    await oDrive.PushValue<float>(117, pos_setpoint);
                    await oDrive.PushValue<float>(118, vel_feed_forward);
                    await oDrive.PushValue<float>(119, current_feed_forward);
                    await oDrive.InvokeEndpoint(116);
                }

                ;
            }
        }

        public partial class SetVelSetpointMethod : IExecutableMember<SetVelSetpointMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate(float vel_setpoint, float current_feed_forward);
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async (float vel_setpoint, float current_feed_forward) =>
                {
                    await oDrive.PushValue<float>(121, vel_setpoint);
                    await oDrive.PushValue<float>(122, current_feed_forward);
                    await oDrive.InvokeEndpoint(120);
                }

                ;
            }
        }

        public partial class SetCurrentSetpointMethod : IExecutableMember<SetCurrentSetpointMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate(float current_setpoint);
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async (float current_setpoint) =>
                {
                    await oDrive.PushValue<float>(124, current_setpoint);
                    await oDrive.InvokeEndpoint(123);
                }

                ;
            }
        }

        public partial class StartAnticoggingCalibrationMethod : IExecutableMember<StartAnticoggingCalibrationMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate();
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async () =>
                {
                    await oDrive.InvokeEndpoint(125);
                }

                ;
            }
        }

        public partial class PosSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(107);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(107, newValue);
            }
        }

        public partial class VelSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(108);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(108, newValue);
            }
        }

        public partial class VelIntegratorCurrentProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(109);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(109, newValue);
            }
        }

        public partial class CurrentSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(110);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(110, newValue);
            }
        }
    }

    public partial class Axis0Encoder : IDeviceSchema
    {
        public EncoderConfig Config = new EncoderConfig();
        public ErrorProperty Error = new ErrorProperty();
        public IsReadyProperty IsReady = new IsReadyProperty();
        public IndexFoundProperty IndexFound = new IndexFoundProperty();
        public ShadowCountProperty ShadowCount = new ShadowCountProperty();
        public CountInCprProperty CountInCpr = new CountInCprProperty();
        public OffsetProperty Offset = new OffsetProperty();
        public InterpolationProperty Interpolation = new InterpolationProperty();
        public PhaseProperty Phase = new PhaseProperty();
        public PosEstimateProperty PosEstimate = new PosEstimateProperty();
        public PosCprProperty PosCpr = new PosCprProperty();
        public HallStateProperty HallState = new HallStateProperty();
        public PllVelProperty PllVel = new PllVelProperty();
        public partial class ErrorProperty : IReadablePropertyMember<byte>, IWriteablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(126);
            }

            public async Task SetProperty(IDevice oDrive, byte newValue)
            {
                await oDrive.PushValue<byte>(126, newValue);
            }
        }

        public partial class IsReadyProperty : IReadablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(127);
            }
        }

        public partial class IndexFoundProperty : IReadablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(128);
            }
        }

        public partial class ShadowCountProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<int>(129);
            }

            public async Task SetProperty(IDevice oDrive, int newValue)
            {
                await oDrive.PushValue<int>(129, newValue);
            }
        }

        public partial class CountInCprProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<int>(130);
            }

            public async Task SetProperty(IDevice oDrive, int newValue)
            {
                await oDrive.PushValue<int>(130, newValue);
            }
        }

        public partial class OffsetProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<int>(131);
            }

            public async Task SetProperty(IDevice oDrive, int newValue)
            {
                await oDrive.PushValue<int>(131, newValue);
            }
        }

        public partial class InterpolationProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(132);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(132, newValue);
            }
        }

        public partial class PhaseProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(133);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(133, newValue);
            }
        }

        public partial class PosEstimateProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(134);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(134, newValue);
            }
        }

        public partial class PosCprProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(135);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(135, newValue);
            }
        }

        public partial class HallStateProperty : IReadablePropertyMember<byte>, IWriteablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(136);
            }

            public async Task SetProperty(IDevice oDrive, byte newValue)
            {
                await oDrive.PushValue<byte>(136, newValue);
            }
        }

        public partial class PllVelProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(137);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(137, newValue);
            }
        }
    }

    public partial class Axis0SensorlessEstimator : IDeviceSchema
    {
        public SensorlessEstimatorConfig Config = new SensorlessEstimatorConfig();
        public ErrorProperty Error = new ErrorProperty();
        public PhaseProperty Phase = new PhaseProperty();
        public PllPosProperty PllPos = new PllPosProperty();
        public PllVelProperty PllVel = new PllVelProperty();
        public partial class ErrorProperty : IReadablePropertyMember<byte>, IWriteablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(147);
            }

            public async Task SetProperty(IDevice oDrive, byte newValue)
            {
                await oDrive.PushValue<byte>(147, newValue);
            }
        }

        public partial class PhaseProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(148);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(148, newValue);
            }
        }

        public partial class PllPosProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(149);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(149, newValue);
            }
        }

        public partial class PllVelProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(150);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(150, newValue);
            }
        }
    }

    public partial class MotorCurrentControl : IDeviceSchema
    {
        public PGainProperty PGain = new PGainProperty();
        public IGainProperty IGain = new IGainProperty();
        public VCurrentControlIntegralDProperty VCurrentControlIntegralD = new VCurrentControlIntegralDProperty();
        public VCurrentControlIntegralQProperty VCurrentControlIntegralQ = new VCurrentControlIntegralQProperty();
        public IbusProperty Ibus = new IbusProperty();
        public FinalVAlphaProperty FinalVAlpha = new FinalVAlphaProperty();
        public FinalVBetaProperty FinalVBeta = new FinalVBetaProperty();
        public IqSetpointProperty IqSetpoint = new IqSetpointProperty();
        public IqMeasuredProperty IqMeasured = new IqMeasuredProperty();
        public MaxAllowedCurrentProperty MaxAllowedCurrent = new MaxAllowedCurrentProperty();
        public partial class PGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(74);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(74, newValue);
            }
        }

        public partial class IGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(75);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(75, newValue);
            }
        }

        public partial class VCurrentControlIntegralDProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(76);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(76, newValue);
            }
        }

        public partial class VCurrentControlIntegralQProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(77);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(77, newValue);
            }
        }

        public partial class IbusProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(78);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(78, newValue);
            }
        }

        public partial class FinalVAlphaProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(79);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(79, newValue);
            }
        }

        public partial class FinalVBetaProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(80);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(80, newValue);
            }
        }

        public partial class IqSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(81);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(81, newValue);
            }
        }

        public partial class IqMeasuredProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(82);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(82, newValue);
            }
        }

        public partial class MaxAllowedCurrentProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(83);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(83, newValue);
            }
        }
    }

    public partial class MotorGateDriver : IDeviceSchema
    {
        public DrvFaultProperty DrvFault = new DrvFaultProperty();
        public partial class DrvFaultProperty : IReadablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(84);
            }
        }
    }

    public partial class MotorTimingLog : IDeviceSchema
    {
        public TIMINGLOGGENERALProperty TIMINGLOGGENERAL = new TIMINGLOGGENERALProperty();
        public TIMINGLOGADCCBIProperty TIMINGLOGADCCBI = new TIMINGLOGADCCBIProperty();
        public TIMINGLOGADCCBDCProperty TIMINGLOGADCCBDC = new TIMINGLOGADCCBDCProperty();
        public TIMINGLOGMEASRProperty TIMINGLOGMEASR = new TIMINGLOGMEASRProperty();
        public TIMINGLOGMEASLProperty TIMINGLOGMEASL = new TIMINGLOGMEASLProperty();
        public TIMINGLOGENCCALIBProperty TIMINGLOGENCCALIB = new TIMINGLOGENCCALIBProperty();
        public TIMINGLOGIDXSEARCHProperty TIMINGLOGIDXSEARCH = new TIMINGLOGIDXSEARCHProperty();
        public TIMINGLOGFOCVOLTAGEProperty TIMINGLOGFOCVOLTAGE = new TIMINGLOGFOCVOLTAGEProperty();
        public TIMINGLOGFOCCURRENTProperty TIMINGLOGFOCCURRENT = new TIMINGLOGFOCCURRENTProperty();
        public partial class TIMINGLOGGENERALProperty : IReadablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(85);
            }
        }

        public partial class TIMINGLOGADCCBIProperty : IReadablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(86);
            }
        }

        public partial class TIMINGLOGADCCBDCProperty : IReadablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(87);
            }
        }

        public partial class TIMINGLOGMEASRProperty : IReadablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(88);
            }
        }

        public partial class TIMINGLOGMEASLProperty : IReadablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(89);
            }
        }

        public partial class TIMINGLOGENCCALIBProperty : IReadablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(90);
            }
        }

        public partial class TIMINGLOGIDXSEARCHProperty : IReadablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(91);
            }
        }

        public partial class TIMINGLOGFOCVOLTAGEProperty : IReadablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(92);
            }
        }

        public partial class TIMINGLOGFOCCURRENTProperty : IReadablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(93);
            }
        }
    }

    public partial class MotorConfig : IDeviceSchema
    {
        public PreCalibratedProperty PreCalibrated = new PreCalibratedProperty();
        public PolePairsProperty PolePairs = new PolePairsProperty();
        public CalibrationCurrentProperty CalibrationCurrent = new CalibrationCurrentProperty();
        public ResistanceCalibMaxVoltageProperty ResistanceCalibMaxVoltage = new ResistanceCalibMaxVoltageProperty();
        public PhaseInductanceProperty PhaseInductance = new PhaseInductanceProperty();
        public PhaseResistanceProperty PhaseResistance = new PhaseResistanceProperty();
        public DirectionProperty Direction = new DirectionProperty();
        public MotorTypeProperty MotorType = new MotorTypeProperty();
        public CurrentLimProperty CurrentLim = new CurrentLimProperty();
        public RequestedCurrentRangeProperty RequestedCurrentRange = new RequestedCurrentRangeProperty();
        public CurrentControlBandwidthProperty CurrentControlBandwidth = new CurrentControlBandwidthProperty();
        public partial class PreCalibratedProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(94);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(94, newValue);
            }
        }

        public partial class PolePairsProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<int>(95);
            }

            public async Task SetProperty(IDevice oDrive, int newValue)
            {
                await oDrive.PushValue<int>(95, newValue);
            }
        }

        public partial class CalibrationCurrentProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(96);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(96, newValue);
            }
        }

        public partial class ResistanceCalibMaxVoltageProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(97);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(97, newValue);
            }
        }

        public partial class PhaseInductanceProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(98);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(98, newValue);
            }
        }

        public partial class PhaseResistanceProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(99);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(99, newValue);
            }
        }

        public partial class DirectionProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<int>(100);
            }

            public async Task SetProperty(IDevice oDrive, int newValue)
            {
                await oDrive.PushValue<int>(100, newValue);
            }
        }

        public partial class MotorTypeProperty : IReadablePropertyMember<byte>, IWriteablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(101);
            }

            public async Task SetProperty(IDevice oDrive, byte newValue)
            {
                await oDrive.PushValue<byte>(101, newValue);
            }
        }

        public partial class CurrentLimProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(102);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(102, newValue);
            }
        }

        public partial class RequestedCurrentRangeProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(103);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(103, newValue);
            }
        }

        public partial class CurrentControlBandwidthProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(104);
            }
        }
    }

    public partial class ControllerConfig : IDeviceSchema
    {
        public ControlModeProperty ControlMode = new ControlModeProperty();
        public PosGainProperty PosGain = new PosGainProperty();
        public VelGainProperty VelGain = new VelGainProperty();
        public VelIntegratorGainProperty VelIntegratorGain = new VelIntegratorGainProperty();
        public VelLimitProperty VelLimit = new VelLimitProperty();
        public partial class ControlModeProperty : IReadablePropertyMember<byte>, IWriteablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(111);
            }

            public async Task SetProperty(IDevice oDrive, byte newValue)
            {
                await oDrive.PushValue<byte>(111, newValue);
            }
        }

        public partial class PosGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(112);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(112, newValue);
            }
        }

        public partial class VelGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(113);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(113, newValue);
            }
        }

        public partial class VelIntegratorGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(114);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(114, newValue);
            }
        }

        public partial class VelLimitProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(115);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(115, newValue);
            }
        }
    }

    public partial class EncoderConfig : IDeviceSchema
    {
        public ModeProperty Mode = new ModeProperty();
        public UseIndexProperty UseIndex = new UseIndexProperty();
        public PreCalibratedProperty PreCalibrated = new PreCalibratedProperty();
        public IdxSearchSpeedProperty IdxSearchSpeed = new IdxSearchSpeedProperty();
        public CprProperty Cpr = new CprProperty();
        public OffsetProperty Offset = new OffsetProperty();
        public OffsetFloatProperty OffsetFloat = new OffsetFloatProperty();
        public BandwidthProperty Bandwidth = new BandwidthProperty();
        public CalibRangeProperty CalibRange = new CalibRangeProperty();
        public partial class ModeProperty : IReadablePropertyMember<byte>, IWriteablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(138);
            }

            public async Task SetProperty(IDevice oDrive, byte newValue)
            {
                await oDrive.PushValue<byte>(138, newValue);
            }
        }

        public partial class UseIndexProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(139);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(139, newValue);
            }
        }

        public partial class PreCalibratedProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(140);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(140, newValue);
            }
        }

        public partial class IdxSearchSpeedProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(141);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(141, newValue);
            }
        }

        public partial class CprProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<int>(142);
            }

            public async Task SetProperty(IDevice oDrive, int newValue)
            {
                await oDrive.PushValue<int>(142, newValue);
            }
        }

        public partial class OffsetProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<int>(143);
            }

            public async Task SetProperty(IDevice oDrive, int newValue)
            {
                await oDrive.PushValue<int>(143, newValue);
            }
        }

        public partial class OffsetFloatProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(144);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(144, newValue);
            }
        }

        public partial class BandwidthProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(145);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(145, newValue);
            }
        }

        public partial class CalibRangeProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(146);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(146, newValue);
            }
        }
    }

    public partial class SensorlessEstimatorConfig : IDeviceSchema
    {
        public ObserverGainProperty ObserverGain = new ObserverGainProperty();
        public PllBandwidthProperty PllBandwidth = new PllBandwidthProperty();
        public PmFluxLinkageProperty PmFluxLinkage = new PmFluxLinkageProperty();
        public partial class ObserverGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(151);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(151, newValue);
            }
        }

        public partial class PllBandwidthProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(152);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(152, newValue);
            }
        }

        public partial class PmFluxLinkageProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(153);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(153, newValue);
            }
        }
    }

    public partial class Axis1Config : IDeviceSchema
    {
        public StartupMotorCalibrationProperty StartupMotorCalibration = new StartupMotorCalibrationProperty();
        public StartupEncoderIndexSearchProperty StartupEncoderIndexSearch = new StartupEncoderIndexSearchProperty();
        public StartupEncoderOffsetCalibrationProperty StartupEncoderOffsetCalibration = new StartupEncoderOffsetCalibrationProperty();
        public StartupClosedLoopControlProperty StartupClosedLoopControl = new StartupClosedLoopControlProperty();
        public StartupSensorlessControlProperty StartupSensorlessControl = new StartupSensorlessControlProperty();
        public EnableStepDirProperty EnableStepDir = new EnableStepDirProperty();
        public CountsPerStepProperty CountsPerStep = new CountsPerStepProperty();
        public RampUpTimeProperty RampUpTime = new RampUpTimeProperty();
        public RampUpDistanceProperty RampUpDistance = new RampUpDistanceProperty();
        public SpinUpCurrentProperty SpinUpCurrent = new SpinUpCurrentProperty();
        public SpinUpAccelerationProperty SpinUpAcceleration = new SpinUpAccelerationProperty();
        public SpinUpTargetVelProperty SpinUpTargetVel = new SpinUpTargetVelProperty();
        public partial class StartupMotorCalibrationProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(159);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(159, newValue);
            }
        }

        public partial class StartupEncoderIndexSearchProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(160);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(160, newValue);
            }
        }

        public partial class StartupEncoderOffsetCalibrationProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(161);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(161, newValue);
            }
        }

        public partial class StartupClosedLoopControlProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(162);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(162, newValue);
            }
        }

        public partial class StartupSensorlessControlProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(163);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(163, newValue);
            }
        }

        public partial class EnableStepDirProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(164);
            }

            public async Task SetProperty(IDevice oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(164, newValue);
            }
        }

        public partial class CountsPerStepProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(165);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(165, newValue);
            }
        }

        public partial class RampUpTimeProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(166);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(166, newValue);
            }
        }

        public partial class RampUpDistanceProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(167);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(167, newValue);
            }
        }

        public partial class SpinUpCurrentProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(168);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(168, newValue);
            }
        }

        public partial class SpinUpAccelerationProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(169);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(169, newValue);
            }
        }

        public partial class SpinUpTargetVelProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(170);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(170, newValue);
            }
        }
    }

    public partial class Axis1Motor : IDeviceSchema
    {
        public SetCurrentControlBandwidthMethod SetCurrentControlBandwidth = new SetCurrentControlBandwidthMethod();
        public MotorCurrentControl CurrentControl = new MotorCurrentControl();
        public MotorGateDriver GateDriver = new MotorGateDriver();
        public MotorTimingLog TimingLog = new MotorTimingLog();
        public MotorConfig Config = new MotorConfig();
        public ErrorProperty Error = new ErrorProperty();
        public ArmedStateProperty ArmedState = new ArmedStateProperty();
        public IsCalibratedProperty IsCalibrated = new IsCalibratedProperty();
        public CurrentMeasPhBProperty CurrentMeasPhB = new CurrentMeasPhBProperty();
        public CurrentMeasPhCProperty CurrentMeasPhC = new CurrentMeasPhCProperty();
        public DCCalibPhBProperty DCCalibPhB = new DCCalibPhBProperty();
        public DCCalibPhCProperty DCCalibPhC = new DCCalibPhCProperty();
        public PhaseCurrentRevGainProperty PhaseCurrentRevGain = new PhaseCurrentRevGainProperty();
        public partial class SetCurrentControlBandwidthMethod : IExecutableMember<SetCurrentControlBandwidthMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate(float current_control_bandwidth);
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async (float current_control_bandwidth) =>
                {
                    await oDrive.PushValue<float>(213, current_control_bandwidth);
                    await oDrive.InvokeEndpoint(212);
                }

                ;
            }
        }

        public partial class ErrorProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<ushort>(173);
            }

            public async Task SetProperty(IDevice oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(173, newValue);
            }
        }

        public partial class ArmedStateProperty : IReadablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(174);
            }
        }

        public partial class IsCalibratedProperty : IReadablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(175);
            }
        }

        public partial class CurrentMeasPhBProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(176);
            }
        }

        public partial class CurrentMeasPhCProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(177);
            }
        }

        public partial class DCCalibPhBProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(178);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(178, newValue);
            }
        }

        public partial class DCCalibPhCProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(179);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(179, newValue);
            }
        }

        public partial class PhaseCurrentRevGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(180);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(180, newValue);
            }
        }
    }

    public partial class Axis1Controller : IDeviceSchema
    {
        public SetPosSetpointMethod SetPosSetpoint = new SetPosSetpointMethod();
        public SetVelSetpointMethod SetVelSetpoint = new SetVelSetpointMethod();
        public SetCurrentSetpointMethod SetCurrentSetpoint = new SetCurrentSetpointMethod();
        public StartAnticoggingCalibrationMethod StartAnticoggingCalibration = new StartAnticoggingCalibrationMethod();
        public ControllerConfig Config = new ControllerConfig();
        public PosSetpointProperty PosSetpoint = new PosSetpointProperty();
        public VelSetpointProperty VelSetpoint = new VelSetpointProperty();
        public VelIntegratorCurrentProperty VelIntegratorCurrent = new VelIntegratorCurrentProperty();
        public CurrentSetpointProperty CurrentSetpoint = new CurrentSetpointProperty();
        public partial class SetPosSetpointMethod : IExecutableMember<SetPosSetpointMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate(float pos_setpoint, float vel_feed_forward, float current_feed_forward);
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async (float pos_setpoint, float vel_feed_forward, float current_feed_forward) =>
                {
                    await oDrive.PushValue<float>(224, pos_setpoint);
                    await oDrive.PushValue<float>(225, vel_feed_forward);
                    await oDrive.PushValue<float>(226, current_feed_forward);
                    await oDrive.InvokeEndpoint(223);
                }

                ;
            }
        }

        public partial class SetVelSetpointMethod : IExecutableMember<SetVelSetpointMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate(float vel_setpoint, float current_feed_forward);
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async (float vel_setpoint, float current_feed_forward) =>
                {
                    await oDrive.PushValue<float>(228, vel_setpoint);
                    await oDrive.PushValue<float>(229, current_feed_forward);
                    await oDrive.InvokeEndpoint(227);
                }

                ;
            }
        }

        public partial class SetCurrentSetpointMethod : IExecutableMember<SetCurrentSetpointMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate(float current_setpoint);
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async (float current_setpoint) =>
                {
                    await oDrive.PushValue<float>(231, current_setpoint);
                    await oDrive.InvokeEndpoint(230);
                }

                ;
            }
        }

        public partial class StartAnticoggingCalibrationMethod : IExecutableMember<StartAnticoggingCalibrationMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate();
            public ExecutionDelegate GetExecutor(IDevice oDrive)
            {
                return async () =>
                {
                    await oDrive.InvokeEndpoint(232);
                }

                ;
            }
        }

        public partial class PosSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(214);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(214, newValue);
            }
        }

        public partial class VelSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(215);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(215, newValue);
            }
        }

        public partial class VelIntegratorCurrentProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(216);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(216, newValue);
            }
        }

        public partial class CurrentSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(217);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(217, newValue);
            }
        }
    }

    public partial class Axis1Encoder : IDeviceSchema
    {
        public EncoderConfig Config = new EncoderConfig();
        public ErrorProperty Error = new ErrorProperty();
        public IsReadyProperty IsReady = new IsReadyProperty();
        public IndexFoundProperty IndexFound = new IndexFoundProperty();
        public ShadowCountProperty ShadowCount = new ShadowCountProperty();
        public CountInCprProperty CountInCpr = new CountInCprProperty();
        public OffsetProperty Offset = new OffsetProperty();
        public InterpolationProperty Interpolation = new InterpolationProperty();
        public PhaseProperty Phase = new PhaseProperty();
        public PosEstimateProperty PosEstimate = new PosEstimateProperty();
        public PosCprProperty PosCpr = new PosCprProperty();
        public HallStateProperty HallState = new HallStateProperty();
        public PllVelProperty PllVel = new PllVelProperty();
        public partial class ErrorProperty : IReadablePropertyMember<byte>, IWriteablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(233);
            }

            public async Task SetProperty(IDevice oDrive, byte newValue)
            {
                await oDrive.PushValue<byte>(233, newValue);
            }
        }

        public partial class IsReadyProperty : IReadablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(234);
            }
        }

        public partial class IndexFoundProperty : IReadablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<bool>(235);
            }
        }

        public partial class ShadowCountProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<int>(236);
            }

            public async Task SetProperty(IDevice oDrive, int newValue)
            {
                await oDrive.PushValue<int>(236, newValue);
            }
        }

        public partial class CountInCprProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<int>(237);
            }

            public async Task SetProperty(IDevice oDrive, int newValue)
            {
                await oDrive.PushValue<int>(237, newValue);
            }
        }

        public partial class OffsetProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<int>(238);
            }

            public async Task SetProperty(IDevice oDrive, int newValue)
            {
                await oDrive.PushValue<int>(238, newValue);
            }
        }

        public partial class InterpolationProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(239);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(239, newValue);
            }
        }

        public partial class PhaseProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(240);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(240, newValue);
            }
        }

        public partial class PosEstimateProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(241);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(241, newValue);
            }
        }

        public partial class PosCprProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(242);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(242, newValue);
            }
        }

        public partial class HallStateProperty : IReadablePropertyMember<byte>, IWriteablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(243);
            }

            public async Task SetProperty(IDevice oDrive, byte newValue)
            {
                await oDrive.PushValue<byte>(243, newValue);
            }
        }

        public partial class PllVelProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(244);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(244, newValue);
            }
        }
    }

    public partial class Axis1SensorlessEstimator : IDeviceSchema
    {
        public SensorlessEstimatorConfig Config = new SensorlessEstimatorConfig();
        public ErrorProperty Error = new ErrorProperty();
        public PhaseProperty Phase = new PhaseProperty();
        public PllPosProperty PllPos = new PllPosProperty();
        public PllVelProperty PllVel = new PllVelProperty();
        public partial class ErrorProperty : IReadablePropertyMember<byte>, IWriteablePropertyMember<byte>
        {
            public async Task<byte> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<byte>(254);
            }

            public async Task SetProperty(IDevice oDrive, byte newValue)
            {
                await oDrive.PushValue<byte>(254, newValue);
            }
        }

        public partial class PhaseProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(255);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(255, newValue);
            }
        }

        public partial class PllPosProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(256);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(256, newValue);
            }
        }

        public partial class PllVelProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(IDevice oDrive)
            {
                return await oDrive.RequestValue<float>(257);
            }

            public async Task SetProperty(IDevice oDrive, float newValue)
            {
                await oDrive.PushValue<float>(257, newValue);
            }
        }
    }
}
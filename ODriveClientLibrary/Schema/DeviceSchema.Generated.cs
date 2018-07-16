namespace ODrive.Schema
{
    using System;
    using System.Threading.Tasks;

    public partial class DeviceSchema
    {
        public static ushort SchemaChecksum = 47683;
        public RunAnticoggingCalibrationMethod RunAnticoggingCalibration = new RunAnticoggingCalibrationMethod();
        public SaveConfigurationMethod SaveConfiguration = new SaveConfigurationMethod();
        public EraseConfigurationMethod EraseConfiguration = new EraseConfigurationMethod();
        public RebootMethod Reboot = new RebootMethod();
        public EnterDfuModeMethod EnterDfuMode = new EnterDfuModeMethod();
        public DeviceSchemaConfig Config = new DeviceSchemaConfig();
        public DeviceSchemaAxis0 Axis0 = new DeviceSchemaAxis0();
        public DeviceSchemaMotor0 Motor0 = new DeviceSchemaMotor0();
        public DeviceSchemaAxis1 Axis1 = new DeviceSchemaAxis1();
        public DeviceSchemaMotor1 Motor1 = new DeviceSchemaMotor1();
        public VbusVoltageProperty VbusVoltage = new VbusVoltageProperty();
        public SerialNumberProperty SerialNumber = new SerialNumberProperty();
        public partial class RunAnticoggingCalibrationMethod : IExecutableMember<RunAnticoggingCalibrationMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate();
            public ExecutionDelegate GetExecutor(Device oDrive)
            {
                return async () =>
                {
                    await oDrive.InvokeEndpoint(3);
                }

                ;
            }
        }

        public partial class SaveConfigurationMethod : IExecutableMember<SaveConfigurationMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate();
            public ExecutionDelegate GetExecutor(Device oDrive)
            {
                return async () =>
                {
                    await oDrive.InvokeEndpoint(208);
                }

                ;
            }
        }

        public partial class EraseConfigurationMethod : IExecutableMember<EraseConfigurationMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate();
            public ExecutionDelegate GetExecutor(Device oDrive)
            {
                return async () =>
                {
                    await oDrive.InvokeEndpoint(210);
                }

                ;
            }
        }

        public partial class RebootMethod : IExecutableMember<RebootMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate();
            public ExecutionDelegate GetExecutor(Device oDrive)
            {
                return async () =>
                {
                    await oDrive.InvokeEndpoint(212);
                }

                ;
            }
        }

        public partial class EnterDfuModeMethod : IExecutableMember<EnterDfuModeMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate();
            public ExecutionDelegate GetExecutor(Device oDrive)
            {
                return async () =>
                {
                    await oDrive.InvokeEndpoint(214);
                }

                ;
            }
        }

        public partial class VbusVoltageProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(1);
            }
        }

        public partial class SerialNumberProperty : IReadablePropertyMember<ulong>
        {
            public async Task<ulong> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ulong>(2);
            }
        }
    }

    public partial class DeviceSchemaConfig
    {
        public BrakeResistanceProperty BrakeResistance = new BrakeResistanceProperty();
        public partial class BrakeResistanceProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(6);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(6, newValue);
            }
        }
    }

    public partial class DeviceSchemaAxis0
    {
        public Axis0Config Config = new Axis0Config();
    }

    public partial class DeviceSchemaMotor0
    {
        public SetPosSetpointMethod SetPosSetpoint = new SetPosSetpointMethod();
        public SetVelSetpointMethod SetVelSetpoint = new SetVelSetpointMethod();
        public SetCurrentSetpointMethod SetCurrentSetpoint = new SetCurrentSetpointMethod();
        public Motor0Config Config = new Motor0Config();
        public Motor0CurrentControl CurrentControl = new Motor0CurrentControl();
        public Motor0GateDriver GateDriver = new Motor0GateDriver();
        public Motor0Encoder Encoder = new Motor0Encoder();
        public Motor0TimingLog TimingLog = new Motor0TimingLog();
        public ErrorProperty Error = new ErrorProperty();
        public PosSetpointProperty PosSetpoint = new PosSetpointProperty();
        public VelSetpointProperty VelSetpoint = new VelSetpointProperty();
        public VelIntegratorCurrentProperty VelIntegratorCurrent = new VelIntegratorCurrentProperty();
        public CurrentSetpointProperty CurrentSetpoint = new CurrentSetpointProperty();
        public CurrentMeasPhBProperty CurrentMeasPhB = new CurrentMeasPhBProperty();
        public CurrentMeasPhCProperty CurrentMeasPhC = new CurrentMeasPhCProperty();
        public DCCalibPhBProperty DCCalibPhB = new DCCalibPhBProperty();
        public DCCalibPhCProperty DCCalibPhC = new DCCalibPhCProperty();
        public ShuntConductanceProperty ShuntConductance = new ShuntConductanceProperty();
        public PhaseCurrentRevGainProperty PhaseCurrentRevGain = new PhaseCurrentRevGainProperty();
        public ThreadReadyProperty ThreadReady = new ThreadReadyProperty();
        public ControlDeadlineProperty ControlDeadline = new ControlDeadlineProperty();
        public LastCpuTimeProperty LastCpuTime = new LastCpuTimeProperty();
        public LoopCounterProperty LoopCounter = new LoopCounterProperty();
        public partial class SetPosSetpointMethod : IExecutableMember<SetPosSetpointMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate(float pos_setpoint, float vel_feed_forward, float current_feed_forward);
            public ExecutionDelegate GetExecutor(Device oDrive)
            {
                return async (float pos_setpoint, float vel_feed_forward, float current_feed_forward) =>
                {
                    await oDrive.PushValue<float>(83, pos_setpoint);
                    await oDrive.PushValue<float>(84, vel_feed_forward);
                    await oDrive.PushValue<float>(85, current_feed_forward);
                    await oDrive.InvokeEndpoint(82);
                }

                ;
            }
        }

        public partial class SetVelSetpointMethod : IExecutableMember<SetVelSetpointMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate(float vel_setpoint, float current_feed_forward);
            public ExecutionDelegate GetExecutor(Device oDrive)
            {
                return async (float vel_setpoint, float current_feed_forward) =>
                {
                    await oDrive.PushValue<float>(88, vel_setpoint);
                    await oDrive.PushValue<float>(89, current_feed_forward);
                    await oDrive.InvokeEndpoint(87);
                }

                ;
            }
        }

        public partial class SetCurrentSetpointMethod : IExecutableMember<SetCurrentSetpointMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate(float current_setpoint);
            public ExecutionDelegate GetExecutor(Device oDrive)
            {
                return async (float current_setpoint) =>
                {
                    await oDrive.PushValue<float>(92, current_setpoint);
                    await oDrive.InvokeEndpoint(91);
                }

                ;
            }
        }

        public partial class ErrorProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(30);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(30, newValue);
            }
        }

        public partial class PosSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(31);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(31, newValue);
            }
        }

        public partial class VelSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(32);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(32, newValue);
            }
        }

        public partial class VelIntegratorCurrentProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(33);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(33, newValue);
            }
        }

        public partial class CurrentSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(34);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(34, newValue);
            }
        }

        public partial class CurrentMeasPhBProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(35);
            }
        }

        public partial class CurrentMeasPhCProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(36);
            }
        }

        public partial class DCCalibPhBProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(37);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(37, newValue);
            }
        }

        public partial class DCCalibPhCProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(38);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(38, newValue);
            }
        }

        public partial class ShuntConductanceProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(39);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(39, newValue);
            }
        }

        public partial class PhaseCurrentRevGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(40);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(40, newValue);
            }
        }

        public partial class ThreadReadyProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<bool>(41);
            }

            public async Task SetProperty(Device oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(41, newValue);
            }
        }

        public partial class ControlDeadlineProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(42);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(42, newValue);
            }
        }

        public partial class LastCpuTimeProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(43);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(43, newValue);
            }
        }

        public partial class LoopCounterProperty : IReadablePropertyMember<uint>, IWriteablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<uint>(44);
            }

            public async Task SetProperty(Device oDrive, uint newValue)
            {
                await oDrive.PushValue<uint>(44, newValue);
            }
        }
    }

    public partial class DeviceSchemaAxis1
    {
        public Axis1Config Config = new Axis1Config();
    }

    public partial class DeviceSchemaMotor1
    {
        public SetPosSetpointMethod SetPosSetpoint = new SetPosSetpointMethod();
        public SetVelSetpointMethod SetVelSetpoint = new SetVelSetpointMethod();
        public SetCurrentSetpointMethod SetCurrentSetpoint = new SetCurrentSetpointMethod();
        public Motor1Config Config = new Motor1Config();
        public Motor1CurrentControl CurrentControl = new Motor1CurrentControl();
        public Motor1GateDriver GateDriver = new Motor1GateDriver();
        public Motor1Encoder Encoder = new Motor1Encoder();
        public Motor1TimingLog TimingLog = new Motor1TimingLog();
        public ErrorProperty Error = new ErrorProperty();
        public PosSetpointProperty PosSetpoint = new PosSetpointProperty();
        public VelSetpointProperty VelSetpoint = new VelSetpointProperty();
        public VelIntegratorCurrentProperty VelIntegratorCurrent = new VelIntegratorCurrentProperty();
        public CurrentSetpointProperty CurrentSetpoint = new CurrentSetpointProperty();
        public CurrentMeasPhBProperty CurrentMeasPhB = new CurrentMeasPhBProperty();
        public CurrentMeasPhCProperty CurrentMeasPhC = new CurrentMeasPhCProperty();
        public DCCalibPhBProperty DCCalibPhB = new DCCalibPhBProperty();
        public DCCalibPhCProperty DCCalibPhC = new DCCalibPhCProperty();
        public ShuntConductanceProperty ShuntConductance = new ShuntConductanceProperty();
        public PhaseCurrentRevGainProperty PhaseCurrentRevGain = new PhaseCurrentRevGainProperty();
        public ThreadReadyProperty ThreadReady = new ThreadReadyProperty();
        public ControlDeadlineProperty ControlDeadline = new ControlDeadlineProperty();
        public LastCpuTimeProperty LastCpuTime = new LastCpuTimeProperty();
        public LoopCounterProperty LoopCounter = new LoopCounterProperty();
        public partial class SetPosSetpointMethod : IExecutableMember<SetPosSetpointMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate(float pos_setpoint, float vel_feed_forward, float current_feed_forward);
            public ExecutionDelegate GetExecutor(Device oDrive)
            {
                return async (float pos_setpoint, float vel_feed_forward, float current_feed_forward) =>
                {
                    await oDrive.PushValue<float>(183, pos_setpoint);
                    await oDrive.PushValue<float>(184, vel_feed_forward);
                    await oDrive.PushValue<float>(185, current_feed_forward);
                    await oDrive.InvokeEndpoint(182);
                }

                ;
            }
        }

        public partial class SetVelSetpointMethod : IExecutableMember<SetVelSetpointMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate(float vel_setpoint, float current_feed_forward);
            public ExecutionDelegate GetExecutor(Device oDrive)
            {
                return async (float vel_setpoint, float current_feed_forward) =>
                {
                    await oDrive.PushValue<float>(188, vel_setpoint);
                    await oDrive.PushValue<float>(189, current_feed_forward);
                    await oDrive.InvokeEndpoint(187);
                }

                ;
            }
        }

        public partial class SetCurrentSetpointMethod : IExecutableMember<SetCurrentSetpointMethod.ExecutionDelegate>
        {
            public delegate Task ExecutionDelegate(float current_setpoint);
            public ExecutionDelegate GetExecutor(Device oDrive)
            {
                return async (float current_setpoint) =>
                {
                    await oDrive.PushValue<float>(192, current_setpoint);
                    await oDrive.InvokeEndpoint(191);
                }

                ;
            }
        }

        public partial class ErrorProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(130);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(130, newValue);
            }
        }

        public partial class PosSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(131);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(131, newValue);
            }
        }

        public partial class VelSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(132);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(132, newValue);
            }
        }

        public partial class VelIntegratorCurrentProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(133);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(133, newValue);
            }
        }

        public partial class CurrentSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(134);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(134, newValue);
            }
        }

        public partial class CurrentMeasPhBProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(135);
            }
        }

        public partial class CurrentMeasPhCProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(136);
            }
        }

        public partial class DCCalibPhBProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(137);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(137, newValue);
            }
        }

        public partial class DCCalibPhCProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(138);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(138, newValue);
            }
        }

        public partial class ShuntConductanceProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(139);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(139, newValue);
            }
        }

        public partial class PhaseCurrentRevGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(140);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(140, newValue);
            }
        }

        public partial class ThreadReadyProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<bool>(141);
            }

            public async Task SetProperty(Device oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(141, newValue);
            }
        }

        public partial class ControlDeadlineProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(142);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(142, newValue);
            }
        }

        public partial class LastCpuTimeProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(143);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(143, newValue);
            }
        }

        public partial class LoopCounterProperty : IReadablePropertyMember<uint>, IWriteablePropertyMember<uint>
        {
            public async Task<uint> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<uint>(144);
            }

            public async Task SetProperty(Device oDrive, uint newValue)
            {
                await oDrive.PushValue<uint>(144, newValue);
            }
        }
    }

    public partial class Axis0Config
    {
        public EnableControlAtStartProperty EnableControlAtStart = new EnableControlAtStartProperty();
        public DoCalibrationAtStartProperty DoCalibrationAtStart = new DoCalibrationAtStartProperty();
        public partial class EnableControlAtStartProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<bool>(10);
            }

            public async Task SetProperty(Device oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(10, newValue);
            }
        }

        public partial class DoCalibrationAtStartProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<bool>(11);
            }

            public async Task SetProperty(Device oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(11, newValue);
            }
        }
    }

    public partial class Motor0Config
    {
        public ControlModeProperty ControlMode = new ControlModeProperty();
        public CountsPerStepProperty CountsPerStep = new CountsPerStepProperty();
        public PolePairsProperty PolePairs = new PolePairsProperty();
        public PosGainProperty PosGain = new PosGainProperty();
        public VelGainProperty VelGain = new VelGainProperty();
        public VelIntegratorGainProperty VelIntegratorGain = new VelIntegratorGainProperty();
        public VelLimitProperty VelLimit = new VelLimitProperty();
        public CalibrationCurrentProperty CalibrationCurrent = new CalibrationCurrentProperty();
        public ResistanceCalibMaxVoltageProperty ResistanceCalibMaxVoltage = new ResistanceCalibMaxVoltageProperty();
        public PhaseInductanceProperty PhaseInductance = new PhaseInductanceProperty();
        public PhaseResistanceProperty PhaseResistance = new PhaseResistanceProperty();
        public MotorTypeProperty MotorType = new MotorTypeProperty();
        public RotorModeProperty RotorMode = new RotorModeProperty();
        public partial class ControlModeProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(16);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(16, newValue);
            }
        }

        public partial class CountsPerStepProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(17);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(17, newValue);
            }
        }

        public partial class PolePairsProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(18);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(18, newValue);
            }
        }

        public partial class PosGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(19);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(19, newValue);
            }
        }

        public partial class VelGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(20);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(20, newValue);
            }
        }

        public partial class VelIntegratorGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(21);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(21, newValue);
            }
        }

        public partial class VelLimitProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(22);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(22, newValue);
            }
        }

        public partial class CalibrationCurrentProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(23);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(23, newValue);
            }
        }

        public partial class ResistanceCalibMaxVoltageProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(24);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(24, newValue);
            }
        }

        public partial class PhaseInductanceProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(25);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(25, newValue);
            }
        }

        public partial class PhaseResistanceProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(26);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(26, newValue);
            }
        }

        public partial class MotorTypeProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(27);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(27, newValue);
            }
        }

        public partial class RotorModeProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(28);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(28, newValue);
            }
        }
    }

    public partial class Motor0CurrentControl
    {
        public CurrentControlConfig Config = new CurrentControlConfig();
        public PGainProperty PGain = new PGainProperty();
        public IGainProperty IGain = new IGainProperty();
        public VCurrentControlIntegralDProperty VCurrentControlIntegralD = new VCurrentControlIntegralDProperty();
        public VCurrentControlIntegralQProperty VCurrentControlIntegralQ = new VCurrentControlIntegralQProperty();
        public IqSetpointProperty IqSetpoint = new IqSetpointProperty();
        public IqMeasuredProperty IqMeasured = new IqMeasuredProperty();
        public IbusProperty Ibus = new IbusProperty();
        public partial class PGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(49);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(49, newValue);
            }
        }

        public partial class IGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(50);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(50, newValue);
            }
        }

        public partial class VCurrentControlIntegralDProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(51);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(51, newValue);
            }
        }

        public partial class VCurrentControlIntegralQProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(52);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(52, newValue);
            }
        }

        public partial class IqSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(53);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(53, newValue);
            }
        }

        public partial class IqMeasuredProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(54);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(54, newValue);
            }
        }

        public partial class IbusProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(55);
            }
        }
    }

    public partial class Motor0GateDriver
    {
        public DrvFaultProperty DrvFault = new DrvFaultProperty();
        public StatusReg1Property StatusReg1 = new StatusReg1Property();
        public StatusReg2Property StatusReg2 = new StatusReg2Property();
        public CtrlReg1Property CtrlReg1 = new CtrlReg1Property();
        public CtrlReg2Property CtrlReg2 = new CtrlReg2Property();
        public partial class DrvFaultProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(58);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(58, newValue);
            }
        }

        public partial class StatusReg1Property : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(59);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(59, newValue);
            }
        }

        public partial class StatusReg2Property : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(60);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(60, newValue);
            }
        }

        public partial class CtrlReg1Property : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(61);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(61, newValue);
            }
        }

        public partial class CtrlReg2Property : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(62);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(62, newValue);
            }
        }
    }

    public partial class Motor0Encoder
    {
        public EncoderConfig Config = new EncoderConfig();
        public PhaseProperty Phase = new PhaseProperty();
        public PllPosProperty PllPos = new PllPosProperty();
        public PllVelProperty PllVel = new PllVelProperty();
        public PllKpProperty PllKp = new PllKpProperty();
        public PllKiProperty PllKi = new PllKiProperty();
        public EncoderOffsetProperty EncoderOffset = new EncoderOffsetProperty();
        public EncoderStateProperty EncoderState = new EncoderStateProperty();
        public MotorDirProperty MotorDir = new MotorDirProperty();
        public partial class PhaseProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(73);
            }
        }

        public partial class PllPosProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(74);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(74, newValue);
            }
        }

        public partial class PllVelProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(75);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(75, newValue);
            }
        }

        public partial class PllKpProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(76);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(76, newValue);
            }
        }

        public partial class PllKiProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(77);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(77, newValue);
            }
        }

        public partial class EncoderOffsetProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(78);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(78, newValue);
            }
        }

        public partial class EncoderStateProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(79);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(79, newValue);
            }
        }

        public partial class MotorDirProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(80);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(80, newValue);
            }
        }
    }

    public partial class Motor0TimingLog
    {
        public TIMINGLOGGENERALProperty TIMINGLOGGENERAL = new TIMINGLOGGENERALProperty();
        public TIMINGLOGADCCBM0IProperty TIMINGLOGADCCBM0I = new TIMINGLOGADCCBM0IProperty();
        public TIMINGLOGADCCBM0DCProperty TIMINGLOGADCCBM0DC = new TIMINGLOGADCCBM0DCProperty();
        public TIMINGLOGADCCBM1IProperty TIMINGLOGADCCBM1I = new TIMINGLOGADCCBM1IProperty();
        public TIMINGLOGADCCBM1DCProperty TIMINGLOGADCCBM1DC = new TIMINGLOGADCCBM1DCProperty();
        public TIMINGLOGMEASRProperty TIMINGLOGMEASR = new TIMINGLOGMEASRProperty();
        public TIMINGLOGMEASLProperty TIMINGLOGMEASL = new TIMINGLOGMEASLProperty();
        public TIMINGLOGENCCALIBProperty TIMINGLOGENCCALIB = new TIMINGLOGENCCALIBProperty();
        public TIMINGLOGIDXSEARCHProperty TIMINGLOGIDXSEARCH = new TIMINGLOGIDXSEARCHProperty();
        public TIMINGLOGFOCVOLTAGEProperty TIMINGLOGFOCVOLTAGE = new TIMINGLOGFOCVOLTAGEProperty();
        public TIMINGLOGFOCCURRENTProperty TIMINGLOGFOCCURRENT = new TIMINGLOGFOCCURRENTProperty();
        public partial class TIMINGLOGGENERALProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(95);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(95, newValue);
            }
        }

        public partial class TIMINGLOGADCCBM0IProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(96);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(96, newValue);
            }
        }

        public partial class TIMINGLOGADCCBM0DCProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(97);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(97, newValue);
            }
        }

        public partial class TIMINGLOGADCCBM1IProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(98);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(98, newValue);
            }
        }

        public partial class TIMINGLOGADCCBM1DCProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(99);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(99, newValue);
            }
        }

        public partial class TIMINGLOGMEASRProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(100);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(100, newValue);
            }
        }

        public partial class TIMINGLOGMEASLProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(101);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(101, newValue);
            }
        }

        public partial class TIMINGLOGENCCALIBProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(102);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(102, newValue);
            }
        }

        public partial class TIMINGLOGIDXSEARCHProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(103);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(103, newValue);
            }
        }

        public partial class TIMINGLOGFOCVOLTAGEProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(104);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(104, newValue);
            }
        }

        public partial class TIMINGLOGFOCCURRENTProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(105);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(105, newValue);
            }
        }
    }

    public partial class CurrentControlConfig
    {
        public CurrentLimProperty CurrentLim = new CurrentLimProperty();
        public partial class CurrentLimProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(47);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(47, newValue);
            }
        }
    }

    public partial class EncoderConfig
    {
        public UseIndexProperty UseIndex = new UseIndexProperty();
        public ManuallyCalibratedProperty ManuallyCalibrated = new ManuallyCalibratedProperty();
        public IdxSearchSpeedProperty IdxSearchSpeed = new IdxSearchSpeedProperty();
        public CprProperty Cpr = new CprProperty();
        public OffsetProperty Offset = new OffsetProperty();
        public MotorDirProperty MotorDir = new MotorDirProperty();
        public partial class UseIndexProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<bool>(66);
            }

            public async Task SetProperty(Device oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(66, newValue);
            }
        }

        public partial class ManuallyCalibratedProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<bool>(67);
            }

            public async Task SetProperty(Device oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(67, newValue);
            }
        }

        public partial class IdxSearchSpeedProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(68);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(68, newValue);
            }
        }

        public partial class CprProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(69);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(69, newValue);
            }
        }

        public partial class OffsetProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(70);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(70, newValue);
            }
        }

        public partial class MotorDirProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(71);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(71, newValue);
            }
        }
    }

    public partial class Axis1Config
    {
        public EnableControlAtStartProperty EnableControlAtStart = new EnableControlAtStartProperty();
        public DoCalibrationAtStartProperty DoCalibrationAtStart = new DoCalibrationAtStartProperty();
        public partial class EnableControlAtStartProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<bool>(110);
            }

            public async Task SetProperty(Device oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(110, newValue);
            }
        }

        public partial class DoCalibrationAtStartProperty : IReadablePropertyMember<bool>, IWriteablePropertyMember<bool>
        {
            public async Task<bool> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<bool>(111);
            }

            public async Task SetProperty(Device oDrive, bool newValue)
            {
                await oDrive.PushValue<bool>(111, newValue);
            }
        }
    }

    public partial class Motor1Config
    {
        public ControlModeProperty ControlMode = new ControlModeProperty();
        public CountsPerStepProperty CountsPerStep = new CountsPerStepProperty();
        public PolePairsProperty PolePairs = new PolePairsProperty();
        public PosGainProperty PosGain = new PosGainProperty();
        public VelGainProperty VelGain = new VelGainProperty();
        public VelIntegratorGainProperty VelIntegratorGain = new VelIntegratorGainProperty();
        public VelLimitProperty VelLimit = new VelLimitProperty();
        public CalibrationCurrentProperty CalibrationCurrent = new CalibrationCurrentProperty();
        public ResistanceCalibMaxVoltageProperty ResistanceCalibMaxVoltage = new ResistanceCalibMaxVoltageProperty();
        public PhaseInductanceProperty PhaseInductance = new PhaseInductanceProperty();
        public PhaseResistanceProperty PhaseResistance = new PhaseResistanceProperty();
        public MotorTypeProperty MotorType = new MotorTypeProperty();
        public RotorModeProperty RotorMode = new RotorModeProperty();
        public partial class ControlModeProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(116);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(116, newValue);
            }
        }

        public partial class CountsPerStepProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(117);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(117, newValue);
            }
        }

        public partial class PolePairsProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(118);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(118, newValue);
            }
        }

        public partial class PosGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(119);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(119, newValue);
            }
        }

        public partial class VelGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(120);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(120, newValue);
            }
        }

        public partial class VelIntegratorGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(121);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(121, newValue);
            }
        }

        public partial class VelLimitProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(122);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(122, newValue);
            }
        }

        public partial class CalibrationCurrentProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(123);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(123, newValue);
            }
        }

        public partial class ResistanceCalibMaxVoltageProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(124);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(124, newValue);
            }
        }

        public partial class PhaseInductanceProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(125);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(125, newValue);
            }
        }

        public partial class PhaseResistanceProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(126);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(126, newValue);
            }
        }

        public partial class MotorTypeProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(127);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(127, newValue);
            }
        }

        public partial class RotorModeProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(128);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(128, newValue);
            }
        }
    }

    public partial class Motor1CurrentControl
    {
        public CurrentControlConfig Config = new CurrentControlConfig();
        public PGainProperty PGain = new PGainProperty();
        public IGainProperty IGain = new IGainProperty();
        public VCurrentControlIntegralDProperty VCurrentControlIntegralD = new VCurrentControlIntegralDProperty();
        public VCurrentControlIntegralQProperty VCurrentControlIntegralQ = new VCurrentControlIntegralQProperty();
        public IqSetpointProperty IqSetpoint = new IqSetpointProperty();
        public IqMeasuredProperty IqMeasured = new IqMeasuredProperty();
        public IbusProperty Ibus = new IbusProperty();
        public partial class PGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(149);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(149, newValue);
            }
        }

        public partial class IGainProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(150);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(150, newValue);
            }
        }

        public partial class VCurrentControlIntegralDProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(151);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(151, newValue);
            }
        }

        public partial class VCurrentControlIntegralQProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(152);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(152, newValue);
            }
        }

        public partial class IqSetpointProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(153);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(153, newValue);
            }
        }

        public partial class IqMeasuredProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(154);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(154, newValue);
            }
        }

        public partial class IbusProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(155);
            }
        }
    }

    public partial class Motor1GateDriver
    {
        public DrvFaultProperty DrvFault = new DrvFaultProperty();
        public StatusReg1Property StatusReg1 = new StatusReg1Property();
        public StatusReg2Property StatusReg2 = new StatusReg2Property();
        public CtrlReg1Property CtrlReg1 = new CtrlReg1Property();
        public CtrlReg2Property CtrlReg2 = new CtrlReg2Property();
        public partial class DrvFaultProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(158);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(158, newValue);
            }
        }

        public partial class StatusReg1Property : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(159);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(159, newValue);
            }
        }

        public partial class StatusReg2Property : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(160);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(160, newValue);
            }
        }

        public partial class CtrlReg1Property : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(161);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(161, newValue);
            }
        }

        public partial class CtrlReg2Property : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(162);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(162, newValue);
            }
        }
    }

    public partial class Motor1Encoder
    {
        public EncoderConfig Config = new EncoderConfig();
        public PhaseProperty Phase = new PhaseProperty();
        public PllPosProperty PllPos = new PllPosProperty();
        public PllVelProperty PllVel = new PllVelProperty();
        public PllKpProperty PllKp = new PllKpProperty();
        public PllKiProperty PllKi = new PllKiProperty();
        public EncoderOffsetProperty EncoderOffset = new EncoderOffsetProperty();
        public EncoderStateProperty EncoderState = new EncoderStateProperty();
        public MotorDirProperty MotorDir = new MotorDirProperty();
        public partial class PhaseProperty : IReadablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(173);
            }
        }

        public partial class PllPosProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(174);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(174, newValue);
            }
        }

        public partial class PllVelProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(175);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(175, newValue);
            }
        }

        public partial class PllKpProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(176);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(176, newValue);
            }
        }

        public partial class PllKiProperty : IReadablePropertyMember<float>, IWriteablePropertyMember<float>
        {
            public async Task<float> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<float>(177);
            }

            public async Task SetProperty(Device oDrive, float newValue)
            {
                await oDrive.PushValue<float>(177, newValue);
            }
        }

        public partial class EncoderOffsetProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(178);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(178, newValue);
            }
        }

        public partial class EncoderStateProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(179);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(179, newValue);
            }
        }

        public partial class MotorDirProperty : IReadablePropertyMember<int>, IWriteablePropertyMember<int>
        {
            public async Task<int> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<int>(180);
            }

            public async Task SetProperty(Device oDrive, int newValue)
            {
                await oDrive.PushValue<int>(180, newValue);
            }
        }
    }

    public partial class Motor1TimingLog
    {
        public TIMINGLOGGENERALProperty TIMINGLOGGENERAL = new TIMINGLOGGENERALProperty();
        public TIMINGLOGADCCBM0IProperty TIMINGLOGADCCBM0I = new TIMINGLOGADCCBM0IProperty();
        public TIMINGLOGADCCBM0DCProperty TIMINGLOGADCCBM0DC = new TIMINGLOGADCCBM0DCProperty();
        public TIMINGLOGADCCBM1IProperty TIMINGLOGADCCBM1I = new TIMINGLOGADCCBM1IProperty();
        public TIMINGLOGADCCBM1DCProperty TIMINGLOGADCCBM1DC = new TIMINGLOGADCCBM1DCProperty();
        public TIMINGLOGMEASRProperty TIMINGLOGMEASR = new TIMINGLOGMEASRProperty();
        public TIMINGLOGMEASLProperty TIMINGLOGMEASL = new TIMINGLOGMEASLProperty();
        public TIMINGLOGENCCALIBProperty TIMINGLOGENCCALIB = new TIMINGLOGENCCALIBProperty();
        public TIMINGLOGIDXSEARCHProperty TIMINGLOGIDXSEARCH = new TIMINGLOGIDXSEARCHProperty();
        public TIMINGLOGFOCVOLTAGEProperty TIMINGLOGFOCVOLTAGE = new TIMINGLOGFOCVOLTAGEProperty();
        public TIMINGLOGFOCCURRENTProperty TIMINGLOGFOCCURRENT = new TIMINGLOGFOCCURRENTProperty();
        public partial class TIMINGLOGGENERALProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(195);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(195, newValue);
            }
        }

        public partial class TIMINGLOGADCCBM0IProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(196);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(196, newValue);
            }
        }

        public partial class TIMINGLOGADCCBM0DCProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(197);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(197, newValue);
            }
        }

        public partial class TIMINGLOGADCCBM1IProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(198);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(198, newValue);
            }
        }

        public partial class TIMINGLOGADCCBM1DCProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(199);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(199, newValue);
            }
        }

        public partial class TIMINGLOGMEASRProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(200);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(200, newValue);
            }
        }

        public partial class TIMINGLOGMEASLProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(201);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(201, newValue);
            }
        }

        public partial class TIMINGLOGENCCALIBProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(202);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(202, newValue);
            }
        }

        public partial class TIMINGLOGIDXSEARCHProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(203);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(203, newValue);
            }
        }

        public partial class TIMINGLOGFOCVOLTAGEProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(204);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(204, newValue);
            }
        }

        public partial class TIMINGLOGFOCCURRENTProperty : IReadablePropertyMember<ushort>, IWriteablePropertyMember<ushort>
        {
            public async Task<ushort> GetProperty(Device oDrive)
            {
                return await oDrive.RequestValue<ushort>(205);
            }

            public async Task SetProperty(Device oDrive, ushort newValue)
            {
                await oDrive.PushValue<ushort>(205, newValue);
            }
        }
    }
}
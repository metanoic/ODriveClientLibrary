namespace ODrive.Enums
{
    using System;

    [Flags]
    public enum AxisErrorCode
    {
        ERROR_NONE = 0x00,
        ERROR_INVALID_STATE = 0x01,
        ERROR_DC_BUS_UNDER_VOLTAGE = 0x02,
        ERROR_DC_BUS_OVER_VOLTAGE = 0x04,
        ERROR_CURRENT_MEASUREMENT_TIMEOUT = 0x08,
        ERROR_BRAKE_RESISTOR_DISARMED = 0x10,
        ERROR_MOTOR_DISARMED = 0x20,
        ERROR_MOTOR_FAILED = 0x40,
        ERROR_SENSORLESS_ESTIMATOR_FAILED = 0x80,
        ERROR_ENCODER_FAILED = 0x100,
        ERROR_CONTROLLER_FAILED = 0x200,
        ERROR_POS_CTRL_DURING_SENS
    }
}
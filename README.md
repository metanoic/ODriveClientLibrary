# ODriveClientLibrary
This project is a fully featured C# library for communicating with the [ODriveRobotics](https://odriverobotics.com/) motor controller board via USB.  It is currently Windows only, but will be cross-platform (.NetStandard library) as dependent repos accept and merge my pull requests.

## Getting Started
Clone the repo and open in Visual Studio.

Navigate to `GenerateDevice.tt` in the root of the `ODriveClientLibrary` project and edit the `string deviceSerialNumber = "..."` line to include the serial number of your ODrive board.  Upon saving the file, the board will be connected to and queried for its schema information. Several files will be generated; these generated files will appear as children to the `GenerateDevice.tt` file, and will end in `.Generated.cs`.

After the files have been generated, build the project.  The `Demo.ReadSetProperties` project _should_ be set as the startup project, and F5'ing should start the application.

## API
### DeviceMonitor
The `DeviceMonitor` class is a singleton whose instance can be accessed via its `Instance` property.  The purpose of this class is to provide the ability to detect when your ODrive board has been connected or disconnected from the computer.

The class monitors all USB notifications and pays attention specifically to any USB device that matches the conditions defined by `DeviceMonitor.Instance..DeviceAvailabilityPredicate`.  Any connected device matching that predicate will be added to the `DeviceMonitor.Instance.AvailableDevices` list, and will be removed from that list if it is disconnected.

The `DeviceAvailabilityPredicate` can be built easily using `Utilities.PredicateBuilder`.  For example:
```csharp
DeviceAvailabilityPredicate = Utilities.PredicateBuilder.True<BasicDeviceInfo>() // Initially we match everything
                .And(deviceInfo => deviceInfo.SerialNumber = "123")
                .And(deviceInfo => deviceInfo.IsConnected);
```

The type returned by `DeviceMonitor.Instance.AvailableDevices` is `BasicDeviceInfo`, which is used to create an instance of `Device` which will allow you to interact with the corresponding board.

### Device
The `Device` class represents your ODrive board and is generated from your board's (or a same version's) schema.  The code generation process was described previously in the __Getting Started__ section.

```csharp
using ODrive.Utilities;

DeviceMonitor.Instance.DeviceAvailabilityPredicate = ODrive.Utilities.PredicateBuilder.True<BasicDeviceInfo>()
    .And(deviceInfo => deviceInfo.IsConnected);

var oDrive = new Device(DeviceMonitor.Instance.AvailableDevices.First());

Console.WriteLine($"Bus Voltage: {oDrive.VbusVoltage}V");
```

Upon `get` or `set` of properties on the `Device` object, a request is made to the ODrive board and the library blocks until the board replies.  These requests are made through `Device.FetchEndpointSync<T>`.  The parameters provided to `FetchEndpointSync` are contained in the `*.Generated.cs` files.

Asynchronous set or get of ODrive properties can be done via `FetchEndpoint<T>`.

## Agenda / TODO
Cross-platform

Retry failed requests

Request timeouts

Disconnect/Reconnect

Reconnect on unintended disconnect

Thorough disposal of objects / lifecycle management

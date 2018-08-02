namespace ODriveClientLibrary
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using LibUsbDotNet;
    using LibUsbDotNet.DeviceNotify;
    using ODriveClientLibrary.Utilities;

    /// <summary>
    /// This singleton class provides a mechanism for being notified when a device the consumer is interested 
    /// in is plugged in to the computer and available to be communicated with.<para/>  By default, its <see cref="AvailableDevices"/>
    /// will be populated with one or more ODrive boards.<para/> The consumer can further refine what devices are
    /// put in the <see cref="AvailableDevices"/> list by composing a replacement predicate for <see cref="DeviceAvailabilityPredicate"/>.
    /// </summary>
    /// <seealso cref="ReactiveUI.ReactiveObject" />
    public sealed class DeviceMonitor : PropertyNotifierBase, IDisposable
    {
        private static readonly CompositeDisposable compositeDisposable = new CompositeDisposable();
        private static readonly Lazy<DeviceMonitor> LazyInstantiator = new Lazy<DeviceMonitor>(() => new DeviceMonitor(), isThreadSafe: true);

        /// <summary>
        /// Gets the instance of the DeviceMonitor singleton
        /// </summary>
        /// <value>The DeviceMonitor instance.</value>
        public static DeviceMonitor Instance { get => LazyInstantiator.Value; }

        private static IDeviceNotifier usbDeviceNotifier;

        private ObservableCollection<BasicDeviceInfo> allDevices = new ObservableCollection<BasicDeviceInfo>();

        /// <summary>
        /// This property is a real-time updating list of <see cref="BasicDeviceInfo"/> objects representing all devices the <see cref="DeviceMonitor"/>
        /// has encountered.
        /// </summary>
        /// <value>
        /// Provides a real-time list of all <see cref="BasicDeviceInfo"/> instances the <see cref="DeviceMonitor"/> has encountered, whether currently <see cref="BasicDeviceInfo.IsConnected"/> or not.
        /// </value>
        /// <remarks>This list is the source of the <see cref="AvailableDevices"/> collection, but <see cref="DeviceAvailabilityPredicate"/> has not been applied.</remarks>
        public ObservableCollection<BasicDeviceInfo> AllDevices
        {
            get => allDevices;
            private set => RaiseAndSetIfChanged(ref allDevices, value);
        }

        private ObservableCollection<BasicDeviceInfo> availableDevices;

        /// <summary>
        /// This property represents the projection of <see cref="AllDevices"/> after applying <see cref="DeviceAvailabilityPredicate"/>.<para/>
        /// In other words, it is a list of devices that the consumer has expressed a desire to communicate with.
        /// </summary>
        /// <value>A list of devices the consumer wants to communicate with.</value>
        public ObservableCollection<BasicDeviceInfo> AvailableDevices
        {
            get => availableDevices;
            private set => RaiseAndSetIfChanged(ref availableDevices, value);
        }

        private Expression<Func<BasicDeviceInfo, bool>> deviceAvailabilityPredicate;

        /// <summary>
        /// Gets or sets a predicate whose application to <see cref="AllDevices"/> determines which devices are allowed to appear in <see cref="AvailableDevices"/>.<para/>
        /// Return <c>true</c> to indicate that the <see cref="BasicDeviceInfo"/> should be included in <see cref="AvailableDevices"/>.
        /// </summary>
        /// <value>The predicate to be applied to <see cref="AllDevices"/>.</value>
        /// <remarks>Returning <c>true</c> indicates the device should appear in <see cref="AvailableDevices"/>.</remarks>
        public Expression<Func<BasicDeviceInfo, bool>> DeviceAvailabilityPredicate
        {
            get => deviceAvailabilityPredicate;
            set => RaiseAndSetIfChanged(ref deviceAvailabilityPredicate, value);
        }

        private Func<BasicDeviceInfo, bool> _compiledDeviceAvailabilityPredicate;
        private Func<BasicDeviceInfo, bool> compiledDeviceAvailabilityPredicate
        {
            get => _compiledDeviceAvailabilityPredicate;
            set => RaiseAndSetIfChanged(ref _compiledDeviceAvailabilityPredicate, value);
        }

        private IObservable<DeviceNotifyEventArgs> deviceInterfaceNotifications = Observable.FromEventPattern<EventHandler<DeviceNotifyEventArgs>, DeviceNotifyEventArgs>(
                h => usbDeviceNotifier.OnDeviceNotify += h,
                h => usbDeviceNotifier.OnDeviceNotify -= h
            ).Select(evt => evt.EventArgs).Where(evt => evt.DeviceType == DeviceType.DeviceInterface);

        private bool DeviceHasODriveVendorAndProduct(BasicDeviceInfo deviceInfo)
        {
            var matchesAnyVendorProductPair = Config.KnownVendorProductPairs.Any(pair =>
            {
                return pair.VendorID == deviceInfo.VendorID && pair.ProductID == deviceInfo.ProductID;
            });
            return matchesAnyVendorProductPair;
        }

        private DeviceMonitor()
        {
            usbDeviceNotifier = DeviceNotifier.OpenDeviceNotifier();

            AllDevices = new ObservableCollection<BasicDeviceInfo>();
            AvailableDevices = new ObservableCollection<BasicDeviceInfo>();

            compositeDisposable.Add(this.OnPropertyChange(x => x.DeviceAvailabilityPredicate).Subscribe(expression =>
               {
                   compiledDeviceAvailabilityPredicate = expression.Compile();
               }
            ));

            DeviceAvailabilityPredicate = PredicateBuilder.True<BasicDeviceInfo>()
                .And(deviceInfo => DeviceHasODriveVendorAndProduct(deviceInfo))
                .And(deviceInfo => deviceInfo.IsConnected);


            // Populate the already-connected devices
            var connectedDevices = UsbDevice.AllDevices.Select(usbRegistry =>
            {
                var newInfo = BasicDeviceInfo.CreateFrom(usbRegistry.Device);
                if (newInfo != null)
                {
                    newInfo.IsConnected = true;
                }

                return newInfo;
            }).Where(deviceInfo => deviceInfo != null).ToList();

            compositeDisposable.Add(Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                handler => handler.Invoke,
                h => AllDevices.CollectionChanged += h,
                h => AllDevices.CollectionChanged -= h)
            .Select(evtPattern => evtPattern.EventArgs)
            .Subscribe(evt =>
            {
                if (evt.NewItems != null)
                {
                    foreach (BasicDeviceInfo deviceInfo in evt.NewItems)
                    {
                        deviceInfo.PropertyChanged += MaintainAvailabileDevices;
                        MaintainAvailabileDevices(deviceInfo, new PropertyChangedEventArgs(nameof(deviceInfo.PropertyChanged)));
                    }
                }

                if (evt.OldItems != null)
                {
                    foreach (BasicDeviceInfo deviceInfo in evt.OldItems)
                    {
                        deviceInfo.PropertyChanged -= MaintainAvailabileDevices;
                        MaintainAvailabileDevices(deviceInfo, new PropertyChangedEventArgs(nameof(deviceInfo.PropertyChanged)));
                    }
                }
            }));

            connectedDevices.ForEach(AllDevices.Add);

            // Actively maintain a list of BasicDeviceInfo's, updating IsConnected as the device is connected or disconnected
            deviceInterfaceNotifications.Subscribe(evt =>
            {
                var existingDeviceInfo = AllDevices.SingleOrDefault(deviceInfo =>
                {
                    return deviceInfo.VendorID == evt.Device.IdVendor &&
                            deviceInfo.ProductID == evt.Device.IdProduct &&
                            deviceInfo.SerialNumber == evt.Device.SerialNumber;
                });

                switch (evt.EventType)
                {
                    case EventType.DeviceArrival:
                        // We've seen this device before and just need to mark it as connected again
                        if (existingDeviceInfo != null)
                        {
                            existingDeviceInfo.IsConnected = true;
                        }
                        else
                        {
                            // existingDeviceInfo being null means we've never seen this device; we need to gather 
                            // its information and add it to our list
                            UsbDevice newDevice = null;
                            var foundRegistry = UsbDevice.AllDevices.Find(usbRegistry =>
                            {
                                // The events only expose so much information about the device.  This combination of
                                // properties should suffice for identifying individual ODrive boards.
                                var doesMatch = usbRegistry.Vid == evt.Device.IdVendor &&
                                    usbRegistry.Pid == evt.Device.IdProduct &&
                                    usbRegistry.Device?.Info?.SerialString == evt.Device.SerialNumber;
                                return doesMatch;
                            });

                            if (foundRegistry != null)
                            {
                                foundRegistry.Open(out newDevice);
                            }

                            if (newDevice != null)
                            {
                                var newDeviceInfo = BasicDeviceInfo.CreateFrom(newDevice);

                                if (newDevice.IsOpen)
                                {
                                    newDevice.Close();
                                }

                                if (newDeviceInfo != null)
                                {
                                    newDeviceInfo.IsConnected = true;
                                    AllDevices.Add(newDeviceInfo);
                                }
                            }
                        }

                        break;
                    case EventType.DeviceRemoveComplete:
                        // Just mark it as not connected, it will be filtered out of the final result
                        if (existingDeviceInfo != null)
                        {
                            existingDeviceInfo.IsConnected = false;
                        }

                        break;
                    default:
                        break;
                }
            });
        }

        private void MaintainAvailabileDevices(object sender, PropertyChangedEventArgs e)
        {
            var deviceInfo = sender as BasicDeviceInfo;
            if (compiledDeviceAvailabilityPredicate.Invoke(deviceInfo))
            {
                AvailableDevices.Add(deviceInfo);
            }
            else
            {
                AvailableDevices.Remove(deviceInfo);
            }
        }

        private bool isDisposed = false; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (!isDisposed && compositeDisposable.IsDisposed == false)
            {
                if (disposing)
                {
                    compositeDisposable.Dispose();
                }

                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}

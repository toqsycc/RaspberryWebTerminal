using System;
using SharpDX.DirectInput;

namespace RaspberryWebTerminal.Models
{
    public class InputDevice
    {
        private readonly Joystick _device;
        public string What { get; }
        public bool IsSucceeded { get; }
        
        public InputDevice()
        {
            DirectInput directInput = new DirectInput();
            Guid deviceGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(
                DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
            {
                deviceGuid = deviceInstance.InstanceGuid;
            }

            if (deviceGuid == Guid.Empty)
            {
                IsSucceeded = false;
                What = $"Unable to find any compatible input device.";
                throw new ApplicationException(What);
            }

            What = $"Query data from device with GUID: {deviceGuid}";
            _device = new Joystick(directInput, deviceGuid);
            _device.Properties.BufferSize = 128;
            _device.Acquire();
            IsSucceeded = true;
        }

        public JoystickUpdate[] PollEvents()
        {
            _device.Poll();
            return _device.GetBufferedData();
        }
    }
}
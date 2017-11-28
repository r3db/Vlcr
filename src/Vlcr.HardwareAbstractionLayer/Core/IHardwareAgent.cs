using System;
using Vlcr.Core;
using Vlcr.HardwareAbstractionLayer.Events;
using Vlcr.HardwareAbstractionLayer.History;

namespace Vlcr.HardwareAbstractionLayer.Core
{
    // Done!
    public interface IHardwareAgent
    {
        HardwareActionStatus Rotate(float radians, float speed);
        HardwareActionStatus Move(Vector heading, float speed);
        HardwareStatus Status { get; }
        RawImage LeftCameraImage { get; }
        RawImage RightCameraImage { get; }
        HardwareHistory History { get; }
        Vector Position { get; }
        event EventHandler<BatteryEventArgs> LowBatteryWarning;
        event EventHandler<BatteryEventArgs> DeadBatteryWarning;
        event EventHandler ConnectionLost;
        event EventHandler ConnectionRegain;
    }
}
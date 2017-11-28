using System;

namespace Vlcr.HardwareAbstractionLayer.Events
{
    public class BatteryEventArgs : EventArgs
    {
        // Done!
        #region Automatic Properties

        public float Charge { get; private set; }

        #endregion

        // Done!
        #region Ctor

        // Done!
        public BatteryEventArgs(float charge)
        {
            this.Charge = charge;
        }

        #endregion
    }
}
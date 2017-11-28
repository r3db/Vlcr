using System;

namespace Vlcr.HardwareAbstractionLayer.Core
{
    // Done!
    [Serializable]
    public sealed class HardwareStatus
    {
        // Done!
        #region Automatic Properties

        public float Batery         { get; private set; }
        public bool  IsCameraStereo { get; set; }
       
        #endregion

        // Done!
        #region .Ctor

        public HardwareStatus(float batery)
        {
            this.Batery = batery;
        }

        #endregion
    }
}
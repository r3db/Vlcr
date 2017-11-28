using System;
using Vlcr.Core;
using Vlcr.HardwareAbstractionLayer.Core;

namespace Vlcr.HardwareAbstractionLayer.History
{
    // Done!
    public sealed class HardwareHistoryItem
    {
        // Done!
        #region Automatic Properties

        public HardwareHistoryType      ActionType      { get; private set; }
        public HardwareActionStatus     ActionStatus    { get; private set; }
        public HardwareStatus           Status          { get; private set; }
        public float                    Degrees         { get; private set; }
        public float                    Speed           { get; private set; }
        public Vector                   Heading         { get; private set; }

        #endregion

        // Done!
        #region .Ctor

        // Done!
        internal HardwareHistoryItem(HardwareHistoryType actionType, HardwareActionStatus actionStatus, HardwareStatus status, float degrees, float speed, Vector heading)
        {
            this.ActionType = actionType;
            this.ActionStatus = actionStatus;
            this.Status = status;
            this.Degrees = degrees;
            this.Speed = speed;
            this.Heading = heading;
        }

        #endregion
    }
}
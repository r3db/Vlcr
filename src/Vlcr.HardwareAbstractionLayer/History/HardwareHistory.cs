using System;
using System.Collections.Generic;
using Vlcr.Core;
using Vlcr.HardwareAbstractionLayer.Core;

namespace Vlcr.HardwareAbstractionLayer.History
{
    // Done!
    public sealed class HardwareHistory : List<HardwareHistoryItem>
    {
        // Done!
        #region Methods

        // Done!
        public void AddRotation(IHardwareAgent agent, HardwareActionStatus actionStatus, float angle, float speed)
        {
            this.Add(new HardwareHistoryItem(HardwareHistoryType.Rotate, actionStatus, Helpers.Clone(agent.Status), angle, speed, null));
        }

        // Done!
        public void AddMove(IHardwareAgent agent, HardwareActionStatus actionStatus, Vector heading, float speed)
        {
            this.Add(new HardwareHistoryItem(HardwareHistoryType.Rotate, actionStatus, Helpers.Clone(agent.Status), 0, speed, heading));
        }

        #endregion
    }
}
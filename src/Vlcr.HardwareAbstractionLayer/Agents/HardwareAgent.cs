using System;
using Vlcr.HardwareAbstractionLayer.History;

namespace Vlcr.HardwareAbstractionLayer.Agents
{
    // Done!
    public abstract class HardwareAgent
    {
        // Done!
        #region Automatic Properties

        public HardwareHistory History { get; private set; }

        #endregion

        // Done!
        #region .Ctor

        protected HardwareAgent()
        {
            this.History = new HardwareHistory();
        }

        #endregion
    }
}
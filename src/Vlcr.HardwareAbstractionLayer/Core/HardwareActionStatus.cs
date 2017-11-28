using System;
using Vlcr.Core;

namespace Vlcr.HardwareAbstractionLayer.Core
{
    // Done!
    public sealed class HardwareActionStatus
    {
        // Done!
        #region Automatic Properties

        public TimeSpan  TimeSpan { get; private set; }
        public FuzzyBool Complete { get; private set; }
        public string    Message  { get; private set; }

        #endregion

        // Done!
        #region .Ctor

        // Done!
        public HardwareActionStatus(TimeSpan span, FuzzyBool complete, string message = "")
        {
            this.TimeSpan = span;
            this.Complete = complete;
            this.Message = message;
        }

        #endregion

        // Done!
        public static bool CompareOnPrediction(HardwareActionStatus prediction, HardwareActionStatus real)
        {
            if(prediction == null || real == null)
            {
                return false;
            }
            return (prediction.TimeSpan - real.TimeSpan).Milliseconds <= 100 && System.Math.Abs(prediction.Complete.Value - real.Complete.Value) <= 0.5f;
        }
    }
}
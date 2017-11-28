using System;
using System.Diagnostics.Contracts;

namespace Vlcr.Math
{
    [Serializable]
    public sealed class Regression
    {
        // Done!
        #region Internal Instance Data

        private readonly DateTime dateTime = DateTime.Now;
        private readonly Func<float, float> activator;
        private readonly Func<TimeSpan, float> decay;
        
        #endregion

        // Done!
        #region .Ctor

        // Done!
        public Regression(Func<float, float> activator, Func<TimeSpan, float> decay = null)
        {
            Contract.Requires(activator != null);
            this.activator = activator;
            this.decay = decay;
        }

        #endregion

        // Done!
        #region Methods

        // Done!
        public float GetValue(float value, DateTime time)
        {
            Contract.Requires(decay != null);
            return this.activator(value)*decay(dateTime - time);
        }

        // Done!
        public float GetValue(float value)
        {
            return this.activator(value);
        }
        
        #endregion

    }
}
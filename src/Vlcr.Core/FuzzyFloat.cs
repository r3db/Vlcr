using System;

namespace Vlcr.Core
{
    [Serializable]
    public class FuzzyFloat
    {
        // Done!
        #region Internal Instance Data

        private float passes;
        private DateTime dateTime;
        private float value;

        private readonly Func<float, float> activator;
        private readonly Func<TimeSpan, float> decay;
        private readonly float delta;

        #endregion

        // Done!
        #region Properties

        public float Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                //this.value = this.activator(value) * (this.decay == null ? 1 : this.decay(DateTime.Now - this.dateTime));
                this.dateTime = DateTime.Now;
            }
        }
        
        #endregion

        // Done!
        #region .Ctor

        // Done!
        public FuzzyFloat(Func<float, float> activator, Func<TimeSpan, float> decay, float delta)
        {
            this.passes = 0;
            this.dateTime = DateTime.Now;
            this.activator = activator;
            this.decay = decay;
            this.delta = delta;
            this.value = 0;
        }

        #endregion

        // Done!
        #region Methods

        // Done!
        private void CallActivator(float pass)
        {
            this.passes += pass;
            this.value = this.activator(this.passes)*(this.decay == null ? 1 : this.decay(DateTime.Now - this.dateTime));
            this.dateTime = DateTime.Now;
        }

        // Done!
        public void Increment()
        {
            this.CallActivator(this.delta);
        }

        // Done!
        public void Decrement()
        {
            this.CallActivator(-this.delta);
        }

        #endregion

    }
}
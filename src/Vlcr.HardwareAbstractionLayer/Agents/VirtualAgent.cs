using System;
using System.Timers;
using Vlcr.Core;
using Vlcr.HardwareAbstractionLayer.Core;
using Vlcr.HardwareAbstractionLayer.Events;
using Vlcr.Math;

namespace Vlcr.HardwareAbstractionLayer.Agents
{
    // Done!
    public sealed class VirtualAgent : HardwareAgent, IHardwareAgent
    {
        // Done!
        #region Internal Static Data

        private const float LowBattery  = 0.10f;
        private const float DeadBattery = 0.08f;

        #endregion

        // Done!
        #region Events

        public event EventHandler<BatteryEventArgs> LowBatteryWarning;
        public event EventHandler<BatteryEventArgs> DeadBatteryWarning;
        public event EventHandler ConnectionLost;
        public event EventHandler ConnectionRegain;

        #endregion

        // Done!
        #region Internal Readonly Data

        private readonly RandomGenerator g = new RandomGenerator();
        private readonly Timer timer = new Timer();
        private readonly Regression r = new Regression(Functions.Sigmoid(-1/30, -3.5f));
        private readonly DateTime dateTime = DateTime.Now;

        #endregion

        // Done!
        #region Internal Instance Data

        private float batery;
        private float angle;
        private Vector position = Vector.Empty;
        private bool isConnected = true;

        #endregion

        // Done!
        #region .Ctor

        // Done!
        public VirtualAgent()
        {   
            this.CtorHelper();   
        }

        // Done!
        private void CtorHelper()
        {
            UpdateBateryLife();
            this.EnableTimer();
        }

        // Done!
        private void EnableTimer()
        {
            this.timer.Interval = 1000;
            this.timer.Elapsed += TimerEvent;
            this.timer.Enabled = true;
            this.timer.Start();
        }

        #endregion

        // Done!
        #region Helpers

        // Done!
        private void UpdateBateryLife()
        {
            this.batery = r.GetValue((DateTime.Now - dateTime).Seconds);
        }

        #endregion

        // Done!
        #region Events

        // Done!
        private void TimerEvent(object sender, ElapsedEventArgs e)
        {
            this.UpdateBateryLife();
            if (batery <= DeadBattery)
            {
                Helpers.SafeInvoke(DeadBatteryWarning, this, new BatteryEventArgs(this.batery));
            }
            else if (batery <= LowBattery)
            {
                Helpers.SafeInvoke(LowBatteryWarning, this, new BatteryEventArgs(this.batery));
            }

            if (isConnected == true && g.NextInt(0, 10000) > 9000)
            {
                isConnected = false;
                Helpers.SafeInvoke(this.ConnectionLost, this);
            }
            if (isConnected == false && g.NextInt(0, 10000) > 1000)
            {
                isConnected = true;
                Helpers.SafeInvoke(this.ConnectionRegain, this);
            }
        }

        #endregion

        // Done!
        #region Implementation of IHardwareAgent

        // Done!
        public HardwareActionStatus Rotate(float radians, float speed)
        {
            if(isConnected == false)
            {
                return new HardwareActionStatus(
                TimeSpan.FromSeconds(0),
                new FuzzyBool(0),
                "Failure: Not Connected");
            }

            float t = (float)(5/(System.Math.PI/2f)*radians);
            var actionStatus = new HardwareActionStatus(
                TimeSpan.FromSeconds(t),
                new FuzzyBool(g.NextSingle(80, 100)),
                "Sucess");
            this.History.AddRotation(this, actionStatus, radians, speed);
            this.angle = Functions.Angle(this.angle + radians);
            return actionStatus;
        }

        // Done!
        public HardwareActionStatus Move(Vector heading, float speed)
        {
            if (isConnected == false)
            {
                return new HardwareActionStatus(
                TimeSpan.FromSeconds(0),
                new FuzzyBool(0),
                "Failure: Not Connected");
            }

            var d = Vector.Distance(this.position, heading);
            var actionStatus = new HardwareActionStatus(
                TimeSpan.FromSeconds(d*speed),
                new FuzzyBool(g.NextSingle(80, 100)),
                "Sucess");
            this.History.AddMove(this, actionStatus, heading, speed);
            this.position += heading;
            return actionStatus;
        }

        // Done!
        public HardwareStatus Status
        {
            get
            {
                return new HardwareStatus(this.batery) { IsCameraStereo = true };
            }
        }

        public RawImage LeftCameraImage
        {
            get { throw new NotImplementedException(); }
        }

        public RawImage RightCameraImage
        {
            get { throw new NotImplementedException(); }
        }

        // Done!
        public Vector Position
        {
            get { return this.position; }
        }

        #endregion
    }
}
//using System;
//using Vlcr.Core;
//using Vlcr.HardwareAbstractionLayer.Core;
//using Vlcr.HardwareAbstractionLayer.Events;

//namespace Vlcr.HardwareAbstractionLayer.Agents
//{
//    // Todo: Implement
//    public sealed class SurveyorSRV1 : HardwareAgent, IHardwareAgent
//    {
//        // Done!
//        #region Events

//        public event EventHandler<BatteryEventArgs> LowBatteryWarning;
//        public event EventHandler<BatteryEventArgs> DeadBatteryWarning;

//        #endregion

//        #region Implementation of IHardwareAgent

//        // Todo: Implement
//        public HardwareActionStatus Rotate(float angle, float speed)
//        {
//            this.History.AddRotation(this, new HardwareActionStatus(TimeSpan.FromTicks(0), new FuzzyBool(0), "Not Implemented"), angle, speed);
//            throw new NotImplementedException();
//        }

//        // Todo: Implement
//        public HardwareActionStatus Move(Vector heading, float speed)
//        {
//            this.History.AddMove(this, new HardwareActionStatus(TimeSpan.FromTicks(0), new FuzzyBool(0), "Not Implemented"), heading, speed);
//            throw new NotImplementedException();
//        }

//        // Todo: Implement
//        public HardwareStatus Status
//        {
//            get { throw new NotImplementedException(); }
//        }

//        // Todo: Implement
//        public RawImage LeftCameraImage
//        {
//            get { throw new NotImplementedException(); }
//        }

//        // Todo: Implement
//        public RawImage RightCameraImage
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public Vector Position
//        {
//            get { throw new NotImplementedException(); }
//        }

//        #endregion
//    }
//}
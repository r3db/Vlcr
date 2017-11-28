using System;
using System.Diagnostics.Contracts;
using Vlcr.Core;
using Vlcr.HardwareAbstractionLayer.Core;
using Vlcr.HardwareAbstractionLayer.Events;
using Vlcr.HardwareAbstractionLayer.History;
using Vlcr.Neural;

namespace Vlcr.HardwareAbstractionLayer.Agents
{
    public sealed class Agent : IHardwareAgent
    {
        // Done!
        #region Events

        public event EventHandler<BatteryEventArgs> LowBatteryWarning;
        public event EventHandler<BatteryEventArgs> DeadBatteryWarning;
        public event EventHandler ConnectionLost;
        public event EventHandler ConnectionRegain;

        #endregion

        // Done!
        #region Internal Instance Data

        private readonly IHardwareAgent agent;
        private readonly NeuralNetwork rotationNetwork = new NeuralNetwork(3, 10, 10, 4);
        private readonly NeuralNetwork moveNetwork = new NeuralNetwork(5, 10, 10, 4);

        #endregion

        // Done!
        #region .Ctor

        // Done!
        public Agent(IHardwareAgent agent)
        {
            Contract.Requires(agent != null);
            this.agent = agent;
        }

        #endregion

        // Done!
        #region Implementation of IHardwareAgent

        // Done!
        public HardwareActionStatus Rotate(float radians, float speed)
        {
            var prediction = this.PredictRotation(radians, speed, this.agent.Status.Batery);
            var real = this.agent.Rotate(radians, speed);
            if(HardwareActionStatus.CompareOnPrediction(prediction, real) == false)
            {
                //this.rotationNetwork.Train(prediction, real);
            }
            return real;
        }

        // Done!
        private HardwareActionStatus PredictRotation(float radians, float speed, float batery)
        {
            Console.WriteLine(this.rotationNetwork);
            return null;
        }

        // Done!
        public HardwareActionStatus Move(Vector heading, float speed)
        {
            return this.agent.Move(heading, speed);
        }

        // Done!
        public HardwareStatus Status
        {
            get { return this.agent.Status; }
        }

        // Done!
        public RawImage LeftCameraImage
        {
            get { return this.agent.LeftCameraImage; }
        }

        // Done!
        public RawImage RightCameraImage
        {
            get { return this.agent.RightCameraImage; }
        }

        // Done!
        public HardwareHistory History
        {
            get { return this.agent.History; }
        }

        public Vector Position
        {
            get { return this.agent.Position; }
        }

        #endregion
    }
}
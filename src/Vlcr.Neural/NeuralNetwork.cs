using System;
using System.Collections.Generic;

namespace Vlcr.Neural
{
    public sealed class NeuralNetwork
    {
        public NeuralNetwork(int input, int hidden, int output)
        {
            
        }

        public NeuralNetwork(int input, int next, params int[] other)
        {

        }
    }

    public struct BaseLine
    {
        //public Func<float, float, float, float>
    }

    public sealed class Neuron
    {
        public IList<Neuron>    Input           { get; private set; }
        public IList<Neuron>    Output          { get; private set; }
        public BaseLine         BaseLine        { get; private set; }
        public DateTime         LastActivated   { get; private set; }
        public TimeSpan         Reactivate      { get; private set; }
    }
}

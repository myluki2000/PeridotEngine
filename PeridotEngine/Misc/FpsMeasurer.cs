using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PeridotEngine.Misc
{
    public class FpsMeasurer
    {
        public int WindowSize
        {
            get => windowSize;
            set
            {
                windowSize = value;
                lastFrameTimes = new double[windowSize];
                lastFrameTimesIndex = 0;
            }
        }

        private int windowSize = 30;
        private double[] lastFrameTimes;
        private int lastFrameTimesIndex = 0;
        private Stopwatch stopwatch = new();

        public FpsMeasurer()
        {
            lastFrameTimes = new double[windowSize];
        }

        public void StartFrameTimeMeasure()
        {
            stopwatch.Restart();
        }

        public void StopFrameTimeMeasure()
        {
            stopwatch.Stop();
            lastFrameTimesIndex = (lastFrameTimesIndex + 1) % windowSize;
            lastFrameTimes[lastFrameTimesIndex] = stopwatch.Elapsed.TotalMilliseconds;
        }

        public double GetLastFrameTime()
        {
            return lastFrameTimes[lastFrameTimesIndex];
        }

        public double GetAverageFrameTime()
        {
            return lastFrameTimes.Average();
        }
    }
}

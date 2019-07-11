using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.Genetic
{
    public class ComputingBoundary
    {
        public double LowerBoundary { get; }

        public double UpperBoundary { get; }

        public int StepCount { get; set; }

        public double Step => (UpperBoundary - LowerBoundary) / StepCount;

        public ComputingBoundary( double lowerBoundary, double upperBoundary, int stepCount )
        {
            LowerBoundary = lowerBoundary;
            UpperBoundary = upperBoundary;
            StepCount = stepCount;
        }
    }
}

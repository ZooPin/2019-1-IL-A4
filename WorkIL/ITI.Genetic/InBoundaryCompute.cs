using ITI.Analyzer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.Genetic
{
    public class InBoundaryCompute
    {
        private readonly ComputingBoundary _xBoundary;
        private readonly ComputingBoundary _yBoundary;
        private readonly Dictionary<string, double> _resolvedIdentifiers;
        private readonly double[,] _dataSet;

        public InBoundaryCompute(ComputingBoundary xBoundary, ComputingBoundary yBoundary, Node secretFunction)
        {
            _xBoundary = xBoundary;
            _yBoundary = yBoundary;

            _resolvedIdentifiers = new Dictionary<string, double>
            {
                { "x", double.NaN },
                { "y", double.NaN }
            };

            _dataSet = ComputeDataSet( secretFunction ); 
        }

        private double[,] ComputeDataSet( Node secretFunction )
        {
            double[,] dataSet = new double[_xBoundary.StepCount,_yBoundary.StepCount];

            for( var i = 0; i <= _xBoundary.StepCount; ++i )
            {
                double x = _xBoundary.LowerBoundary + i * _xBoundary.Step;
                for( var j = 0; j <= _yBoundary.StepCount; ++j )
                {
                    double y = _yBoundary.LowerBoundary + j * _yBoundary.Step;

                    _resolvedIdentifiers["x"] = x;
                    _resolvedIdentifiers["y"] = y;

                    var v = new ComputeVisitor( _resolvedIdentifiers );
                    v.VisitNode( secretFunction );
                    dataSet[i, j] = v.Result;
                }
            }
            return dataSet;
        }

        public ComputeNodeResult ComputeInRange( Node candidate )
        {
            double gap = 0.0d;

            for( var i = 0; i <= _xBoundary.StepCount; ++i )
            {
                double x = _xBoundary.LowerBoundary + i * _xBoundary.Step;
                for( var j = 0; j <= _yBoundary.StepCount; ++j )
                {
                    double y = _yBoundary.LowerBoundary + j * _yBoundary.Step;

                    _resolvedIdentifiers["x"] = x;
                    _resolvedIdentifiers["y"] = y;

                    var v = new ComputeVisitor(_resolvedIdentifiers);
                    v.VisitNode( candidate );
                    gap += GapBetween( _dataSet[i,j], v.Result );
                }
            }
            return new ComputeNodeResult( candidate, gap );
        }

        private double GapBetween( double a, double b )
        {
            return Math.Pow( (a - b), 2 );
        }
    }
}

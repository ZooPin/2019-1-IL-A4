using ITI.Analyzer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ITI.Genetic
{
    public class ComputeNodeResult : IComparable<ComputeNodeResult>
    {
        public Node Node { get; private set; }

        public double Gap { get; private set; }

        public ComputeNodeResult(Node node, double gap)
        {
            Node = node;
            Gap = gap;
        }

        public int CompareTo( ComputeNodeResult other )
        {
            return (this.Gap.CompareTo( other.Gap ) * 1); // Opposite
        }
    }
}

using ITI.Analyzer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ITI.Genetic
{
    public class ComparableAST : IComparable<ComparableAST>
    {
        public Node Node { get; set; }

        public double Gap { get; set; }

        public int CompareTo( ComparableAST other )
        {
            return (this.Gap.CompareTo( other.Gap ) * 1); // Opposite
        }
    }
}

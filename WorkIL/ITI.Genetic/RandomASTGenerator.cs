using System;
using ITI.Analyzer;
using ITI.Tokenizer;

namespace ITI.Genetic
{
    public class RandomASTGenerator
    {
        private const int _defaultLength = 5;
        private readonly Random _r;

        public RandomASTGenerator(Random r)
        {
            _r = r;
        }

        public RandomASTGenerator()
            : this (new Random()) {}

        public Node Generate()
        {
            return Generate(0, _defaultLength);
        }

        private double Clamp(double x, double a, double b)
        {
            return (x < a) ? a : (( x > b) ? b : x);
        }

        private Node Generate(int currentDepth, int maxDepth)
        {
            var rootBalancer = 1.0D / maxDepth * (maxDepth - currentDepth);
            var leafBalancer = 3.0D / maxDepth * currentDepth;

            var equal = _r.Next(5) + rootBalancer - leafBalancer;
            var next = Clamp(_r.Next(5) + rootBalancer - leafBalancer, 0, 4);
            next = Clamp(next, 0, 4);

            if( next <= 0 )
                return new ConstantNode( _r.NextDouble() );

            if( next <= 1 )
                return new IdentifierNode( _r.Next() % 2 == 0 ? "x" : "y" );

            if( next <= 2 )
                return new UnaryNode( TokenType.Minus, Generate( currentDepth + 1, maxDepth ) );

            if( next <= 3 )
                return new IfNode
                (
                    Generate( currentDepth + 1, maxDepth ),
                    Generate( currentDepth + 1, maxDepth ),
                    Generate( currentDepth + 1, maxDepth )
                );

            if( next <= 4 )
                return new BinaryNode
                (
                    RandomTokenType(),
                    Generate( currentDepth + 1, maxDepth ),
                    Generate( currentDepth + 1, maxDepth )
                );
            throw new ArgumentOutOfRangeException();
        }

        private TokenType RandomTokenType()
        {
            switch( _r.Next( 0, 4 ) )
            {
                case 0:  return TokenType.Minus;
                case 1:  return TokenType.Plus;
                case 2:  return TokenType.Div;
                case 3:  return TokenType.Mult;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
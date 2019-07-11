using ITI.Analyzer;
using ITI.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.Genetic
{
    public class RandomNodeGenerator
    {
        private readonly Random _r = new Random();
        private readonly string[] _identifier = {"x", "y"};

        public Node Node()
        {
            switch(_r.Next(0, 5))
            {
                case 0: return BinaryNode();
                case 1: return ConstantNode();
                case 2: return IfNode();
                case 3: return UnaryNode();
                case 4: return IdentifierNode();
                default:
                    throw new NotSupportedException();
            }
        }

        private Node ConstantOrIdentifierNode()
        {
            if (_r.Next(0, 100) > 70)
            {
                return ConstantNode();
            }
            return IdentifierNode();

        }

        private Node BinaryOrIdentifierOrConstantNode()
        {
            var r = _r.Next(0,100);
            if (r > 70)
            {
                return BinaryNode();
            }
            
            if ( r < 70 && r > 40)
            {
                return IdentifierNode();
            }

            return ConstantNode();
        }

        private BinaryNode BinaryNode()
        {
            return new BinaryNode(Operation(), ConstantOrIdentifierNode(), ConstantOrIdentifierNode());
        }

        private ConstantNode ConstantNode()
        {
            return new ConstantNode(_r.NextDouble()*100);
        }

        private IfNode IfNode()
        {
            return new IfNode(BinaryOrIdentifierOrConstantNode(), BinaryOrIdentifierOrConstantNode(), BinaryOrIdentifierOrConstantNode());
        }

        private UnaryNode UnaryNode()
        {
            return new UnaryNode(TokenType.Minus, BinaryOrIdentifierOrConstantNode());
        }

        private IdentifierNode IdentifierNode()
        {
            return new IdentifierNode(_identifier[_r.Next(0, _identifier.Length)]);
        }
        private TokenType Operation()
        {
            switch(_r.Next(0, 4))
            {
                case 0: return TokenType.Plus;
                case 1: return TokenType.Minus;
                case 2: return TokenType.Mult;
                case 3: return TokenType.Div;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}

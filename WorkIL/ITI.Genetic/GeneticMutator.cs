using System;
using ITI.Analyzer;

namespace ITI.Genetic
{
    public class GeneticMutator : MutationVisitor
    {
        private readonly RandomNodeGenerator _rNode = new RandomNodeGenerator();
        private readonly GetRandomNodeVisitor _randomParent = new GetRandomNodeVisitor();
        private readonly Random _r = new Random();
        private Node _parent;

        public Node Birth(Node left, Node right)
        {
            _parent = right;
            return base.VisitNode(left);
        }

        public override Node Visit(BinaryNode n)
        {
            if(RollDice(out var node))
                return node;

            return base.VisitNode(n);
        }

        public override Node Visit(ConstantNode n)
        {
            if(RollDice(out var node))
                return node;

            return base.VisitNode(n);
        }

        public override Node Visit(IfNode n)
        {
            if(RollDice(out var node))
                return node;

            return base.VisitNode(n);
        }

        public override Node Visit(UnaryNode n)
        {
            if(RollDice(out var node))
                return node;

            return base.VisitNode(n);
        }

        public override Node Visit(IdentifierNode n)
        {
            if(RollDice(out var node))
                return node;

            return base.VisitNode(n);
        }

        private bool RollDice(out Node value)
        {
            var p = _r.Next(0, 100);

            // 45% of no change
            if (p > 55)
            {
                value = null;
                return false;
            }
            
            // 45% of the other parent
            if ( p < 55 && p > 10 )
            {
                value = RandomFromParent(_parent);
                return true;
            }

            // 10% of mutation
            value = RandomNode();
            return true;
        }

        private Node RandomNode()
        {
            return _rNode.Node();
        }

        private Node RandomFromParent(Node parent)
        {
            return _randomParent.Get(parent);
        }
    }
}
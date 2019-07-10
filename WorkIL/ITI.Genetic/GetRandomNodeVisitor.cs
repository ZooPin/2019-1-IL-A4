using System;
using ITI.Analyzer;

namespace ITI.Genetic
{
    public class GetRandomNodeVisitor: NodeVisitor
    {
        public Node ChoosenOne {get; private set;}
        private Random _r = new Random();
        public int TotalNode;
        private int _count;
        public void Clear()
        {
            _count = 0;
            TotalNode = 0;
            ChoosenOne = null;
        }

        public override void Visit(BinaryNode n)
        {
            if (IsTheChoosenOne())
            {
                ChoosenOne = n;
                return;
            }

            VisitNode(n.Left);
            if (ChoosenOne == null)
                VisitNode(n.Right);
        }

        public override void Visit(ConstantNode n)
        {
            if(IsTheChoosenOne())
            {
                ChoosenOne = n;
                return;
            }
        }

        public override void Visit(IdentifierNode n)
        {
            if (IsTheChoosenOne())
            {
                ChoosenOne = n;
                return;
            }
        }

        public override void Visit(IfNode n)
        {
            if (IsTheChoosenOne())
            {
                ChoosenOne = n;
                return;
            }

            if (ChoosenOne != null)
                VisitNode(n.Condition);

            if (ChoosenOne != null)
                VisitNode(n.WhenTrue);
            
            if (ChoosenOne != null)
                VisitNode(n.WhenFalse);
        }

        private bool IsTheChoosenOne()
        {
            if (_count++ >= TotalNode)
                return true;

            return _r.Next(0, 100) > 70;
        }
    }
}
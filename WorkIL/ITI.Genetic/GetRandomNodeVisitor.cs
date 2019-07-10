using System.Security.Cryptography.X509Certificates;
using System;
using ITI.Analyzer;

namespace ITI.Genetic
{
    public class GetRandomNodeVisitor: NodeVisitor
    {
        private int _choosen {get; set;}
        private Node _choosenNode {get; set;}
        private Random _r = new Random();
        private int _totalNode;
        private int _count;
        public void Clear()
        {
            _count = 0;
            _choosenNode = null;
        }

        public Node Get(Node n, int totalNode)
        {
            _choosen = _r.Next(0, totalNode);
            base.VisitNode(n);
            return _choosenNode;
        }

        public override void Visit(BinaryNode n)
        {
            if(IsOver) return;
            if(IsTheChoosenOne(n))
                return;

            VisitNode(n.Left);
            VisitNode(n.Right);
        }

        public override void Visit(ConstantNode n)
        {
            if(IsOver) return;
            IsTheChoosenOne(n);
        }

        public override void Visit(IdentifierNode n)
        {
            if(IsOver) return;
            IsTheChoosenOne(n);
        }

        public override void Visit(IfNode n)
        {
            if(IsOver) return;
            if (IsTheChoosenOne(n))
                return;

            VisitNode(n.Condition);
            VisitNode(n.WhenTrue);
            VisitNode(n.WhenFalse);
        }

        private bool IsTheChoosenOne(Node n)
        {
            if (_count++ == _choosen)
            {
                _choosenNode = n;
                return true;
            }
            return false;
        }
        private bool IsOver => _choosenNode != null; 
    }
}
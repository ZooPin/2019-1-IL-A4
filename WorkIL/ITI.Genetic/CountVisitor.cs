using ITI.Analyzer;

namespace ITI.Genetic
{
    public class CountVisitor :  NodeVisitor
    {
        public int Count {get; private set;}
        public void Clear() => Count = 0;

        public override void Visit(BinaryNode n)
        {
            Count++;
            base.Visit(n);
        }

        public override void Visit(IfNode n) 
        {
            Count++;
            base.Visit(n);
        }

        public override void Visit(ConstantNode n)
        {
            Count++;
        }

        public override void Visit(IdentifierNode n)
        {
            Count++;
        }

        public override void Visit(UnaryNode n)
        {
            Count++;
            base.Visit(n);
        }
    }
}
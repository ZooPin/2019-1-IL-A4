using FluentAssertions;
using ITI.Genetic;
using NUnit.Framework;

namespace SimpleUnitTests
{
    [TestFixture]
    public class GeneticTests
    {
        [Test]
        public void test_calculator()
        {
            var _r = new RandomNodeGenerator();
            var node = _r.Node();

            var randomOne = new GetRandomNodeVisitor();
            var optiVis = new ITI.Genetic.OptimizationVisitor();
            optiVis.VisitNode( node );
            var n = randomOne.Get( node, optiVis.NodeCount );
            n.Should().NotBeNull();
        }
    }
}

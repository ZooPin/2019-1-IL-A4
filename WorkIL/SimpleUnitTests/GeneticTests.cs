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
            var randomOne = new GetRandomNodeVisitor();

            var node = _r.Node();
            var n = randomOne.Get( node);
            
            n.Should().NotBeNull();
        }
    }
}

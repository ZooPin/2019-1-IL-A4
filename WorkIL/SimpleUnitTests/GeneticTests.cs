using FluentAssertions;
using ITI.Genetic;
using NUnit.Framework;

namespace SimpleUnitTests
{
    [TestFixture]
    public class GeneticTests
    {
        [Test]
        public void test_random_and_get_one()
        {
            var _r = new RandomNodeGenerator();
            var randomOne = new GetRandomNodeVisitor();

            var node = _r.Node();
            var n = randomOne.Get( node);
            randomOne.Clear();
            n.Should().NotBeNull();
        }

        [Test]
        public void test_create_child()
        {
            var _r = new RandomNodeGenerator();
            var daddy = _r.Node();
            var mommy = _r.Node();

            var genMutator = new GeneticMutator();

            var child = genMutator.Birth(daddy, mommy);

            child.Should().NotBeNull();
        }
    }
}

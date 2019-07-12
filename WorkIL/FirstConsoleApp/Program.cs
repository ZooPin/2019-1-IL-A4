using ITI.Analyzer;
using ITI.Genetic;
using SimpleUnitTests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FirstConsoleApp
{
    class CUser
    {
        public string Name { get; }

        public int Power { get; set; }

        public CUser()
            : this( "Default Name" )
        {
        }

        public CUser( string name )
        {
            Name = name;
        }

        public override string ToString() => $" Class {Name}";
    }

    struct SUser
    {
        public string Name { get; }

        public int Power { get; set; }

        // This is forbidden for performance reason.
        //public SUser()
        ////: this( "Default Name" )
        //{
        //    Power = 78;
        //    Name = "Toto";
        //}

        public SUser( string name )
        {
            Name = name;
            // For structs, all fields MUST be explicitly initialized.
            // This is not the case for class...
            Power = 0;
        }

        public override string ToString() => String.Format( " Struct {0}", Name );
    }


    class Program
    {
        static void Main( string[] args )
        {
            /*
            var acu = new CUser[100];
            var asu = new SUser[100];

            var cu = new CUser( "Spi" );
            var cu2 = cu;
            cu.Power = 42;
            Debug.Assert( cu2.Power == 42 );

            var su = new SUser( "Spi" );
            var su2 = su;
            su.Power = 42;
            Debug.Assert( su2.Power == 0 );

            // With array:
            asu[0].Power = 42;
            Debug.Assert( asu[0].Power == 42 );

            var lsu = new List<SUser>();
            lsu.Add( new SUser( "In a list." ) );

            // This NOW (newer C# version) is forbidden.
            // But before...
            // lsu[0].Power = 42;
            // We'd have had 0 here instead of the expercted 42...
            // Debug.Assert( lsu[0].Power == 0 );
            */

            string sfString = "(( 3 + x ) + ( y - 2 )) + ( x * y * 3 ) / 2";
            var sf = new SimpleAnalyzer().Parse( sfString );

            var boundary = new ComputingBoundary( -1.0d, 1.0d, 20 );
            var computer = new InBoundaryCompute(boundary, boundary, sf );
            var heap = new Heap<ComputeNodeResult>();

            var randomGenerator = new RandomASTGenerator();

            for(int i = 0; i < 100; i++ )
            {
                var candidate = randomGenerator.Generate();
                var nodeResult = computer.ComputeInRange( candidate );
                heap.Add( nodeResult );
            }

            var bestCandidate = new ComputeNodeResult[10];

            while(heap.Peek().Gap > 10.0d)
            {
                for( int i = 0; i < bestCandidate.Length; i++ )
                {

                    bestCandidate[i] = heap.RemoveMax();
                }

                heap.Clear();

                for( int i = 0; i < bestCandidate.Length; i++ )
                {
                    heap.Add( bestCandidate[i] );
                }

                var genMutator = new GeneticMutator();

                for( int i = 0; i < bestCandidate.Length - 1; i += 2 )
                {
                    for( int j = 0; j < 10; j++ )
                    {
                        var children = genMutator.Birth( bestCandidate[i].Node, bestCandidate[i + 1].Node );

                        heap.Add( computer.ComputeInRange( children ) );
                    }
                }
                Console.WriteLine( heap.Peek().Gap );
            }

            var printVisitor = new PrintVisitor();
            printVisitor.VisitNode( heap.Peek().Node );
            Console.WriteLine( printVisitor.Result );
            Console.ReadLine();
        }

    }

}

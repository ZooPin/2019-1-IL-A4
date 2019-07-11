using ITI.Analyzer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.Genetic
{
    public static class Temp
    {
        public static void ComputeInRange()
        {
            int count = 0;
            double gap = 0.0d;

            for( double x = -1.0d; x <= 1.0d; x += 0.1d )
            {
                for( double y = -1.0d; y <= 1.0d; y += 0.1d )
                {
                    Console.WriteLine( $"x : {x} y : {y}" );

                    double resultFs = SecretFunction(x, y); // Compute secret function with x and y 
                    double resultCandidate = Candidate(x, y); // Compute candidate function with x and y
                    //
                    //var v = new ComputeVisitor();
                    //
                    gap += GapBetween( resultFs, resultCandidate );

                    count++;
                }
            }
            Console.WriteLine($"Loop : {count}" );
            Console.WriteLine($"Gap between secret and candidate : {gap}");
            // Add to the heap
        }


        public static double SecretFunction (double x, double y)
        {
            return 1;
        }

        public static double Candidate( double x, double y )
        {
            return 0;
        }

        public static double GapBetween( double a, double b)
        {
            return Math.Pow((a - b), 2);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Rand0081.SimdTutorial
{
    public class Program
    {
        static int GetSplitPoint<T>(int desiredSize) where T : struct
        {
            var mask = ~(Vector<T>.Count - 1);
            return desiredSize & mask;
        }

        static void Main(string[] args)
        {
            const int testsize = 45;
            var a = Enumerable.Range(0, testsize).ToArray();
            var b = Enumerable.Range(0, testsize).ToArray();
            var c = new int[testsize];
            int i;
            int splitpoint = GetSplitPoint<int>(testsize);

            for (i = 0; i < splitpoint; i += Vector<int>.Count)
            {
                Console.WriteLine($"Vectorized implementation i = {i}");
                var avec = new Vector<int>(a, i);
                var bvec = new Vector<int>(b, i);
                (avec + bvec).CopyTo(c, i);
            }
            for (; i < testsize; ++i)
            {
                Console.WriteLine($"Simple loop i = {i}");
                c[i] = a[i] + b[i];
            }

            Console.WriteLine($"c[21] = {c[21]}");
        }
    }
}

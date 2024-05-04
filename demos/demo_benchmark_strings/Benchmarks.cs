using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using System.Text;

namespace demo_benchmark_strings
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        private const int Iterations = 10000;

        [Benchmark]
        public string ConcatenateStrings()
        {
            string result = "";
            for (int i = 0; i < Iterations; i++)
            {
                result += "Hello ";
            }
            return result;
        }

        [Benchmark]
        public string StringBuilderConcatenation()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Iterations; i++)
            {
                sb.Append("Hello ");
            }
            return sb.ToString();
        }
    }
}

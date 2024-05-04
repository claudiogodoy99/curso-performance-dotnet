using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using System.Text;

namespace demo_benchmark
{
    //uso dos diagnosers para ter mais informação osbre algo especifico 
    [MemoryDiagnoser]
    // [InliningDiagnoser]
    // [TailCallDiagnoser]
    // [EtwProfiler]
    // [ConcurrencyVisualizerProfiler]
    // [NativeMemoryProfiler]
    // [ThreadingDiagnoser]
    // [ExceptionDiagnoser]
    public class Benchmarks
    {
        [Benchmark]
        public void Scenario1()
        {
            var str = "uma string";

            for(int i=0;i<= 100;i++){
                str += "concat ";
            }

        }

        [Benchmark]
        public void Scenario2()
        {
            var str = new StringBuilder("uma string");
             for(int i=0;i<= 100;i++){
                str.Append("concat");
            }
        }
    }
}

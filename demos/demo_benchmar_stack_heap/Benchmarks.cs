using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace demo_benchmar_stack_heap
{
     [MemoryDiagnoser]
    public class Benchmarks
    {
        [Benchmark(Baseline = true)]
        public void StackAllocation()
        {
            int[] arr = new int[100];

            for(int i =0; i < 100; i++) {
                arr[i] = i;
            }
        }

        [Benchmark]
        public void HeapAllocation()
        {
             HeapAlloc[] arr = new HeapAlloc[100];

            for(int i =0; i < 100; i++) {
                arr[i] = new HeapAlloc(1);
            }
        }
    }
}



public class HeapAlloc
{
    public HeapAlloc(int value)
    {
        this.value = value;
    }
    public int value = 0;
}
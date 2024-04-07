using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace demo_benchmark_struct_vs_class
{
   [MemoryDiagnoser]
    public class Benchmarks
    {
        [Benchmark(Baseline = true)]
        public void StackAllocation()
        {
            HeapAllocSt[] arr = new HeapAllocSt[100];

            for(int i =0; i < 100; i++) {
                arr[i] = new HeapAllocSt{ value = i};
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
public struct HeapAllocSt{

    public HeapAllocSt(){
        
    }
    public int value = 0;
}


public class HeapAlloc
{
    public HeapAlloc(int value)
    {
        this.value = value;
    }
    public int value = 0;
}

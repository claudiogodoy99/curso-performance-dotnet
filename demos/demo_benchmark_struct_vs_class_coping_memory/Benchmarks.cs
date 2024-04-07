using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace demo_benchmark_struct_vs_class_coping_memory
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        [Benchmark(Baseline = true)]
        public void StackAllocation()
        {
            HeapAllocSt[] arr = new HeapAllocSt[100];

            for (int i = 0; i < 100; i++)
            {
                arr[i] = new HeapAllocSt(){value =new int[10000]};
            }

            for (int i = 0; i < 10000; i++)
            {   
                for (int n = 0; i < 100; i++)
                {
                    ModificaValorStruct(arr[n]);
                }
            }
        }

        [Benchmark]
        public void HeapAllocation()
        {
            HeapAlloc[] arr = new HeapAlloc[100];

            for (int i = 0; i < 100; i++)
            {
                arr[i] = new HeapAlloc(new int[10000]);
            }


            for (int i = 0; i < 10000; i++)
            {
                for (int n = 0; i < 100; i++)
                {
                    ModificaValorClass(arr[n]);
                }
            }
        }

        public void ModificaValorStruct(HeapAllocSt obj)
        {
            obj.value= [1,2];
        }

        public void ModificaValorClass(HeapAlloc obj)
        {
            obj.value= [1,2];
        }
    }
}
public struct HeapAllocSt
{

    public HeapAllocSt()
    {

    }
    public int[] value;
}


public class HeapAlloc
{
    public HeapAlloc(int[] value)
    {
        this.value = value;
    }
    public int[] value; 
}

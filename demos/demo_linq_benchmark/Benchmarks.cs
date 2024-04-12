using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace demo_linq_benchmark
{
    [MemoryDiagnoser]
    public class Benchmarks
    {

        List<string> strs = new List<string>();
        string padrao = "abcABC123!@#";

        [GlobalSetup]
        public void Setup()
        {
            Random random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                strs.Add(GenerateRandomString(random, 10));
            }

            string GenerateRandomString(Random random, int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+[{]};:',<.>/?";
                return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }

        }

        [Benchmark]
        public void LinqExpression()
        {
            //10 mil vezes
            for (int i = 0; i < 10000; i++)
            {
                var found = GetLongString(strs);
            }
        }


        [Benchmark]
        public void LoopUsandoContains()
        {
            //Executar a busca 10 mil vezes
            for (int i = 0; i < 10000; i++)
            {
                var found = GetLongStringFor(strs);

            }
        }

        public List<string> GetLongString(List<string> str) {
            return str.Where(s => s.Length > 10).ToList();
        }
        
        public List<string> GetLongStringFor(List<string> str) {
            var result = new List<string>();
            foreach (var s in str)
            {
                if (s.Length > 10)
                {
                    result.Add(s);
                }
            }
            return result;
        }
    }

}

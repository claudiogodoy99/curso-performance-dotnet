using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using System.Text.RegularExpressions;

namespace demo_regex_benchmark
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        const string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
        const int num_ismath = 5000;
        const string email = "address@domain200.com";

        Regex _regex = new Regex(pattern);

        [Benchmark]
        public void Non_Compiled() {

            for(int i = 0; i<= num_ismath; i++ ) {
                Regex regex = new Regex(pattern);
                regex.IsMatch(email);
            }
        }

        [Benchmark]
        public void Compiled() {
            

            for(int i = 0; i<= num_ismath; i++ ) {
                _regex.IsMatch(email);
            }
        }

       
        [Benchmark]
        public void Cached()
        {
             for(int i = 0; i<= num_ismath; i++ ) {
                
                ValidateEmail(email,_regex);
            }
        }

        static void ValidateEmail(string email, Regex regex)
        {
            regex.IsMatch(email);
        }

    }
}


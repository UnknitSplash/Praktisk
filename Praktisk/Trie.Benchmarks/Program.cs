using System;
using BenchmarkDotNet.Running;

namespace Trie.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TrieBenchmarks>();
        }
    }
}
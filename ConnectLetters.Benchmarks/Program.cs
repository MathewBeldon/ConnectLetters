using BenchmarkDotNet.Running;

namespace ConnectLetters.Benchmarks
{
    public class Program
    {
        public static void Main()
        {
            _ = BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}

using BenchmarkDotNet.Attributes;

namespace ConnectLetters.Benchmarks
{
    [MemoryDiagnoser]
    public class ConnectLettersBenchmark
    {
        [Arguments("ABABABAB")]
        [Arguments("AAAAAAAA")]
        [Arguments("BABBYYAYAAB")]
        [Arguments("XYABXYXYAB")]
        [Arguments("AXXBXYABXAYB")]
        [Benchmark(Description = "Eoghan's Connect Letters")]
        public void RunEoghan(string connections)
        {
            Eoghan.ProcessInput(connections);
        }

        [Arguments("ABABABAB")]
        [Arguments("AAAAAAAA")]
        [Arguments("BABBYYAYAAB")]
        [Arguments("XYABXYXYAB")]
        [Arguments("AXXBXYABXAYB")]
        [Benchmark(Description = "Matt's Connect Letters")]
        public void RunMatt(string connections)
        {
            Matt.ProcessInput(connections);
        }
    }
}

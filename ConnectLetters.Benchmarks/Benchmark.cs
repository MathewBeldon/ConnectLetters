using BenchmarkDotNet.Attributes;

namespace ConnectLetters.Benchmarks
{
    [MemoryDiagnoser]
    public class ConnectLetters
    {
        [Arguments("ABABABAB")]
        [Arguments("AAAAAAAA")]
        [Arguments("BABBYYAYAAB")]
        [Arguments("XYABXYXYAB")]
        [Arguments("AXXBXYABXAYB")]
        [Benchmark(Description = "Connect Letters")]
        public void Run(string connections)
        {
            LetterConnector.Utilities.ProcessInput(connections);
        }
    }
}
